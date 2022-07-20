using Curso.ComercioElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Dominio.Repositories
{
    public interface ICartRepository
    {
        Task<ICollection<Cart>> GetAsync();
    }
}
