using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Services
{
    public interface IBrandAppService
    {

        Task<ICollection<BrandDto>> GetAllAsync();

        Task<BrandDto> GetAsync(string code);

        Task CreateAsync(CreateBrandDto brandDto);

        Task UpdateAsync(CreateBrandDto brandDto);
        Task DeleteAsync(string code);
    }
}
