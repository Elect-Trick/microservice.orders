using BusinessLogicLayer.DependencyInjection;
using BusinessLogicLayer.HttpClient;
using BusinessLogicLayer.Policies;
using BusinessLogicLayer.PollyContracts;
using DataAccessLayer.DependencyInjection;
using OrdersAPI.Middleware;
using FluentValidation;
using BusinessLogicLayer.Validators;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessLayer();
builder.Services.AddHttpClient<UserMicroServiceClient>(client =>
{
    client.BaseAddress = new Uri($"http://{Environment.GetEnvironmentVariable("UserMSName")}:{Environment.GetEnvironmentVariable("UserMSPort")}/");
}).AddPolicyHandler((serviceProvicer, request) =>
{
    return serviceProvicer.GetRequiredService<IUserMicroServicePolicies>().GetRetryPolicy();
});

builder.Services.AddHttpClient<ProductMicroServiceClient>(client =>
{
    client.BaseAddress = new Uri($"http://{Environment.GetEnvironmentVariable("ProductsMSName")}:{Environment.GetEnvironmentVariable("ProductsMSPort")}/");
});


builder.Services.AddBusinessLogicLayer();
builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<OrderValidator>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("OrdersCorsPolicy",builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader().AllowCredentials() ;
    });
});

var app = builder.Build();
app.UseExceptionHandlingMiddleware();
app.UseRouting();
app.UseCors("OrdersCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();


app.Run();
