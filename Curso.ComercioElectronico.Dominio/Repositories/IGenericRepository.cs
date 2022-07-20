using Curso.ComercioElectronico.Dominio.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Dominio.Repositories
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<ICollection<T>> GetAsync();
        Task<ICollection<T>> GetListAsync(int limit=20);
        IQueryable<T> GetQueryable();        
        Task<T> GetAsync(string code);
        
        Task<T> GetAsync(Guid Id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);
        
        Task DeleteAsync(T entity);
        //opcionales
        //Task ViewOrder(Guid Id);

    }
}
