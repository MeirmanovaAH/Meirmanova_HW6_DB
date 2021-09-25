
namespace Meirmanova_HW6_DB
{
    class Program
    {
        public const string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=Start123;Database=postgres";
        static void Main(string[] args)
        {
            Creator.CreateTables(ConnectionString);
            FillData.FillTablesData(ConnectionString);
            Printer.PrintTablesData(ConnectionString);
            TableChanger changer = new(ConnectionString);
            changer.Start();
        }

    }
}

