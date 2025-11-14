using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticaEnvios.Models
{
    public class PlanDeEntrega
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlanID { get; set; }

        [ForeignKey("TipoProducto")]
        [Required(ErrorMessage = "TipoProducto es obligatorio")]
        public int TipoProductoID { get; set; }

        [Required(ErrorMessage = "Cantidad es obligatoria")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "Fecha de Registro es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; set; }

        [Required(ErrorMessage = "Fecha de Entrega es obligatoria")]
        [DataType(DataType.Date)]
        public DateTime FechaEntrega { get; set; }

        [Required(ErrorMessage = "Precio de Envío es obligatorio")]
        public decimal PrecioEnvio { get; set; }

        [Required(ErrorMessage = "Número de Guía es obligatorio")]
        public int NumeroGuia { get; set; }

        [ForeignKey("Cliente")]
        [Required(ErrorMessage = "La cedula del Cliente es obligatoria")]
        public string ClienteCedula { get; set; }
    }
}

