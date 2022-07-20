using AutoMapper;
using AutoMapper.QueryableExtensions;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.ExcepcionCustom;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Dominio.Entities;
using Curso.ComercioElectronico.Dominio.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.ServicesImpl
{
    public class DeliveryModeAppService : IDeliveryModeAppService
    {
        private readonly IGenericRepository<DeliveryMode> repository;

        public DeliveryModeAppService(IGenericRepository<DeliveryMode> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(CreateDeliveryModeDto orderDto)
        {
            var order = new DeliveryMode {
                Id = Guid.NewGuid(),                
                Name = orderDto.Name,
                Description = orderDto.Description,                
                CreationDate = DateTime.Now
            };
            await repository.CreateAsync(order);

        }
        public async Task DeleteAsync(Guid Id)
        {
            var order = await repository.GetAsync(Id);
            if (order == null)
            {
                throw new NotFoundException($"El codigo del delivery que esta eliminando no existe {Id}");
            }
            order.IsDeleted = true;
            order.ModifiedDate = DateTime.Now;
            await repository.UpdateAsync(order);
        }

        public async Task<ICollection<DeliveryModeDto>> GetAllAsync()
        {
            var query = repository.GetQueryable();

            //Filtrando los no eliminados
            query = query.Where(x => x.IsDeleted == false);

            var result = query.Select(x=> new DeliveryModeDto
            {
                Id = x.Id,
                Name=x.Name,
                Description=x.Description,
            });

            return await result.ToListAsync();
        }
                
        public async Task<DeliveryModeDto> GetAsync(Guid Id)
        {
            var query = repository.GetQueryable();

            //Filtrando los no eliminados
            query = query.Where(x => x.IsDeleted == false);

            //Filtrando la informacion por el Id
            query = query.Where(x => x.Id == Id);

            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<DeliveryMode, DeliveryModeDto>()
            .ForMember(p => p.Id, x => x.MapFrom(org => org.Id)));
            

            var resultquery = query.ProjectTo<DeliveryModeDto>(configuration);
            return await resultquery.SingleOrDefaultAsync();
        }        
        public async Task UpdateAsync(Guid Id, CreateDeliveryModeDto orderDto)
        {
            var order = await repository.GetAsync(Id);
            if (order == null)
            {
                throw new NotFoundException($"El codigo de la marca que esta eliminando no existe {order.Id}");
            }
            order.Name = orderDto.Name;
            order.Description = orderDto.Description;             
            order.ModifiedDate = DateTime.Now;
        
            await repository.UpdateAsync(order);
        }
        public async Task<PaginationDM<DeliveryModeDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Name", string order = "asc")
        {

            var query = repository.GetQueryable();

            //Filtrando los no eliminados
            query = query.Where(x => x.IsDeleted == false);

            //Search 
            if (!string.IsNullOrEmpty(search))
            {
                //filtro x multiples campos
                query = query.Where(
                    x => x.Name.ToUpper().Contains(search));
            }

            var total = await query.CountAsync();
            //2.Pagination
            query = query
                .Skip(offset)
                .Take(limit);

            //3Ordenamiento
            if (!string.IsNullOrEmpty(sort))
            {
                //soportar Nmae 
                //sort =>Name or price. Other throw exception
                switch (sort.ToUpper())
                {
                    case "NAME":
                        query = query.OrderBy(x => x.Name);
                        break;
                    case "DESCRIPTION":
                        query = query.OrderBy(x => x.Description);
                        break;
                    default:
                        throw new ArgumentException($"el parametro sort {sort} no es soportado");
                }
            }

            var queryDto = query.Select(x => new DeliveryModeDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,                
            });

            var item = await queryDto.ToListAsync();
            var result = new PaginationDM<DeliveryModeDto>();
            result.Total = total;
            result.Items = item;
            return result;           
        }
    }
}
