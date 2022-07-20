using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public decimal Price { get; set; }

        public string ProductType { get; set; }

        public string Brand { get; set; }
        public int NumStock { get; set; }

    }
}
