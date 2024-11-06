using Aplicacion.General;
using Infraestructura;
using Microsoft.AspNetCore.Mvc;

namespace service_apis.Controllers.General
{
    [ApiController]
    [Route("api/sistema/lista/notas")]
    public class ListaNotasControllers : ControllerBase
    {
        [HttpGet("lista_notas")]
        public async Task<IActionResult> ReturnListaNotas()
        {
            var listaNotas = new List<Ap_De_Lista_Notas>();
            using (Conexion db = ConexionDB.Connection())
            {
                listaNotas = db.ListaNotasConexion.
                   Select(x => new Ap_De_Lista_Notas
                   {
                       ID = x.ID,
                       NOMBRE = x.NOMBRE
                   })
                   .OrderBy(x => x.NOMBRE)
                   .ToList();

            }

            return await Task.Run(() => { return Ok(listaNotas); });
        }


         protected internal Ap_De_Lista_Notas ReturnListaNotasId(int id)
         {
             var listaNotas = new List<Ap_De_Lista_Notas>();
             using (Conexion db = ConexionDB.Connection())
             {
                 listaNotas = db.ListaNotasConexion.
                    Select(x => new Ap_De_Lista_Notas
                    {
                        ID = x.ID,
                        NOMBRE = x.NOMBRE
                    })
                    .Where(x => x.ID == id)
                    .OrderBy(x => x.NOMBRE)
                    .ToList();

             }

             Ap_De_Lista_Notas ap_De_Lista_Notas = new Ap_De_Lista_Notas();
             if (listaNotas.Count > 0)
             {
                 ap_De_Lista_Notas = listaNotas[0];
             }

             return ap_De_Lista_Notas;
         }
    }
}
