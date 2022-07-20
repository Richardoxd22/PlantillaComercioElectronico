using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IProductAppService
    {   
        Task<ICollection<ProductDto>> GetAllAsync();
        //para recuperar el limite de la paginacion 
        //Task<ICollection<ProductDto>> GetListAsync(int limit=10);

        //parametros de paginacion, Ordenamiento , busqueda 
        Task<ResultPagination<ProductDto>> GetListAsync(string? search="Name", int offset = 0, int limit = 10, string sort="Name", string order="asc");
        Task<ProductDto> GetAsync(Guid Id);

        Task CreateAsync(CreateProductDto productDto);

        Task UpdateAsync(Guid Id, CreateProductDto productDto);

        Task DeleteAsync(Guid Id);
    }

    public class ResultPagination<T>
    {
        public int Total { get; set; }
        public ICollection<T> Items { get; set; } = new List<T>();
    }
}
