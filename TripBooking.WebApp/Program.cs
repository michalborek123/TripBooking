using System.Reflection;
using TripBooking.Core.Infrastructure;
using TripBooking.Data.Context;
using TripBooking.Data.Mappings;
using TripBooking.WebApp.Midlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});
builder.Services.AddRepositories();
builder.Services.AddDataBase();
builder.Services.AddMappings();

builder.Services.AddSingleton<ExceptionMiddlware>();

builder.Services.AddValidation();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddlware>();

app.MapControllers();

app.Run();
