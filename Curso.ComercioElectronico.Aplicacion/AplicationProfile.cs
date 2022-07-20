using AutoMapper;
using Curso.ComercioElectronico.Aplicacion.Dtos;
using Curso.ComercioElectronico.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion
{
    public class AplicationProfile : Profile
    {
        public AplicationProfile() 
        { 
            //CreateMap<Origen , Destino>
            CreateMap<ProductDto,Product>();
            CreateMap<ProductTypeDto,ProductType>();
            CreateMap<BrandDto,Brand>(); 
            CreateMap<OrderDto,Order>(); 
            CreateMap<DeliveryModeDto,DeliveryModeDto>(); 
            CreateMap<CartDto,Cart>();                         
        }
    }
}
