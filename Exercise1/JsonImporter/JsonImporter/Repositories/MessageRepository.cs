using JsonImporter.Models;
using JsonImporter.Database;
using System;
using System.Collections.Generic;

namespace JsonImporter.Repositories
{
    class MessageRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static int SaveMessages(List<Message> messages)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    foreach(Message message in messages)
                    {
                        db.Messages.Add(message);
                    }
                    return db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding Message to database: " + ex);
                logger.Error("Error while adding Message to database: " + ex);
                return 0;
            }
        }
    }
}
