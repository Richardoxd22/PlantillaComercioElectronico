using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface ICartAppService
    {   
        Task<ICollection<CartDto>> GetAllAsync();        
        Task<CartPagination<CartDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Name", string order = "asc");
        Task<CartDto> GetAsync(Guid Id);

        Task CreateAsync(CreateCartDto cartDto);

        Task UpdateAsync(Guid Id, CreateCartDto cartDto);

        Task DeleteAsync(Guid Id);
    }

    public class CartPagination<T>
    {
        public int Total { get; set; }
        public int totalsum { get; set; }
        public ICollection<T> Items { get; set; } = new List<T>();
    }
}
