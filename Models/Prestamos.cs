using System.ComponentModel.DataAnnotations;


namespace GestionPrestamos2022.Models
{
    public class Prestamos
    {
        [Key]
        public int PrestamoId { get; set; }

        [Required(ErrorMessage = "Complete este Campo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Fecha { get; set; } 

        [Required(ErrorMessage = "Complete este Campo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime Vence { get; set; }

        [Required(ErrorMessage = "Es obligatorio el Concepto")]
        public string? Concepto { get; set; }

        [Range(minimum:1000, maximum:100000000, ErrorMessage ="Prestamo minimo 1,000 y maximo 100,000,000")]
        public double Monto { get; set; }

        public double Balance { get; set; }

        [Required(ErrorMessage ="La persona es Obligatoria")]
        public int PersonaId { get; set; }
    }


}