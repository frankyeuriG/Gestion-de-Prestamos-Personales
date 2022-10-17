
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace GestionPrestamos2022{

    public class Pagos{

        [Key]
        public int PagoId {get; set;}

        [Required(ErrorMessage ="Requerida")]
        public DateTime Fecha {get; set;}

        [Required(ErrorMessage ="La Persona es Obligatoria")]
        public int PersonaId{get; set;}

        public string? Concepto{get; set;}

        public double Monto{get; set;}
        
        [ForeignKey("PagoId")]
         public virtual  List<PagosDetalle> PagosDetalle {get; set;} = new List<PagosDetalle>();

    }

    public class PagosDetalle{

        [Key]
        public int Id {get; set;}

        public int PagoId{get; set;}

        [Required(ErrorMessage ="Obligatorio")]
        public int PrestamoId{get; set;}

        [Range(minimum:100 , maximum:1000000000000, ErrorMessage ="El minimo a pagar es 100")]
        public double ValorPagado {get; set;}

    }

}