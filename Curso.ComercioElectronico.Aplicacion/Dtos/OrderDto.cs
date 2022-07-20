namespace Curso.ComercioElectronico.Aplicacion.Dtos
{
    public class OrderDto
    {                
        public Guid Id { get; set; }
        public Guid DeliveryModeId { get; set; }
        public string DeliveryMode { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
    }
}