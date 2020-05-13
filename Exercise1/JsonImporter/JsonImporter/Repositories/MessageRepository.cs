using JsonImporter.Models;
using JsonImporter.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace JsonImporter.Repositories
{
    class MessageRepository
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static int SaveMessages(List<Message> messages)
        {
            var clock = new Stopwatch();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    clock.Start();
                    db.Messages.AddRange(messages);
                    int messagesAdded = db.SaveChanges();
                    clock.Stop();

                    Console.WriteLine($"{messagesAdded} rows was successfully added to database. Elapsed time: {clock.ElapsedMilliseconds} ms.");
                    logger.Info($"{messagesAdded} rows was successfully added to database. Elapsed time: {clock.ElapsedMilliseconds} ms.");

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

        public static void SaveMessagesBulk(List<Message> messages)
        {
            var clock = new Stopwatch();

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    clock.Start();
                    db.BulkInsert(messages, options => options.IncludeGraph = true);
                    clock.Stop();

                    Console.WriteLine($"Messages was successfully added to database. Elapsed time: {clock.ElapsedMilliseconds} ms.");
                    logger.Info($"Messages was successfully added to database. Elapsed time: {clock.ElapsedMilliseconds} ms.");                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while adding messages to database: " + ex);
                logger.Error("Error while adding messages to database: " + ex);
            }            
        }
    }
}
