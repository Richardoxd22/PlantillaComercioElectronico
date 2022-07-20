using Curso.ComercioElectronico.Aplicacion.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Validators
{
    public class CreateProductDtoValidor : AbstractValidator<CreateProductDto>
    {        
        public CreateProductDtoValidor()
        {
            RuleFor(x => x.Price).NotNull();
            RuleFor(x => x.Name).MaximumLength(20)
                .WithMessage("La cantidad no puede estar vacia");
            RuleFor(x => x.BrandId).NotNull();
            RuleFor(x => x.ProductTypeId).NotNull();
            RuleFor(x => x.Description).MaximumLength(200);
            RuleFor(x => x.NumStock).GreaterThanOrEqualTo(0);
        }        
    }    
}
