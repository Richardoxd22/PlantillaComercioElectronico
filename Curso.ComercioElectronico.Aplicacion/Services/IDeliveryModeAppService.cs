using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IDeliveryModeAppService
    {   
        Task<ICollection<DeliveryModeDto>> GetAllAsync();        
        Task<PaginationDM<DeliveryModeDto>> GetListAsync(string? search="",int offset = 0, int limit = 10, string sort="Name", string order="asc");
        Task<DeliveryModeDto> GetAsync(Guid Id);

        Task CreateAsync(CreateDeliveryModeDto deliveryModeDto);

        Task UpdateAsync(Guid Id, CreateDeliveryModeDto deliveryModeDto);

        Task DeleteAsync(Guid Id);
    }

    public class PaginationDM<T>
    {
        public int Total { get; set; }
        public ICollection<T> Items { get; set; } = new List<T>();
    }
}
