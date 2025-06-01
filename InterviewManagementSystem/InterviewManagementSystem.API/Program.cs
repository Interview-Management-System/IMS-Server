global using Microsoft.AspNetCore.SignalR;
using InterviewManagementSystem.API.Configurations;
using InterviewManagementSystem.API.Middlewares;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

SerilogConfiguration.ConfigureSerilog();
builder.Services.AddSignalR();
builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var env = builder.Environment;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(env);
builder.Services.AddMapper();
builder.Services.AddFluentValidation();
builder.Services.AddExceptionHandlers();
builder.Services.AddInjectionService(configuration, env);
builder.Services.AddIMSAuthentication(configuration);
builder.Services.AddIMSAuthorization();
builder.Services.AddIMSCors();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCompression();

var app = builder.Build();

app.UseImsSwagger(builder);
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseMiddleware<UncompressedLoggingMiddleware>();
app.UseResponseCompression();
app.UseMiddleware<CompressedSizeLoggingMiddleware>();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CancellationTokenMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.MapControllers();
app.UseHubs();

app.Run();