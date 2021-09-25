using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meirmanova_HW6_DB
{
    internal static class Printer
    {
        public static void PrintTablesData(string ConnectionString)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            GetAndPrintTableData("items", connection);
            GetAndPrintTableData("item_amount", connection);
            GetAndPrintTableData("shops", connection);
        }
        private static void GetAndPrintTableData(string tableName, NpgsqlConnection connection)
        {
            string sql = $"SELECT * FROM {tableName}";
            using var cmd = new NpgsqlCommand(sql, connection);

            List<string[]> tableData;
            using (var reader = cmd.ExecuteReader())
            {
                tableData = GetTableData(reader);
            }

            PrintTableData(tableName, tableData);
        }
        private static List<string[]> GetTableData(NpgsqlDataReader reader)
        {
            List<string[]> tableData = new List<string[]>();
            // header
            int lentgh = reader.FieldCount;
            string[] value = new string[lentgh];
            for (int i = 0; i < lentgh; i++)
            {
                value[i] = reader.GetName(i);
            }
            tableData.Add(value);

            // values
            while (reader.Read())
            {
                string[] value2 = new string[lentgh];
                for (int i = 0; i < lentgh; i++)
                {
                    value2[i] = reader.GetValue(i).ToString();
                }
                tableData.Add(value2);
            }

            return tableData;
        }
        private static void PrintTableData(string tableName, List<string[]> tableData)
        {
            int[] lengths = GetColumsMaXLengths(tableData);
            StringBuilder builder = new StringBuilder();
            int tableLength = lengths.Sum() + lengths.Count() * 3 + 1;

            // table name
            string format = String.Format("{{0, {0}}}", tableLength / 2);
            builder.Append(String.Format(format, tableName) + Environment.NewLine);

            var line = new string('-', tableLength);
            int counter = 0;

            // table
            foreach (string[] row in tableData)
            {
                if (counter == 0 || counter == 1) builder.Append(line + Environment.NewLine);
                counter++;

                builder.Append("|");
                for (int i = 0; i < row.Length; i++)
                {
                    format = string.Format("{{0,-{0}}}|", lengths[i] + 1);
                    builder.Append(" " + string.Format(format, row[i]));
                }
                builder.Append($"{ Environment.NewLine}");
            }
            builder.Append(line + Environment.NewLine);
            Console.WriteLine(builder);
        }
        private static int[] GetColumsMaXLengths(List<string[]> tableData)
        {
            int[] maxLength = new int[tableData[1].Length];
            for (int i = 0; i < maxLength.Length; i++)
            {
                maxLength[i] = tableData.Max(x => x[i].Length);
            }
            return maxLength;
        }

    }
}
