using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OrderGeneratorApi.Domain.Interfaces;
using OrderGeneratorApi.Infra.Queues.Order;
using OrderGeneratorApi.App.UseCases.Order;
using OrderGeneratorApi.Infra.Settings;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Autorização
var publicKey = File.ReadAllText("./oauth/keys/oauth-public.key");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new RsaSecurityKey(LoadRsaPublicKey(publicKey))
    };
});

static RSA LoadRsaPublicKey(string publicKeyPem)
{
    var rsa = RSA.Create();
    rsa.ImportFromPem(publicKeyPem.ToCharArray());
    return rsa;
}

// Configurações do RabbitMQ
builder.Services.Configure<RabbitMqSettings>(
    builder.Configuration.GetSection("RabbitMqSettings")
);

// Buindings Interfaces, Implementações e UseCases
builder.Services.AddScoped<IOrder, OrderQueue>();
builder.Services.AddScoped<CreateOrderUseCase>();


// Configura Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/app-log.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .CreateLogger();

// Define Serilog como logger principal
builder.Host.UseSerilog();

// Configura versionamento
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

// Registrando as controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
