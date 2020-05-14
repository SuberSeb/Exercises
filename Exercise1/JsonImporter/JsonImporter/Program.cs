using JsonImporter.Models;
using JsonImporter.Tools;
using System;
using JsonImporter.Menu;

namespace JsonImporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MenuMain.ShowMessageCreateDialog();
            MenuMain.ShowDatabaseImportDialog(MenuMain.ShowJsonParserDialog());

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}