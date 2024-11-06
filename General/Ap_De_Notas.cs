using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.General
{
    public class Ap_De_Notas
    {
        public int ID { get; set; }
        public bool ACTIVO { get; set; }
        public decimal CALIFICACION { get; set; }

        public int ID_ALUMNOS { get; set; }
        public int ID_MATERIA { get; set; }
        public int ID_LISTA_NOTAS { get; set; }

        public Ap_De_Alumnos alumnos {get; set;}
        public Ap_De_Lista_Notas notas { get; set; }
        public Ap_De_Materias materias { get; set; }

    }
}
