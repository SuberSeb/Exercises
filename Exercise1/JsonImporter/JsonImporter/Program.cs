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
            //MenuMain.ShowMessageCreateDialog();
            //MenuMain.ShowDatabaseImportDialog(MenuMain.ShowJsonParserDialog());
            //ApacheKafka
            //MenuMain.ShowDeleteMessageFileDialog();

            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("Send 10 messages");
                Console.ReadKey();
                Producer.SendToKafka();
            }            

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}