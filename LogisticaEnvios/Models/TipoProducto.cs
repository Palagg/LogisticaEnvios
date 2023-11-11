using System.ComponentModel.DataAnnotations;

namespace LogisticaEnvios.Models
{
    public class TipoProducto
    {
        [Key]
        public int TipoProductoID { get; set; }

        [Required(ErrorMessage = "El nombre del tipo de producto es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del tipo de producto no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(300, ErrorMessage = "La descripción del tipo de producto no puede exceder los 300 caracteres.")]
        public string Descripcion { get; set; }
    }
}
