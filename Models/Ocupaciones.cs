using System.ComponentModel.DataAnnotations;

namespace GestionPrestamos2022.Models 
{ 
    public class Ocupaciones
    {
        [Key]
        public int OcupacionId{ get; set;}

        [Required(ErrorMessage = "La Descripcion es Obligatoria")]
        public string? Descripcion { get; set;}
    
        [Range(minimum:9000, maximum: 5000000, ErrorMessage = "Su salario debe ser mayor de 9,000 y Menor que 5,000,000" )]
        public float Salario{get; set;}
    }
 }