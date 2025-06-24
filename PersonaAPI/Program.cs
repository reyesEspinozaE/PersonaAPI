using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonaAPI.Context;
using PersonaAPI.Models;
using PersonaAPI.Services.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("ConnectionDB");
var apiToken = builder.Configuration.GetSection("TokenSettings");

// Configurar Entity Framework con PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.Configure<TokenSettings>(apiToken);


builder.Services.AddScoped<ValidateTokenFilter>();

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(e =>
{
    e.SwaggerDoc("v1", new OpenApiInfo { Title = "Legal", Version = "v1" });
    //Inyectar la definición de seguridad para el Bearer token de Autorización en el Headers
    e.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter Token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // Agregar los requerimientos de seguridad para incluir el token en el Headers
    e.AddSecurityRequirement(new OpenApiSecurityRequirement
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

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

// No se encontrarán problemas
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
