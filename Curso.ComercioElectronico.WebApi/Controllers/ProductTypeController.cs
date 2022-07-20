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
    public class ProductTypeController : ControllerBase, IProductTypeAppService
    {
        private readonly IProductTypeAppService ProductTypeAppService;

        public ProductTypeController(IProductTypeAppService ProductTypeAppService)
        {
            this.ProductTypeAppService = ProductTypeAppService;
        }

        [HttpGet]
        public async Task<ICollection<ProductTypeDto>> GetAllAsync()
        {
            return await ProductTypeAppService.GetAllAsync();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{code}")]
        public async Task<ProductTypeDto> GetAsync(string code)
        {
            return await ProductTypeAppService.GetAsync(code);
        }

        [HttpPost]
        public async Task CreateAsync(CreateProductTypeDto productTypeDto)
        {
            await ProductTypeAppService.CreateAsync(productTypeDto);
        }

        [HttpPut]
        public async Task UpdateAsync(CreateProductTypeDto productTypeDto)
        {
            await ProductTypeAppService.UpdateAsync(productTypeDto);
        }

        [HttpDelete("{code}")]
        public async Task DeleteAsync(string code)
        {
            await ProductTypeAppService.DeleteAsync(code);
        }
    }
}
