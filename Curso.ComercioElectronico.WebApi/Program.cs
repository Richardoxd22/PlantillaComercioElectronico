using Curso.ComercioElectronico.Aplicacion;
using Curso.ComercioElectronico.Aplicacion.ExcepcionCustom;
using Curso.ComercioElectronico.Aplicacion.GroupServicesAplicacion;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Aplicacion.ServicesImpl;
using Curso.ComercioElectronico.Dominio;
using Curso.ComercioElectronico.Dominio.GroupServicesDominio;
using Curso.ComercioElectronico.Dominio.Repositories;
using Curso.ComercioElectronico.Infraestructura;
using Curso.ComercioElectronico.Infraestructura.GroupServices;
using Curso.ComercioElectronico.Infraestructura.Repositories;
using Curso.ComercioElectronico.WebApi.Controllers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers(options =>

{
    //Aplicar filter globalmente a todos los controller
    options.Filters.Add<ApiExceptionFilterAttribute>();
});

//Agregar conexion a base de datos
builder.Services.AddDbContext<ComercioElectronicoDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ComercioElectronico"));
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add CORS
builder.Services.AddCors();

//1. Configurar el esquema de Autentificacion JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{

    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});


//configuration
builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JWT"));

//Asp.net (Es el tercer actor quien crea los objetos)
//Configurar las dependencias. Se lo realiza con IServiceCollection

//forma coorecta a la logica de tomar el extendido
builder.Services.AddInfraestructuraGroup(builder.Configuration);
builder.Services.AddAplicaciongroup(builder.Configuration);
builder.Services.AddDominioGroup(builder.Configuration);
builder.Services.AddFluentValidationAutoValidation();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Politica global CORS Middleware
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // Permitir cualquier origen
    .AllowCredentials());


//2. registra el middleware que usa los esquemas de autenticación registrados
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
