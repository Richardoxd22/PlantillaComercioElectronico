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
    public class BrandController : ControllerBase, IBrandAppService
    {
        private readonly IBrandAppService brandAppService;

        public BrandController(IBrandAppService brandAppService)
        {
            this.brandAppService = brandAppService;
        }        
        [HttpGet]
        public async Task<ICollection<BrandDto>> GetAllAsync()
        {
            return await brandAppService.GetAllAsync();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{code}")]
        public async Task<BrandDto> GetAsync(string code)
        {
            return await brandAppService.GetAsync(code);
        }

        [HttpPost]
        public async Task CreateAsync(CreateBrandDto brandDto)
        {
            await brandAppService.CreateAsync(brandDto);
        }

        [HttpPut]
        public async Task UpdateAsync(CreateBrandDto brandDto)
        {
            await brandAppService.UpdateAsync(brandDto);
        }

        [HttpDelete("{code}")]
        public async Task DeleteAsync(string code)
        {
            await brandAppService.DeleteAsync(code);
        }
    }
}
