using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }        
        public Guid OrderId { get; set; }        
        //requerido
        public int CantProduct { get; set; }

        public decimal Total { get; set; }
    }
}
