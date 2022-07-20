using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Dominio.GroupServicesDominio
{
    public static class DominioGroupServiceCollectionExtension
    {
        public static IServiceCollection AddDominioGroup(this IServiceCollection services, IConfiguration config)
        {            
            return services;
        }
    }
}
