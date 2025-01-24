global using InterviewManagementSystem.Application.DTOs;
using InterviewManagementSystem.API.Configurations;
using InterviewManagementSystem.API.Middlewares;
using InterviewManagementSystem.Application.Shared.Helpers;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

var configuration = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddJWTAuthentication(configuration);
builder.Services.AddRoleAuthorization();
builder.Services.AddCrossOriginResourceSharing();
builder.Services.AddMapper();
builder.Services.AddFluentValidation();
builder.Services.AddInjectionService(configuration);
builder.Services.AddExceptionHandlers();
builder.Services.AddHttpContextAccessor();
FilterHelper.Service = builder.Services.BuildServiceProvider();


var app = builder.Build();


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


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

app.Run();

