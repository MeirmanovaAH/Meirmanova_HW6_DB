using Npgsql;
using System;

namespace Meirmanova_HW6_DB
{
    internal class TableChanger
    {
        private readonly string _connectionString;
        public TableChanger(string ConnectionString)
        {
            _connectionString = ConnectionString;
        }
        public void Start()
        {
            string input = "";
            do
            {
                string message =
                    @" 
                    Выберите действие:
                    1 - добавить строку в таблицу items
                    2 - добавить строку в таблицу shops
                    3 - добавить строку в таблицу item_amount

                    5 - вывести данные таблиц

                    0 - выход
                ";
                Console.WriteLine(message);
                input = Console.ReadLine();
                TryWorking(input);
                if (input == "5") Printer.PrintTablesData(_connectionString);

            }
            while (input != "0");

        }

        private void TryWorking(string input)
        {
            if (input != "1" && input != "2" && input != "3") return;
            TypeFormatMessage(input);
            string input2 = Console.ReadLine();
            string[] args = input2.Split(" ");
            TryInsert(args, input);
        }
        private void TypeFormatMessage(string input)
        {
            string name = "";
            string message = "";
            if (input == "1")
            {
                name = "items";
                message = $"Для добавления строки в таблицу {name}\nвведите через пробел(string name, string code, string category)";
            }
            else if (input == "2")
            {
                name = "shops";
                message = $"Для добавления строки в таблицу {name}\nведите через пробел(string address, string phone)";
            }
            else if (input == "3")
            {
                name = "item_amount";
                message = $"Для добавления строки в таблицу {name}\nвведите через пробел(int amount, int shop_id, int item_id)";
            }
            else
            {
                throw new Exception("Непредвиденная ошибка ввода");
            }
            Console.WriteLine($"\n{message}\n");
        }

        private void TryInsert(string[] args, string input)
        {
            try
            {
                if (input == "1")
                {
                    InsertIntoItems(args[0], args[1], args[2]);
                }
                else if (input == "2")
                {
                    InsertIntoShops(args[0], args[1]);
                }
                else if (input == "3")
                {
                    InsertIntoItemAmount(int.Parse(args[0]), int.Parse(args[1]), int.Parse(args[2]));
                }
                else
                {
                    Console.WriteLine("Непредвиденная ошибка ввода");
                }
            }
            catch
            {
                Console.WriteLine(">>> Неверный формат данных");
            }

        }

        public void InsertIntoItems(string name, string code, string category)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var sql =
                $@"

                    INSERT INTO items (name, code, category)
                    VALUES (:name, :code, :category);
                ";

            using var cmd = new NpgsqlCommand(sql, connection);
            var parameters = cmd.Parameters;
            parameters.Add(new NpgsqlParameter("name", $"{name}"));
            parameters.Add(new NpgsqlParameter("code", $"{code}"));
            parameters.Add(new NpgsqlParameter("category", $"{category}"));
            try
            {
                cmd.ExecuteNonQuery();
                PrintSuccessMessage();
            }
            catch (Exception ex)
            {
                PrintExceptioMessage(ex.Message);
            }
        }

        public void InsertIntoShops(string address, string phone)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var sql =
                $@"

                    INSERT INTO shops (address, phone)
                    VALUES (:address, :phone);
                ";

            using var cmd = new NpgsqlCommand(sql, connection);
            var parameters = cmd.Parameters;
            parameters.Add(new NpgsqlParameter("address", $"{address}"));
            parameters.Add(new NpgsqlParameter("phone", $"{phone}"));
            try
            {
                cmd.ExecuteNonQuery();
                PrintSuccessMessage();
            }
            catch (Exception ex)
            {
                PrintExceptioMessage(ex.Message);
            }
        }

        public void InsertIntoItemAmount(int amount, int shop_id, int item_id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            var sql =
                $@"

                    INSERT INTO item_amount (amount, shop_id, item_id)
                    VALUES (@amount, @shop_id, @item_id);
                ";

            using var cmd = new NpgsqlCommand(sql, connection);
            var parameters = cmd.Parameters;
            parameters.Add(new NpgsqlParameter("amount", amount));
            parameters.Add(new NpgsqlParameter("shop_id", shop_id));
            parameters.Add(new NpgsqlParameter("item_id", item_id));
            try
            {
                cmd.ExecuteNonQuery();
                PrintSuccessMessage();
            }
            catch (Exception ex)
            {
                PrintExceptioMessage(ex.Message);
            }
        }

        private void PrintExceptioMessage(string message)
        {
            var s = new string('*', 50);
            Console.WriteLine($"\n{s}ОШИБКА{s}");
            Console.WriteLine(message);
            Console.WriteLine($"{s}******{s}\n");
        }

        private void PrintSuccessMessage() => Console.WriteLine("\n>>> Строка добавлена");
    }
}
