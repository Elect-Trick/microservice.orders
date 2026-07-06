using BusinessLogicLayer.DependencyInjection;
using DataAccessLayer.DependencyInjection;
using OrdersAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessLayer();
builder.Services.AddBusinessLogicLayer();
var app = builder.Build();

app.UseExceptionHandlingMiddleware();

app.Run();