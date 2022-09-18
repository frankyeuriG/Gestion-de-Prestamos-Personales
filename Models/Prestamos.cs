using System.ComponentModel.DataAnnotations;


namespace GestionPrestamos2022.Models
{
    public class Prestamo
    {
        [Key]
        public int PrestamosId { get; set; }

        public DateTime? FechaPrestamo { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Complete este Campo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? FechaVence { get; set; }

        [Required(ErrorMessage = "Es obligatorio el Concepto")]
        public string Concepto { get; set; }

        [Range(minimum: 1, maximum: 100000000000, ErrorMessage = "Monto prestamo")]
        public float Monto { get; set; }

        [Range(minimum: 0, maximum: 1000000000000, ErrorMessage = "Balance")]
        public float Balance { get; set; }

        public int PersonaId { get; set; }
    }


}