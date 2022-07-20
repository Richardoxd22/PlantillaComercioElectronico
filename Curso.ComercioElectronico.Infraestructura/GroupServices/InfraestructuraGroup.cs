using Curso.ComercioElectronico.Dominio;
using Curso.ComercioElectronico.Dominio.Repositories;
using Curso.ComercioElectronico.Infraestructura.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Infraestructura.GroupServices
{
    public static class InfraestructuraGroupServiceCollectionExtension
    {
        public static IServiceCollection AddInfraestructuraGroup(this IServiceCollection services, IConfiguration config)
        {

            services.AddDbContext<ComercioElectronicoDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();            
            //agregadas
            services.AddScoped<IOrderRepository, OrderRepository>();            
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IDeliveryModeRepository, DeliveryModeRepository>();

            return services;
        }
    }   
}

