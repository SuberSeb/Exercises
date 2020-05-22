namespace JsonImporter.Database
{
    public class DbState
    {
        public static bool IsDatabaseAvailable()
        {
            using var db = new ApplicationDbContext();

            if (db.Database.CanConnect())
                return true;
            else
                return false;
        }
    }
}
