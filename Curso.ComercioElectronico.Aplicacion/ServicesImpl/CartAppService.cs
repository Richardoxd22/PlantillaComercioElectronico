using AutoMapper;
using AutoMapper.QueryableExtensions;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Aplicacion.ExcepcionCustom;
using Curso.ComercioElectronico.Aplicacion.Services;
using Curso.ComercioElectronico.Dominio.Entities;
using Curso.ComercioElectronico.Dominio.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.ServicesImpl
{
    public class CartAppService : ICartAppService
    {
        private readonly IGenericRepository<Cart> repository;
        private readonly IValidator<CreateCartDto> validator;

        public CartAppService(IGenericRepository<Cart> repository, IValidator<CreateCartDto>validator)
        {
            this.validator = validator;
            this.repository = repository;
        }

        public async Task CreateAsync(CreateCartDto cartDto)
        {
            var cart = new Cart {        
                Id = Guid.NewGuid(),
                DeliveryModeId = cartDto.DeliveryModeId,
                ProductId = cartDto.ProductId,
                CartResult = cartDto.CartResult,
                CantProduct =cartDto.CantProduct,
                Stock = cartDto.Stock,
                Price = cartDto.Price,                
                CreationDate = DateTime.Now
            };
            await repository.CreateAsync(cart);            
        }
        public async Task DeleteAsync(Guid Id)
        {
            var cart = await repository.GetAsync(Id);
            if (cart == null)
            {
                throw new NotFoundException($"El codigo del carrito que esta eliminando no existe {Id}");
            }
            cart.IsDeleted = true;
            cart.ModifiedDate = DateTime.Now;
            await repository.UpdateAsync(cart);
        }

        public async Task<ICollection<CartDto>> GetAllAsync()
        {
            var query = repository.GetQueryable();            
            query = query.Where(x => x.IsDeleted == false);            
            var result = query.Select(x=> new CartDto
            {                
                Id=x.Id,
                DeliveryModeId = x.DeliveryModeId,
                ProductId = x.ProductId,  
                CantProduct=x.CantProduct,
                CartResult = (x.CantProduct * x.Product.Price),
                Stock = x.Stock,
                Price = x.Product.Price,   
                CantStock=x.Product.NumStock,
                Name = x.Product.Name,                
            });                     

            return await result.ToListAsync();
        }
        
        public async Task<CartDto> GetAsync(Guid Id)
        {
            var query = repository.GetQueryable();

            //Filtrando los no eliminados
            query = query.Where(x => x.IsDeleted == false);

            //Filtrando la informacion por el Id
            query = query.Where(x => x.Id == Id);
           
            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<Cart, CartDto>()
            .ForMember(p => p.ProductId, x => x.MapFrom(org => org.Product.Id))
            .ForMember(p => p.Name, x => x.MapFrom(org => org.Product.Name))
            .ForMember(p => p.Price, x => x.MapFrom(org => org.Product.Price))
            .ForMember(p => p.CantProduct, x => x.MapFrom(org => org.CantProduct))
            .ForMember(p => p.CantStock, x => x.MapFrom(org => org.Product.NumStock))
            .ForMember(p => p.CartResult, x => x.MapFrom(org => (org.CantProduct * org.Product.Price)))
            .ForMember(p => p.DeliveryModeId, x => x.MapFrom(org => org.DeliveryMode.Id)));
            

            var resultquery = query.ProjectTo<CartDto>(configuration);
            return await resultquery.SingleOrDefaultAsync();
        }        
        public async Task UpdateAsync(Guid Id, CreateCartDto cartDto)
        {
            var cart = await repository.GetAsync(Id);
            if (cart == null)
            {
                throw new NotFoundException($"El codigo de la marca que esta eliminando no existe {cart.Id}");
            }                     
            cart.DeliveryModeId = cartDto.DeliveryModeId;
            cart.ProductId = cartDto.ProductId;
            cart.CantProduct = cartDto.CantProduct;
            cart.CartResult = cartDto.CartResult;
            cart.Stock = cartDto.Stock;
            cart.Price = cartDto.Price;
            cart.ModifiedDate = DateTime.Now;
        
            await repository.UpdateAsync(cart);
        }

        public async Task<CartPagination<CartDto>> GetListAsync(string? search = "", int offset = 0, int limit = 10, string sort = "Price", string order = "asc")
        {

            var query = repository.GetQueryable();

            //Filtrando los no eliminados
            query = query.Where(x => x.IsDeleted == false);

            if (!string.IsNullOrEmpty(search))
            {
                //filtro x multiples campos
                query = query.Where(x => x.Product.Name.ToUpper().Contains(search));
            }
            else
                throw new NotFoundException($"No se encontraron semenjanzas");


            var total = await query.CountAsync();            
            query = query
                .Skip(offset)
                .Take(limit);
            
            if (!string.IsNullOrEmpty(sort))
            {

                switch (sort.ToUpper())
                {
                    case "STOCK":
                        query = query.OrderBy(x => x.Stock);
                        break;
                    case "PRICE":
                        query = query.OrderBy(x => x.Price);
                        break;
                    case "CARTRESULT":
                        query = query.OrderBy(x => x.CartResult);
                        break;
                    default:
                        throw new ArgumentException($"el parametro sort {sort} no es soportado");
                }
            }

            var queryDto = query.Select(x => new CartDto
            {
                Id = x.Id,
                DeliveryModeId = x.DeliveryModeId,
                ProductId = x.ProductId,
                CantProduct = x.CantProduct,
                CartResult = (x.CantProduct * x.Product.Price),
                Stock = x.Stock,
                Price = x.Product.Price,
                Name= x.Product.Name,
                CantStock= x.Product.NumStock,
            });         
            var item = await queryDto.ToListAsync();            
            var result = new CartPagination<CartDto>();
            
            result.Total = total;            
            result.Items = item;
            return result;
        }
    }
}
