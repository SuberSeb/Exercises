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
                    db.Messages.AddRange(messages);
                    int messagesAdded = db.SaveChanges();

                    Console.WriteLine($"{messagesAdded} rows was successfully added to database.");
                    logger.Info($"{messagesAdded} rows was successfully added to database.");

                    return messagesAdded;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding messages to database: " + ex);
                logger.Error("Error while adding messages to database: " + ex);

                return 0;
            }
        }
    }
}
