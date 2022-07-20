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
    public class CartRepository : ICartRepository
    {
        private readonly ComercioElectronicoDbContext context;

        public CartRepository(ComercioElectronicoDbContext context)
        {
            this.context = context;
        }

        public async Task<ICollection<Cart>> GetAsync()
        {
            return await context.Carts.ToListAsync();
        }
    }
}
