using CarRental.Api.Endpoints;
using CarRental.Infrastructure;
using CarRental.UseCase.Booking.Create;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(CreateCommand).Assembly));
builder.Services.AddCors(s => s.AddPolicy("AllowAllOrigins", b => b.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
builder.Configuration.AddJsonFile("appsettings.development.json", true, false);
var app = builder.Build();

app.MapBookingEndpoints();
app.MapSettingsEndpoints();
app.MapCarEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

await app.RunAsync();