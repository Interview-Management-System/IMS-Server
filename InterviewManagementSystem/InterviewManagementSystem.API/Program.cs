using System.Text.Json.Serialization;
using InterviewManagementSystem.API.Configurations;
using InterviewManagementSystem.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services
    .AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddJWTAuthentication(builder.Configuration);
builder.Services.AddCrossOriginResourceSharing();
builder.Services.AddInjectionService();
//builder.Services.AddExceptionHandlers();
builder.Services.AddHttpContextAccessor();





var app = builder.Build();
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
