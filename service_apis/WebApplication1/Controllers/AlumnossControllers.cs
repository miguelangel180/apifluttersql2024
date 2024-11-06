using Aplicacion.General;
using Entidad.General;
using Infraestructura;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace service_apis.Controllers.General
{
    [ApiController]
    [Route("api/sistema/alumnos")]
    public class AlumnosControllers : ControllerBase
    {
        // Método GET para obtener la lista completa de alumnos activos
        [HttpGet("lista")]
        public async Task<IActionResult> ReturnListaAlumnos()
        {
            var listaAlumnos = new List<Ap_De_Alumnos>();
            using (Conexion db = ConexionDB.Connection())
            {
                listaAlumnos = db.AlumnosConexion
                    .Where(x => x.ACTIVO == true)  // Solo alumnos activos
                    .Select(x => new Ap_De_Alumnos
                    {
                        ID = x.ID,
                        NOMBRE = x.NOMBRE,
                        CI = x.CI,
                        ACTIVO = x.ACTIVO
                    })
                    .OrderBy(x => x.NOMBRE)
                    .ToList();
            }

            return await Task.Run(() => Ok(listaAlumnos));
        }

        // Método protegido para obtener un alumno por ID (usado internamente)
        protected internal Ap_De_Alumnos ReturnListaAlumnosId(int id)
        {
            var listaAlumnos = new List<Ap_De_Alumnos>();
            using (Conexion db = ConexionDB.Connection())
            {
                listaAlumnos = db.AlumnosConexion
                    .Where(x => x.ACTIVO == true && x.ID == id)  // Solo alumno activo y por ID
                    .Select(x => new Ap_De_Alumnos
                    {
                        ID = x.ID,
                        NOMBRE = x.NOMBRE,
                        CI = x.CI
                    })
                    .OrderBy(x => x.NOMBRE)
                    .ToList();
            }

            return listaAlumnos.FirstOrDefault();
        }

        // Método POST para insertar un nuevo alumno
        [HttpPost("insert")]
        public async Task<IActionResult> InsertAlumno([FromBody] En_De_Alumnos newAlumno)
        {
            try
            {
                using (Conexion db = ConexionDB.Connection())
                {
                    newAlumno.ACTIVO = true; // Asignamos el estado activo por defecto
                    db.AlumnosConexion.Add(newAlumno);
                    db.SaveChanges();
                }
                return await Task.Run(() => Ok(newAlumno));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Método POST para actualizar un alumno existente
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAlumno([FromBody] En_De_Alumnos updatedAlumno)
        {
            try
            {
                using (Conexion db = ConexionDB.Connection())
                {
                    var existingAlumno = db.AlumnosConexion.FirstOrDefault(x => x.ID == updatedAlumno.ID);
                    if (existingAlumno == null)
                    {
                        return NotFound("Alumno no encontrado");
                    }

                    // Actualizamos los campos necesarios
                    existingAlumno.CI = updatedAlumno.CI;
                    existingAlumno.NOMBRE = updatedAlumno.NOMBRE;
                    existingAlumno.ACTIVO = updatedAlumno.ACTIVO;
                    db.SaveChanges();
                }
                return await Task.Run(() => Ok(updatedAlumno));
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        // Método POST para cambiar el estado a inactivo (eliminar) usando CI
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteAlumno([FromBody] En_De_Alumnos alumno)
        {
            try
            {
                int result = -1;

                using (Conexion db = ConexionDB.Connection())
                {
                    // Obtenemos el alumno por su ID
                    var alumnoEncontrado = db.AlumnosConexion
                        .Where(x => x.ID == alumno.ID)
                        .Select(x => new Ap_De_Alumnos
                        {
                            ID = x.ID
                        }).ToList();

                    if (alumnoEncontrado.Count > 0)
                    {
                        // Cambiamos el estado a inactivo
                        alumno.ACTIVO = false;

                        // Actualizamos el registro con el estado cambiado
                        db.AlumnosConexion.Update(alumno);
                        db.SaveChanges();
                        result = 1;
                    }
                }
                return await Task.Run(() => Ok(result));
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("UNIQUE KEY"))
                {
                    return await Task.Run(() => Ok(2));
                }
                return Problem(ex.Message);
            }
        }
    }
}