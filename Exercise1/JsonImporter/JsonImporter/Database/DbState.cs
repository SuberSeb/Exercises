using Microsoft.EntityFrameworkCore;
using System;

namespace JsonImporter.Database
{
    public class DbState
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static bool IsDatabaseAvailable()
        {
            try
            {
                using var db = new ApplicationDbContext();
                db.Database.OpenConnection();
                db.Database.CloseConnection();
                return true;
            }
            catch (Exception ex)
            {                
                logger.Error("Database is not available: " + ex);
                return false;
            }
        }
    }
}
