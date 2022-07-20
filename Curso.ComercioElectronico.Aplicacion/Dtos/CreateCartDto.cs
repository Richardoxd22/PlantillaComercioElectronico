using Curso.ComercioElectronico.Dominio.Entities;

namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class CreateCartDto
    {        
        public Guid ProductId { get; set; }
        public Guid DeliveryModeId { get; set; }
        
        public decimal Price { get; set; }                
        public int CantProduct { get; set; }

        public int CantStock { get; set; }
        public decimal CartResult { get; set; }
        public bool Stock { get; set; } = true;
    }
}