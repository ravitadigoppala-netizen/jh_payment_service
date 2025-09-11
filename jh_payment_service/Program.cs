using jh_payment_service.Mapper;
using jh_payment_service.Middleware;
using jh_payment_service.Model;
using jh_payment_service.Service;
using jh_payment_service.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add services to the container.
builder.Services.AddHttpClient<RefundService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5041/api/Refund"); // replace with actual base URL
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IProcessPaymentService, ProcessPaymentService>();
builder.Services.AddScoped<IValidator, Validator>();
builder.Services.AddSingleton<IHttpClientService, HttpClientService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddHttpClient("PaymentDb-Microservice", client =>
{
    client.BaseAddress = new Uri("http://localhost:5110/api/"); // Replace with target service URL
    client.Timeout = TimeSpan.FromSeconds(30);
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});

// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0); // Default: v1.0
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true; // Shows supported versions in headers

    // Accept version from URL, header, or query string
    options.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),   // ?api-version=1.0
        new HeaderApiVersionReader("X-Version"),          // Header: X-Version: 1.0
        new MediaTypeApiVersionReader("ver"));            // Header: Content-Type: application/json;ver=1.0
});

// Register payment handlers
builder.Services.AddSingleton<CardPaymentHandler>();
builder.Services.AddSingleton<UPIPaymentHandler>();
builder.Services.AddSingleton<NetBankingHandler>();
builder.Services.AddSingleton<WalletHandler>();

builder.Services.AddSingleton<IPaymentService>(provider =>
{
    var handlers = new Dictionary<PaymentMethodType, IPaymentHandler>
    {
        { PaymentMethodType.Card, provider.GetRequiredService<CardPaymentHandler>() },
        { PaymentMethodType.UPI, provider.GetRequiredService<UPIPaymentHandler>() },
        { PaymentMethodType.NetBanking, provider.GetRequiredService<NetBankingHandler>() },
        { PaymentMethodType.Wallet, provider.GetRequiredService<WalletHandler>() }
    };

    return new PaymentService(handlers);
});


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
