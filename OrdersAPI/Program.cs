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
builder.Services.AddCors();
var app = builder.Build();

app.UseExceptionHandlingMiddleware();
app.UseCors(config =>
{
    config.WithOrigins("http://localhost:42000")
        .AllowAnyHeader()
        .AllowAnyMethod();
});
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();