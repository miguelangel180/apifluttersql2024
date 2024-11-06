using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructura
{
    public class ConexionDB
    {
        public static string CadenaConexion;

        public static Conexion Connection()
        {
            var optionBuilder = new DbContextOptionsBuilder<Conexion>();
            optionBuilder.UseSqlServer(CadenaConexion);
            optionBuilder.UseLazyLoadingProxies();
            optionBuilder.EnableSensitiveDataLogging();

            var db = new Conexion(optionBuilder.Options);
            db.ChangeTracker.AutoDetectChangesEnabled = false;
            db.Database.SetCommandTimeout(TimeSpan.FromMinutes(30));
            return db;
        }

    }
}
