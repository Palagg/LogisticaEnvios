using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticaEnvios.Models
{
    public class Puerto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PuertoID { get; set; }

        [Required(ErrorMessage = "El nombre del puerto es obligatorio.")]
        [StringLength(20, ErrorMessage = "El nombre del puerto no puede exceder los 20 caracteres.")]
        public string Nombre{ get; set; }
        
        [Required(ErrorMessage = "La ubicación del puerto es obligatorio.")]
        [StringLength(50, ErrorMessage = "La ubicación del puerto no puede exceder los 50 caracteres.")]
        public string Ubicacion { get; set; }
    }
}