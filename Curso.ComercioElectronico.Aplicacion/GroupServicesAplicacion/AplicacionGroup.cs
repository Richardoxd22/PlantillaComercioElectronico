using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Aplicacion.ServicesImpl;
using Curso.ComercioElectronico.Dominio.Repositories;
using Curso.ComercioElectronico.Infraestructura.Repositories;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.GroupServicesAplicacion
{
    public static class AplicacionGroupServiceCollectionExtension
    {
        public static IServiceCollection AddAplicaciongroup(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<IBrandAppService, BrandAppService>();
            services.AddScoped<IProductTypeAppService, ProductTypeAppService>();            
            services.AddScoped<IOrderAppService, OrderAppService>();            
            services.AddScoped<IDeliveryModeAppService, DeliveryModeAppService>(); 
            services.AddScoped<ICartAppService, CartAppService>(); 
            //Validaciones 
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            //agregar todos los perfiles que existen en el proyecto 
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
