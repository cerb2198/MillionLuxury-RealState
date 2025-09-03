using MillionLuxury.RealEstate.API;
using MillionLuxury.RealEstate.API.Extensions;
using MillionLuxury.RealEstate.Application;
using MillionLuxury.RealEstate.Infrastructure;
using MillionLuxury.RealEstate.Infrastructure.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddApplication();
builder.Services.AddInfrastructure(config);
builder.Services.AddApi(builder);

var app = builder.Build();

await app.Services.InitializeDatabaseAsync(config);

app.ConfigureApplication(app.Environment);

app.Run();
