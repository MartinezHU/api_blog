using Blog.Application;
using Blog.Infrastructure;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


// Habilitar PII para ver detalles completos del error
IdentityModelEventSource.ShowPII = true;

// Cargar variables de entorno desde .env
Env.Load();

// Obtener la clave secreta del entorno y verificar que no sea nula

var encodedKey = Environment.GetEnvironmentVariable("API_AUTH_SECRET_KEY");
var decodedKey = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(encodedKey!));

if (string.IsNullOrEmpty(decodedKey))
{
    throw new InvalidOperationException("La clave secreta de autenticación no está configurada.");
}

var key = Encoding.UTF8.GetBytes(decodedKey);

// Configurar autenticación JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key), // Usar la clave secreta
        ValidateIssuer = true, // Validar el emisor (issuer)
        ValidIssuer = "api_auth", // Debe coincidir con el valor de "iss" en el token
        ValidateAudience = false, // No validar el audience (puedes cambiarlo si es necesario)
        ValidateLifetime = true, // Validar la expiración del token
        ClockSkew = TimeSpan.Zero, // Sin margen de tiempo para la expiración
        RequireSignedTokens = true, // Asegurarse de que el token esté firmado
        TryAllIssuerSigningKeys = true // Intentar todas las claves si no hay un kid
    };

    // Opcional: Habilitar logging para depuración
    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("Authentication failed: " + context.Exception.Message);
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("Token validated successfully.");
            return Task.CompletedTask;
        }
    };
});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor(); // Para procesar las peticiones HTTP

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

//builder.Services.AddAuthorization(options =>
//{
//    //options.AddPolicy("PostOwnerPolicy", policy => policy.Requirements.Add(new PostOwnerRequirement()));
//    //options.AddPolicy("CommentModeratorPolicy", policy => policy.Requirements.Add(new CommentModeratorRequirement()));
//});

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Aplicar middlewares en el orden correcto
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
