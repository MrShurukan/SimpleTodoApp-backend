using Microsoft.EntityFrameworkCore;
using SimpleTodoApp;
using SimpleTodoApp.DatabaseContexts;
using SimpleTodoApp.Models;
using SimpleTodoApp.Repositories;

MapsterConfiguration.Configure();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

const string allowSpecificOrigins = "CorsPolicy";

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowSpecificOrigins,
        corsBuilder => corsBuilder
            .AllowAnyMethod()
            .AllowCredentials()
            .SetIsOriginAllowed((host) => true)
            .AllowAnyHeader());
});

var dbConnectionString = File.ReadAllText(".dbconnection");

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(dbConnectionString);
});

builder.Services.AddScoped<ICategoryRepository, CategoryDatabaseRepository>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemDatabaseRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();