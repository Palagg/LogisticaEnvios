using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticaEnvios.Models
{
    public class EnvioMaritimo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnvioMaritimoID { get; set; }

        [Required(ErrorMessage = "El numero de la flota es obligatorio.")]
        [StringLength(8, ErrorMessage = "El numero de la flota no puede exceder los 8 caracteres.")]
        public string NumeroFlota { get; set; }

        [ForeignKey("Puerto")]
        [Required(ErrorMessage = "El ID del puerto de entrega es obligatorio.")]
        public int PuertoEntregaID { get; set; }

        [ForeignKey("PlanDeEntrega")]
        [Required(ErrorMessage = "El ID del Plan de entrega es obligatorio.")]
        public int PlanID { get; set; }
    }
}
