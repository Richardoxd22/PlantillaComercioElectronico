using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Dominio.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Curso.ComercioElectronico.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase, ICartAppService
    {
        private readonly ICartAppService CartAppService;

        public CartController(ICartAppService CartAppService)
        {
            this.CartAppService = CartAppService;
        }
        
        [HttpGet]
        public async Task<ICollection<CartDto>> GetAllAsync()
        {
            return await CartAppService.GetAllAsync();
        }
        [HttpGet("Paginacion")]
        public async Task<CartPagination<CartDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Price", string order = "asc")
        {
            return await CartAppService.GetListAsync( search,offset, limit, sort, order);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("{Id}")]
        public async Task<CartDto> GetAsync(Guid Id)
        {
            return await CartAppService.GetAsync(Id);
        }

        [HttpPost]
        public async Task CreateAsync(CreateCartDto cartDto)
        {
            await CartAppService.CreateAsync(cartDto);
        }

        [HttpPut]
        public async Task UpdateAsync(Guid Id, CreateCartDto cartDto)
        {
            await CartAppService.UpdateAsync(Id,cartDto);
        }
        
        [HttpDelete("{Id}")]
        public async Task DeleteAsync(Guid Id)
        {
            await CartAppService.DeleteAsync(Id);
        }
    }
}
