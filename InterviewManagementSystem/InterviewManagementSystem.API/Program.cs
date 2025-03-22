global using InterviewManagementSystem.Application.DTOs;
global using Microsoft.AspNetCore.SignalR;
using InterviewManagementSystem.API.Configurations;
using InterviewManagementSystem.API.Middlewares;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSignalR();
builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Configuration
    .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "infra-settings.json"), optional: false, reloadOnChange: true);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddJWTAuthentication(configuration);
builder.Services.AddRoleAuthorization();
builder.Services.AddCorsPolicy();
builder.Services.AddMapper();
builder.Services.AddFluentValidation();
builder.Services.AddInjectionService(configuration);
builder.Services.AddExceptionHandlers();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.AddConfig();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CancellationTokenMiddleware>();
app.MapControllers();
app.UseHubs();

app.Run();

