using CarRental.Api.Endpoints;
using CarRental.Infrastructure;
using CarRental.Infrastructure.Sql;
using CarRental.UseCase.Booking.Create;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddInfrastructureSql(builder.Configuration);
builder.Services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(CreateCommand).Assembly));
builder.Services.AddCors(s => s.AddPolicy("AllowAllOrigins", b => b.WithOrigins("*").AllowAnyHeader().AllowAnyMethod()));
var app = builder.Build();

app.MapBookingEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseCors("AllowAllOrigins");
}

app.UseHttpsRedirection();

await app.RunAsync();