using Aplicacion.General;
using Infraestructura;
using Microsoft.AspNetCore.Mvc;

namespace service_apis.Controllers.General
{
    [ApiController]
    [Route("api/sistema/alumnos")]
    public class AlumnosControllers : ControllerBase
    {
        [HttpGet("lista")]
        public async Task<IActionResult> ReturnListaAlumnos()
        {
            var listaAlumnos = new List<Ap_De_Alumnos>();
            using (Conexion db = ConexionDB.Connection())
            {
                listaAlumnos = db.AlumnosConexion.
                   Select(x => new Ap_De_Alumnos
                   {
                       ID = x.ID,
                       NOMBRE = x.NOMBRE,
                       CI = x.CI
                   })
                   .OrderBy(x => x.NOMBRE)
                   .ToList();

            }

            return await Task.Run(() => { return Ok(listaAlumnos); });
        }


        protected internal Ap_De_Alumnos ReturnListaAlumnosId(int id)
        {
            var listaAlumnos = new List<Ap_De_Alumnos>();
            using (Conexion db = ConexionDB.Connection())
            {
                listaAlumnos = db.AlumnosConexion.
                   Select(x => new Ap_De_Alumnos
                   {
                       ID = x.ID,
                       NOMBRE = x.NOMBRE,
                       CI = x.CI
                   })
                   .Where(x => x.ID == id)
                   .OrderBy(x => x.NOMBRE)
                   .ToList();

            }

            Ap_De_Alumnos result = new Ap_De_Alumnos();
            if(listaAlumnos.Count == 1)
            {
                result = listaAlumnos[0];
            }
               
            return result;
        }


    }
}
