using Curso.ComercioElectronico.Dominio.Entities;
using Curso.ComercioElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Infraestructura.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ComercioElectronicoDbContext context;

        public BrandRepository(ComercioElectronicoDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Brand>> GetAsync()
        {
            return await context.Brands.ToListAsync();
        }        
    }
}
