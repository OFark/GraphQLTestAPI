using Microsoft.EntityFrameworkCore;
using Sera.Infrastructure.SQL.Repositories;
using TestWebAPI;
using TestWebAPI.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IGraphQLRepository, GraphQLRepository>()
                .AddPooledDbContextFactory<dBContext>(options => { options.UseInMemoryDatabase("database"); });


builder.ConfigureGraphQL();

var app = builder.Build();

app.MapGraphQL();

app.UseHttpsRedirection();


app.Run();

