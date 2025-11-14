using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticaEnvios.Models
{
    public class Bodega
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BodegaID { get; set; }

        [Required(ErrorMessage = "La ubicación de la bodega es obligatoria.")]
        [StringLength(50, ErrorMessage = "La ubicación de la bodega no puede exceder los 50 caracteres.")]
        public string Ubicacion { get; set; }

        [Required(ErrorMessage = "La capacidad de la bodega es obligatoria.")]
        public int Capacidad { get; set; }
    }
}