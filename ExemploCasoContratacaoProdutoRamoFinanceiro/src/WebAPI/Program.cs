using System.Runtime.CompilerServices;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Application.DependencyInjection;
using ExemploCasoContratacaoProdutoRamoFinanceiro.Infrastructure.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using WebAPI.Filters;

[assembly: InternalsVisibleTo("ExemploCasoContratacaoProdutoRamoFinanceiro.Application.IntegrationTests")]
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => options.Filters.Add<ApiExceptionFilterAttribute>())
                .AddNewtonsoftJson(options => options.SerializerSettings.Converters.Add(new StringEnumConverter()))
                .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// Customise default API behaviour
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddSwaggerGen();
builder.Host.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();