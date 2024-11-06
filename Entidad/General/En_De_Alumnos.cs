using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad.General
{
    [Table("ALUMNOS", Schema = "dbo")]
    public partial class En_De_Alumnos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Display(Name = "ID")]
        public int ID { get; set; }

        [StringLength(10, ErrorMessage = "El campo [0] no puede tener mas de [1] caracteres", MinimumLength = 0)]
        [Required]
        [Display(Name = "CI")]

        public string CI { get; set; }


        [StringLength(100, ErrorMessage = "El campo [0] no puede tener mas de [1] caracteres", MinimumLength = 0)]
        [Required]
        [Display(Name = "NOMBRE")]

        public string NOMBRE { get; set; }

    }
}
