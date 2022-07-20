using Curso.ComercioElectronico.Dominio.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Dominio.Entities
{
    public class DeliveryMode:BaseBusinessEntity
    {        
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
