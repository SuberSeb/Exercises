using JsonImporter.Models;
using JsonImporter.Tools;
using System;
using System.Collections.Generic;

namespace JsonImporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Menu.ShowMessageCreateDialog();
            List<Message> messages = Menu.ShowJsonParserDialog();
            Menu.ShowDatabaseImportDialog(messages);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}