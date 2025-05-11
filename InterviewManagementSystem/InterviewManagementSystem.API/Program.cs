global using Microsoft.AspNetCore.SignalR;
using InterviewManagementSystem.API.Configurations;
using InterviewManagementSystem.API.Middlewares;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

SerilogConfiguration.ConfigureSerilog();
// opts => opts.AddFilter<CancellationFilter>()
builder.Services.AddSignalR();
builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

configuration.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "infra-settings.json"), optional: false, reloadOnChange: true);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddMapper();
builder.Services.AddFluentValidation();
builder.Services.AddExceptionHandlers();
builder.Services.AddInjectionService(configuration);
builder.Services.AddIMSAuthentication(configuration);
builder.Services.AddIMSAuthorization();
builder.Services.AddIMSCors();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CancellationTokenMiddleware>();
app.UseMiddleware<LoggingMiddleware>();
app.MapControllers();
app.UseHubs();

app.Run();