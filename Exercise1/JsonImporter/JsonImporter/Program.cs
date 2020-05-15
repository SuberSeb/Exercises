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
                ConsoleMenu.ShowMessageCreateDialog();
                ConsoleMenu.ShowDatabaseImportDialog(ConsoleMenu.ShowJsonParserDialog());
                ConsoleMenu.ShowDeleteMessageFileDialog();

                Console.WriteLine();
                Console.WriteLine("Do it again? Press Esc to exit.");
                buttonPressed = Console.ReadKey();
            }
            while (buttonPressed.Key != ConsoleKey.Escape);
        }
    }
}