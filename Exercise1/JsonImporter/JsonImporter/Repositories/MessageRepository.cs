using JsonImporter.Models;
using JsonImporter.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace JsonImporter.Repositories
{
    class MessageRepository
    {
        public static void SaveMessage(Message message)
        {
            using (var db = new ApplicationDbContext())
            {
                //db.Database.Log = Console.Write;
                db.Messages.Add(message);
                db.SaveChanges();
            }
        }
    }
}
