using JsonImporter.Menu;
using System;

namespace JsonImporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleKeyInfo buttonPressed;

            do
            {
                MenuMain.ShowMessageCreateDialog();
                MenuMain.ShowDatabaseImportDialog(MenuMain.ShowJsonParserDialog());
                MenuMain.ShowDeleteMessageFileDialog();

                Console.WriteLine();
                Console.WriteLine("Do it again? Press Esc to exit.");
                buttonPressed = Console.ReadKey();
            }
            while (buttonPressed.Key != ConsoleKey.Escape);            
        }
    }
}