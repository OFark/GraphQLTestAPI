using FluentValidation;
using HotChocolate.Data.Filters;
using HotChocolate.Execution.Configuration;
using HotChocolate.Utilities;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TestWebAPI.Interfaces;
using TestWebAPI.Schema.Queries;

namespace TestWebAPI;

public static class DependencyInjection
{
    public static WebApplicationBuilder ConfigureGraphQL(this WebApplicationBuilder builder)
    {

        builder.Services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddStronglyTypesIds()
            .AddMutationConventions()
            .RegisterService<IGraphQLRepository>(ServiceKind.Resolver)
            .AddQueryType<Query>()
            .AddTypeExtenders()
            .AddFiltering()
            .AddSorting()
            .AddProjections()
            .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment())
        ;


        return builder;
    }

    private static IRequestExecutorBuilder AddStronglyTypesIds(this IRequestExecutorBuilder builder)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        var stronglyTypeIDs = assemblies.SelectMany((a) => from t in a.GetLoadableTypes()
                                                           where t.GetTypeInfo().IsValueType &&
                                                                 t.GetCustomAttributes<GeneratedCodeAttribute>(true).Any(x => x.Tool == "StronglyTypedId")
                                                           select t).ToList();

        var guidTypes = stronglyTypeIDs.Where(t => t.GetConstructor([typeof(Guid)]) is not null);
        var stringTypes = stronglyTypeIDs.Where(t => t.GetConstructor([typeof(string)]) is not null);

        builder.AddTypeConverter(GuidChangeType);
        builder.AddTypeConverter<string, DateTime>(s => DateTime.Parse(s));

        Action<IFilterConventionDescriptor> configureFilter = x => x.AddDefaults();

        foreach (var t in guidTypes)
        {
            builder.BindRuntimeType(t, typeof(UuidType));
            configureFilter += x => x.BindRuntimeType(t, typeof(UuidOperationFilterInputType));
        }

        var filterConvention = new FilterConvention(configureFilter);

        builder.AddConvention<IFilterConvention>(filterConvention);

        return builder;
    }

    private static IRequestExecutorBuilder AddTypeExtenders(this IRequestExecutorBuilder builder)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        var extendingTypes = assemblies.SelectMany((a) => from t in a.GetLoadableTypes()
                                                          where t.GetTypeInfo().IsClass &&
                                                                t.GetCustomAttributes<ExtendObjectTypeAttribute>(true).Any()
                                                          select t).ToList();

        foreach (var t in extendingTypes)
        {
            builder.AddTypeExtension(t);
        }

        return builder;
    }
    private static Type[] GetLoadableTypes(this Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException ex)
        {
            return (from t in ex.Types
                    where t != null
                    select t).ToArray();
        }
    }

    private static bool GuidChangeType(Type inputType, Type outputType, [NotNullWhen(true)] out ChangeType? changeType)
    {
        changeType = null;
        Console.WriteLine("Customer Converter");

        if (inputType == null || outputType == null)
            return false;

        if (inputType.GetCustomAttribute<GeneratedCodeAttribute>(true) is GeneratedCodeAttribute igca && igca.Tool == "StronglyTypedId" &&
            outputType == typeof(Guid))
        {
            changeType = o => inputType.GetProperty("Value")?.GetValue(o);
            return true;
        }

        if (inputType == typeof(Guid) &&
            outputType.GetCustomAttribute<GeneratedCodeAttribute>(true) is GeneratedCodeAttribute ogca && ogca.Tool == "StronglyTypedId")
        {
            changeType = o => outputType.GetConstructor([typeof(Guid)])?.Invoke([o]);
            return true;
        }

        return false;
    }
}