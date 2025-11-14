using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticaEnvios.Models
{
    public class EnvioTerrestre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnvioTerrestreID { get; set; }

        [Required(ErrorMessage = "Se requiere la placa del vehículo.")]
        [StringLength(6, ErrorMessage = "La placa del vehículo debe tener como máximo 6 caracteres.")]
        public string PlacaVehiculo { get; set; }

        [ForeignKey("BodegaEntregaID")]
        [Required(ErrorMessage = "Se requiere la ID de la bodega de entrega.")]
        public int BodegaEntregaID { get; set; }

        [ForeignKey("PlanID")]
        [Required(ErrorMessage = "Se requiere la ID del plan de entrega.")]
        public int PlanID { get; set; }
    }
}
