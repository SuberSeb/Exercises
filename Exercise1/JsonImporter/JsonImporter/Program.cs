using JsonImporter.Models;
using JsonImporter.Tools;
using System;
using JsonImporter.Menu;
using JsonImporter.Queue;

namespace JsonImporter
{
    public class Program
    {
        public static void Main(string[] args)
        {            
            MenuMain.ShowMessageCreateDialog();
            MenuMain.ShowDatabaseImportDialog(MenuMain.ShowJsonParserDialog());
            MenuMain.ShowDeleteMessageFileDialog();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}