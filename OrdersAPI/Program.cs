using BusinessLogicLayer.DependencyInjection;
using DataAccessLayer.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessLayer();
builder.Services.AddBusinessLogicLayer();
var app = builder.Build();


app.Run();