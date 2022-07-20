using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IOrderAppService
    {

        Task<ICollection<OrderDto>> GetAllAsync();

        Task<OrderDto> GetAsync(Guid Id);

        Task CreateAsync(CreateOrderDto brandDto);

        Task UpdateAsync(Guid Id,CreateOrderDto brandDto);
        Task DeleteAsync(Guid Id);
    }

}
