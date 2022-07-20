using Curso.ComercioElectronico.Aplicacion.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Validators
{
    public class CreateBrandDtoValidor : AbstractValidator<CreateBrandDto>
    {
        string patter = "^[0-9a-zA-Z-]+$";
        public CreateBrandDtoValidor()
        {
            RuleFor(x => x.Code).MaximumLength(4).NotNull();
            RuleFor(x => x.Description).NotNull()
                .WithMessage("La descripcion no puede estar vacia");
            RuleFor(x => x.Code).Matches(patter).WithMessage("no esta al formato del codigo");           
            //regla personalizada
            RuleFor(bD => bD.Description).Must(x => ValidarDescripcion(x))
                .WithMessage("La descripcion va a contener de 10 a 30 caracteres");
        }
        public bool ValidarDescripcion(string desc)
        {
            if(desc.Length<10 && desc.Length > 30)
                return false;
            return true;
        }        
    }
    
}
