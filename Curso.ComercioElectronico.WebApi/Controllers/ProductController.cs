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
    public class ProductController : ControllerBase, IProductAppService
    {
        private readonly IProductAppService productAppService;

        public ProductController(IProductAppService productAppService)
        {
            this.productAppService = productAppService;
        }
        
        [HttpGet]
        public async Task<ICollection<ProductDto>> GetAllAsync()
        {
            return await productAppService.GetAllAsync();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{Id}")]
        public async Task<ProductDto> GetAsync(Guid Id)
        {
            return await productAppService.GetAsync(Id);
        }

        [HttpPost]
        public async Task CreateAsync(CreateProductDto productDto)
        {
            await productAppService.CreateAsync(productDto);
        }

        [HttpPut]
        public async Task UpdateAsync(Guid Id, CreateProductDto productDto)
        {
            await productAppService.UpdateAsync(Id, productDto);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid Id)
        {
            await productAppService.DeleteAsync(Id);
        }

        //enrutamiento para ir a la paginacion con el liminte n y probar con el offset             
        [HttpGet("Paginacion")]
        public async Task<ResultPagination<ProductDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Name", string order = "asc")
        {
            return await productAppService.GetListAsync(search,offset,limit,sort,order);
        }                      
    }
}
