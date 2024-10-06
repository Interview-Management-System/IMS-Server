global using InterviewManagementSystem.Application.DTOs;
using InterviewManagementSystem.API.Configurations;
using InterviewManagementSystem.API.Middlewares;
using InterviewManagementSystem.Application.CustomClasses.Helpers;
using System.Text.Json.Serialization;



var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        //x.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddJWTAuthentication(builder.Configuration);
builder.Services.AddRoleAuthorization();
builder.Services.AddCrossOriginResourceSharing();
builder.Services.AddMapper();
builder.Services.AddFluentValidation();
builder.Services.AddInjectionService();
//builder.Services.AddExceptionHandlers();
builder.Services.AddHttpContextAccessor();
FilterHelper.service = builder.Services.BuildServiceProvider();


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

