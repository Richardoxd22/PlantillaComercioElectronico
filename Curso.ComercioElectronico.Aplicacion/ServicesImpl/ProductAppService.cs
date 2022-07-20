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
    public class ProductAppService : IProductAppService
    {
        private readonly IGenericRepository<Product> repository;
        private readonly IValidator<CreateCartDto> validator;

        public ProductAppService(IGenericRepository<Product> repository, IValidator<CreateCartDto>validator)
        {
            this.validator = validator;
            this.repository = repository;
        }

        public async Task CreateAsync(CreateProductDto productDto)
        {
            
            var product = new Product 
            {                
                Id = Guid.NewGuid(),
                Name = productDto.Name,
                Description = productDto.Description,
                BrandId = productDto.BrandId,
                ProductTypeId = productDto.ProductTypeId,
                Price = productDto.Price,
                NumStock = productDto.NumStock,
                CreationDate = DateTime.Now                
            };
            if (product == null)
            {
                throw new NotFoundException($"No esta ingresando correcto los datos");
            }
            await repository.CreateAsync(product);

        }
        public async Task DeleteAsync(Guid Id)
        {
            var product = await repository.GetAsync(Id);
            if (product == null)
            {
                throw new NotFoundException($"El codigo del producto que esta eliminando no existe {Id}");
            }
            product.IsDeleted = true;
            product.ModifiedDate = DateTime.Now;
            await repository.UpdateAsync(product);
        }

        public async Task<ICollection<ProductDto>> GetAllAsync()
        {
            var query = repository.GetQueryable();

            //Filtrando los no eliminados
            query = query.Where(x => x.IsDeleted == false);

            var result = query.Select(x=> new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Brand = x.Brand.Description,
                NumStock = x.NumStock,
                ProductType = x.ProductType.Description
            });

            return await result.ToListAsync();
        }        
        public async Task<ResultPagination<ProductDto>> GetListAsync(string? search = "Name||description", int offset = 0, int limit = 10, string sort = "Name", string order = "asc")
        {

            var query = repository.GetQueryable();

            //Filtrando los no eliminados
            query = query.Where(x => x.IsDeleted == false);

            //Search 
            if (!string.IsNullOrEmpty(search))
            {
                //filtro x multiples campos
                query = query.Where(x => x.Name.ToUpper().Contains(search) || (x.Description.ToLower().Contains(search)));                                
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
                    case "NAME":
                        query = query.OrderBy(x => x.Name);
                        break;
                    case "PRICE":
                        query = query.OrderBy(x => x.Price);
                        break;
                    case "DESCRIPTION":
                        query = query.OrderBy(x => x.Description);
                        break;
                    default:
                        throw new ArgumentException($"el parametro sort {sort} no es soportado");
                }
            }

            var queryDto = query.Select(x => new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Brand = x.Brand.Description,
                NumStock=x.NumStock,
                ProductType = x.ProductType.Description
            });

            var item = await queryDto.ToListAsync();
            var result = new ResultPagination<ProductDto>();
            result.Total = total;
            result.Items = item;
            return result;           
        }

        public async Task<ProductDto> GetAsync(Guid Id)
        {
            var query = repository.GetQueryable();

            //Filtrando los no eliminados
            query = query.Where(x => x.IsDeleted == false);

            //Filtrando la informacion por el Id
            query = query.Where(x => x.Id == Id);            
            var configuration = new MapperConfiguration(cfg => cfg.CreateProjection<Product, ProductDto>()
            .ForMember(p => p.Brand, x => x.MapFrom(org => org.Brand.Description))
            .ForMember(p => p.NumStock, x => x.MapFrom(org => org.NumStock))
            .ForMember(p => p.ProductType, x => x.MapFrom(org => org.ProductType.Description)));

            var resultquery = query.ProjectTo<ProductDto>(configuration);
            return await resultquery.SingleOrDefaultAsync();
        }        
        public async Task UpdateAsync(Guid Id, CreateProductDto productDto)
        {
            var product = await repository.GetAsync(Id);
            if (product == null)
            {
                throw new NotFoundException($"El codigo de la Producto que esta eliminando no existe {product.Id}");
            }
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.NumStock = productDto.NumStock;
            product.Price = productDto.Price;
            product.BrandId = productDto.BrandId;
            product.ProductTypeId = productDto.ProductTypeId;
            product.ModifiedDate = DateTime.Now;
        
            await repository.UpdateAsync(product);
        }
    }
}
