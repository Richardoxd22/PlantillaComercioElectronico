using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Curso.ComercioElectronico.Dominio.Entities.Base
{
    public class BaseBusinessEntity : BaseEntity
    {
        public Guid Id { get; set; }
    }
}
