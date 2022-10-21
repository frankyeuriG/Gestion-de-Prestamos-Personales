using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace GestionPrestamos2022.Models
{
    public class Personas{

        [Key]
        public int PersonaId {get; set;}

       [Required(ErrorMessage ="Es obligatorio introducir el nombre")]
        public string? Nombre { get; set; }

        [RegularExpression("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$", ErrorMessage = "Debe Indicar un Email Valido")]
        public string? Email { get; set; }
        
         [Required(ErrorMessage ="Es obligatorio la Direccion")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "Complete este Campo")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime? FechaNacimiento { get; set; }

        public string? Telefono {get; set;}

        public int OcupacionId{get; set;}

        public double Balance{get; set;}
    }

  
}