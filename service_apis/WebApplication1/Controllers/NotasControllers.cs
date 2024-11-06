using Aplicacion.General;
using Entidad.General;
using Infraestructura;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using service_apis.Controllers.General;

namespace service_apis.Controllers.General
{
    [ApiController]
    [Route("api/sistema/notas")]
    public class NotasControllers : ControllerBase
    {
        [HttpGet("lista")]
        public async Task<IActionResult> ReturnListaNotas()
        {

            AlumnosControllers alumnosController = new AlumnosControllers();
            ListaNotasControllers listNotasController = new ListaNotasControllers();
            MateriasControllers materiasController = new MateriasControllers();


            var listaNotas = new List<Ap_De_Notas>();
            using (Conexion db = ConexionDB.Connection())
            {
                listaNotas = db.NotasConexion.
               
                   Where(x => x.ACTIVO == true).
                   Select(x => new Ap_De_Notas
                   {
                       ID = x.ID,
                       ACTIVO = x.ACTIVO,
                       CALIFICACION = x.CALIFICACION,
                       ID_ALUMNOS = x.ID_ALUMNOS,
                       ID_LISTA_NOTAS = x.ID_LISTA_NOTAS,
                       ID_MATERIA = x.ID_MATERIA,
                       alumnos = alumnosController.ReturnListaAlumnosId(x.ID_ALUMNOS),
                       materias = materiasController.ReturnListaMateriasId(x.ID_MATERIA),
                       notas = listNotasController.ReturnListaNotasId(x.ID_LISTA_NOTAS)
                   })
                
                   .OrderBy(x => x.CALIFICACION)
                   
                   .ToList();

            }

            return await Task.Run(() => { return Ok(listaNotas); });
        }


        [HttpGet("lista_alumno")]
        public async Task<IActionResult> ReturnListaNotasAlumno(int idAlumno)
        {

            AlumnosControllers alumnosController = new AlumnosControllers();
            ListaNotasControllers listNotasController = new ListaNotasControllers();
            MateriasControllers materiasController = new MateriasControllers();


            var listaNotas = new List<Ap_De_Notas>();
            using (Conexion db = ConexionDB.Connection())
            {
                listaNotas = db.NotasConexion.

                   Where(x => x.ACTIVO == true).
                   Select(x => new Ap_De_Notas
                   {
                       ID = x.ID,
                       ACTIVO = x.ACTIVO,
                       CALIFICACION = x.CALIFICACION,
                       ID_ALUMNOS = x.ID_ALUMNOS,
                       ID_LISTA_NOTAS = x.ID_LISTA_NOTAS,
                       ID_MATERIA = x.ID_MATERIA,
                       alumnos = alumnosController.ReturnListaAlumnosId(x.ID_ALUMNOS),
                       materias = materiasController.ReturnListaMateriasId(x.ID_MATERIA),
                       notas = listNotasController.ReturnListaNotasId(x.ID_LISTA_NOTAS)
                   })
                   .Where(x => x.ID_ALUMNOS == idAlumno)

                   .OrderBy(x => x.CALIFICACION)

                   .ToList();

            }

            return await Task.Run(() => { return Ok(listaNotas); });
        }


        [HttpPost("insert")]
        public async Task<IActionResult> ReturnDatosPersonalesAcro(En_De_Notas en_De_Notas)
        {
            try
            {
                int result = -1;
                using (Conexion db = ConexionDB.Connection())
                {
                    db.NotasConexion.Add(en_De_Notas);
                    result = 1;
                    db.SaveChanges();
                }

                return await Task.Run(() => { return Ok(result); });
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE KEY"))
                {
                    return await Task.Run(() => { return Ok(2); });
                }
                return Problem(ex.Message);
            }
        }


        [HttpPost("update")]
        public async Task<IActionResult> UpdateDatosPersonalesByIndetificador(En_De_Notas en_De_Notas)
        { 
            try
            {
                int result = -1;


                using (Conexion db = ConexionDB.Connection())
                {
                    var lista_en_De_Notas = new List<Ap_De_Notas>();

                    lista_en_De_Notas = db.NotasConexion.
                           Where(x => x.ID == en_De_Notas.ID).
                           Select(x => new Ap_De_Notas
                           {
                               ID = x.ID,
                           }).ToList();



                    if (lista_en_De_Notas.Count > 0)
                    {
                        db.NotasConexion.Update(en_De_Notas);
                        db.SaveChanges();
                        result = 1;
                    }

                }
                return await Task.Run(() => { return Ok(result); });
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE KEY"))
                {
                    return await Task.Run(() => { return Ok(2); });
                }
                return Problem(ex.Message);
            }
        }


        [HttpPost("delete")]

        public async Task<IActionResult> deleteDatosPersonalesByIndetificador(En_De_Notas en_De_Notas)
        {
            try
            {
                int result = -1;


                using (Conexion db = ConexionDB.Connection())
                {
                    var lista_en_De_Notas = new List<Ap_De_Notas>();

                    lista_en_De_Notas = db.NotasConexion.
                           Where(x => x.ID == en_De_Notas.ID).
                           Select(x => new Ap_De_Notas
                           {
                               ID = x.ID,
                           }).ToList();



                    if (lista_en_De_Notas.Count > 0)
                    {

                        //en_De_Notas.ACTIVO = false;

                        db.NotasConexion.Update(en_De_Notas);
                        db.SaveChanges();
                        result = 1;
                    }

                }
                return await Task.Run(() => { return Ok(result); });
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE KEY"))
                {
                    return await Task.Run(() => { return Ok(2); });
                }
                return Problem(ex.Message);
            }
        }


    }
}
