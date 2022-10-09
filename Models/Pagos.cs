
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace GestionPrestamos2022{

    public class Pagos{

        [Key]
        public int PagosId {get; set;}

        [Required(ErrorMessage ="Requerida")]
        public DateTime Fecha {get; set;}

        [Required(ErrorMessage ="PersonaId obligatorio")]
        public int PersonaId{get; set;}

        [Required(ErrorMessage ="Concepto Obligatorio")]
        public string? Concepto{get; set;}

        [Range(minimum:1 , maximum: 100000000000000 ,ErrorMessage ="Limited")]
        public double Monto{get; set;}

        
        [ForeignKey("Id")]
         public virtual  List<PagosDetalle> PagosDetalle {get; set;} = new List<PagosDetalle>();

    }

    public class PagosDetalle{

        [Key]
        public int Id {get; set;}

        [Required(ErrorMessage ="PagoId Obligatorio")]
        public int PagosId{get; set;}

        [Required(ErrorMessage ="Obligatorio")]
        public int PrestamosId{get; set;}

        public double ValorPagado {get; set;}

    }

}