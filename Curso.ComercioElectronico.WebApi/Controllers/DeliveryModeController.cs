using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace Curso.ComercioElectronico.WebApi.Controllers
{    
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryModeController : ControllerBase, IDeliveryModeAppService
    {
        private readonly IDeliveryModeAppService deliveryModeAppService;

        public DeliveryModeController(IDeliveryModeAppService deliveryModeAppService)
        {
            this.deliveryModeAppService = deliveryModeAppService;
        }        
        [HttpGet]
        public async Task<ICollection<DeliveryModeDto>> GetAllAsync()
        {
            return await deliveryModeAppService.GetAllAsync();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{Id}")]
        public async Task<DeliveryModeDto> GetAsync(Guid Id)
        {
            return await deliveryModeAppService.GetAsync(Id);
        }

        [HttpPost]
        public async Task CreateAsync(CreateDeliveryModeDto deliveryModeDto)
        {
            await deliveryModeAppService.CreateAsync(deliveryModeDto);
        }

        [HttpPut]
        public async Task UpdateAsync(Guid Id, CreateDeliveryModeDto deliveryModeDto)
        {
            await deliveryModeAppService.UpdateAsync(Id, deliveryModeDto);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid Id)
        {
            await deliveryModeAppService.DeleteAsync(Id);
        }

        //enrutamiento para ir a la paginacion con el liminte n y probar con el offset             
        [HttpGet("lista")]
        public async Task<PaginationDM<DeliveryModeDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Name", string order = "asc")
        {
            return await deliveryModeAppService.GetListAsync(search,offset,limit,sort,order);
        }                      
    }
}
