using Curso.ComercioElectronico.Aplicacion.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Validators
{
    public class CreateCartDtoValidor : AbstractValidator<CreateCartDto>
    {        
        public CreateCartDtoValidor()
        {
            RuleFor(x => x.Price).NotNull();
            RuleFor(x => x.CantProduct).NotNull()
                .WithMessage("La cantidad no puede estar vacia");
            RuleFor(x => x.DeliveryModeId).NotNull();
            RuleFor(x => x.ProductId).NotNull();
            RuleFor(x => x.Stock).NotNull().WithMessage("Debe siempre estar con algun valor");            
        }        
    }    
}
