using System.ComponentModel.DataAnnotations;

namespace LogisticaEnvios.Models
{
    public class Cliente
    {
        [Key]
        [StringLength(15, ErrorMessage = "La cédula no puede exceder los 15 caracteres.")]
        public string Cedula { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre del cliente no puede exceder los 50 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(100, ErrorMessage = "La dirección del cliente no puede exceder los 100 caracteres.")]
        public string Direccion { get; set; }

        [StringLength(50, ErrorMessage = "El email del cliente no puede exceder los 50 caracteres.")]
        public string Email { get; set; }
    }
}
