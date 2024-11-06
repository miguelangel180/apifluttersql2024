using Entidad.General;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Infraestructura
{
    public class Conexion : DbContext
    {
        public Conexion(DbContextOptions<Conexion> options) : base(options)
        {
        }

        public DbSet<En_De_Alumnos> AlumnosConexion { get; set; }
        public DbSet<En_De_Lista_Notas> ListaNotasConexion { get; set; }
        public DbSet<En_De_Materias> MateriasConexion { get; set; }
        public DbSet<En_De_Notas> NotasConexion { get; set; }



    }
}
