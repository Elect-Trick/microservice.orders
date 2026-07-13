using BusinessLogicLayer.DependencyInjection;
using DataAccessLayer.DependencyInjection;
using OrdersAPI.Middleware;
using FluentValidation;
using BusinessLogicLayer.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessLayer();

builder.Services.AddBusinessLogicLayer();
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<OrderValidator>();
var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();