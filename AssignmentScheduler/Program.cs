using AssignmentScheduler.Api.Extensions;
using FluentMigrator.Runner;
using AssignmentScheduler.Migrations;
using AssignmentScheduler.Repository;
using AssignmentScheduler.Repository.Factories;
using AssignmentScheduler.Repository.Factories.Interfaces;
using AssignmentScheduler.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddCors(options =>
    options.AddPolicy(name: "AllowCors",
        builder =>
        {
            builder.WithOrigins()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
        }));

builder.Services.AddFluentMigratorCore()
    .AddLogging(c => c.AddFluentMigratorConsole())
    .ConfigureRunner(c => c
          .AddSqlServer()
          .WithGlobalConnectionString("Server=DESKTOP-O3H3HP8;Database=AssignmentSchedulerDb;User id=User3;password=user3;")
          .ScanIn(typeof(_2022052500).Assembly).For.Migrations());

// Add services to the container.

builder.Services.AddControllers();

// Factories
builder.Services.AddTransient<IConnectionFactory, ConnectionFactory>();

// Repositories
builder.Services.AddTransient<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowCors");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.MigrateUp();

app.Run();
