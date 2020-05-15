using JsonImporter.Menu;

namespace JsonImporter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MenuMain.ShowMessageCreateDialog();
            MenuMain.ShowDatabaseImportDialog(MenuMain.ShowJsonParserDialog());
            MenuMain.ShowDeleteMessageFileDialog();
        }
    }
}