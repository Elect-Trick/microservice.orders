using BusinessLogicLayer.DependencyInjection;
using DataAccessLayer.DependencyInjection;
using OrdersAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessLayer();
builder.Services.AddBusinessLogicLayer();
builder.Services.AddControllers();
var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();