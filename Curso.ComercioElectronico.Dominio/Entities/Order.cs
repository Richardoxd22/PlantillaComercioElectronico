using Curso.ComercioElectronico.Dominio.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Dominio.Entities
{
    public class Order: BaseBusinessEntity
    {        
        public Guid DeliveryModeId { get; set; }
        public DeliveryMode DeliveryMode { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }        
    }
}
