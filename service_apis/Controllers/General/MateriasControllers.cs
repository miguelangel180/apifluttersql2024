using Aplicacion.General;
using Infraestructura;
using Microsoft.AspNetCore.Mvc;

namespace service_apis.Controllers.General
{
    [ApiController]
    [Route("api/sistema/materias")]
    public class MateriasControllers : ControllerBase
    {
        [HttpGet("lista")]
        public async Task<IActionResult> ReturnListaMaterias()
        {
            var listaMaterias = new List<Ap_De_Materias>();
            using (Conexion db = ConexionDB.Connection())
            {
                listaMaterias = db.MateriasConexion.
                   Select(x => new Ap_De_Materias
                   {
                       ID = x.ID,
                       NOMBRE = x.NOMBRE,
                       DESCRIPCION = x.DESCRIPCION
                   })
                   .OrderBy(x => x.NOMBRE)
                   .ToList();

            }

            return await Task.Run(() => { return Ok(listaMaterias); });
        }

        protected internal Ap_De_Materias ReturnListaMateriasId(int id)
        {
            var listaMaterias = new List<Ap_De_Materias>();
            using (Conexion db = ConexionDB.Connection())
            {
                listaMaterias = db.MateriasConexion.
                   Select(x => new Ap_De_Materias
                   {
                       ID = x.ID,
                       NOMBRE = x.NOMBRE,
                       DESCRIPCION = x.DESCRIPCION
                   })
                   .Where(x => x.ID == id)
                   .OrderBy(x => x.NOMBRE)
                   .ToList();

            }


            Ap_De_Materias ap_De_Materias = new Ap_De_Materias();
            if (listaMaterias.Count > 0)
            {
                ap_De_Materias = listaMaterias[0];
            }

            return ap_De_Materias;
        }
    }
}
