using Curso.ComercioElectronico.Dominio.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Dominio.Entities
{
    public class ProductType : BaseCatalogEntity
    {
        public string Description { get; set; }

    }
}
