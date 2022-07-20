using Curso.ComercioElectronico.Dominio.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Dominio.Entities
{
    public class Cart : BaseEntity
    {
        public Guid Id { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid DeliveryModeId { get; set; }
        public DeliveryMode DeliveryMode { get; set; }
        public int CantProduct { get; set; }
        public decimal CartResult { get; set; }
        public int CantStock { get; set; }
        public bool Stock { get; set; }

    }
}
