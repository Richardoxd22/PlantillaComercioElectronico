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
    public class OrderAppService : IOrderAppService
    {
        private readonly IGenericRepository<Order> repository;

        public OrderAppService(IGenericRepository<Order> repository)
        {
            this.repository = repository;
        }

        public async Task CreateAsync(CreateOrderDto orderDto)
        {
            var order = new Order {
                Id = Guid.NewGuid(),                
                DeliveryModeId=orderDto.DeliveryModeId,
                Subtotal= orderDto.Subtotal,
                Total=orderDto.Total,                
                CreationDate = DateTime.Now
            };
            await repository.CreateAsync(order);

        }
        public async Task DeleteAsync(Guid Id)
        {
            var order = await repository.GetAsync(Id);
            if (order == null)
            {
                throw new NotFoundException($"El codigo de la marca que esta eliminando no existe {Id}");
            }
            order.IsDeleted = true;
            order.ModifiedDate = DateTime.Now;
            await repository.UpdateAsync(order);
        }

        public async Task<ICollection<OrderDto>> GetAllAsync()
        {
            var query = repository.GetQueryable();

            //Filtrando los no eliminados
            query = query.Where(x => x.IsDeleted == false);

            var result = query.Select(x=> new OrderDto
            {
                Id = x.Id,
                DeliveryModeId=x.DeliveryModeId,
                Subtotal = x.Subtotal,
                Total = x.Total,                
            });

            return await result.ToListAsync();
        }
                
        public async Task<OrderDto> GetAsync(Guid Id)
        {
            var query = repository.GetQueryable();

            //Filtrando los no eliminados
            query = query.Where(x => x.IsDeleted == false);

            //Filtrando la informacion por el Id
            query = query.Where(x => x.Id == Id);

            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<Order, OrderDto>()
            .ForMember(p => p.DeliveryMode, x => x.MapFrom(org => org.DeliveryMode.Id)));
            

            var resultquery = query.ProjectTo<OrderDto>(configuration);
            return await resultquery.SingleOrDefaultAsync();
        }        
        public async Task UpdateAsync(Guid Id, CreateOrderDto orderDto)
        {
            var order = await repository.GetAsync(Id);
            if (order == null)
            {
                throw new NotFoundException($"El codigo de la marca que esta eliminando no existe {order.Id}");
            }
            order.DeliveryModeId = orderDto.DeliveryModeId;
            order.Subtotal = orderDto.Subtotal;
            order.Total = orderDto.Total;            
            order.ModifiedDate = DateTime.Now;
        
            await repository.UpdateAsync(order);
        }
    }
}
