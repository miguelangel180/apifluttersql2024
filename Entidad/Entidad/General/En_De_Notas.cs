using Aplicacion.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.General
{
    [Table("NOTAS", Schema = "dbo")]
    public partial class  En_De_Notas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "ACTIVO")]
        public bool ACTIVO { get; set; }


        [Required]
        [Display(Name = "CALIFICACION")]
        public decimal CALIFICACION { get; set; }



        [Required]
        [Display(Name = "ID_ALUMNOS")]
        public int ID_ALUMNOS { get; set; }


        [Required]
        [Display(Name = "ID_MATERIA")]
        public int ID_MATERIA { get; set; }


        [Required]
        [Display(Name = "ID_LISTA_NOTAS")]
        public int ID_LISTA_NOTAS { get; set; }


        /*[Required]
        [Display(Name = "ID_ALUMNOS")]
        public Ap_De_Alumnos alumnos { get; set; }


        [Required]
        [Display(Name = "ID_MATERIA")]
        public Ap_De_Materias materia { get; set; }


        [Required]
        [Display(Name = "ID_LISTA_NOTAS")]
        public  Ap_De_Lista_Notas notas { get; set; }*/

            
    }
}
