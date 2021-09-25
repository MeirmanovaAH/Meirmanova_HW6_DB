using Npgsql;

namespace Meirmanova_HW6_DB
{
    class FillData
    {
        public static void FillTablesData(string ConnectionString)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();

            FillDataTables(connection);

            System.Console.WriteLine("\nДанные внесены в таблицы\n");
        }

        private static void FillDataTables(NpgsqlConnection connection)
        {

            var setSQL =
                @"
                    INSERT INTO items (name, code, category)
                    VALUES
                    ('Sansa', 'northsqueen111','collection figure'),
                    ('Daenerys', 'bloodoffire111','collection figure'),
                    ('Jon', 'bastard111','collection figure'),
                    ('Cercei', 'westerosqueen111','collection figure'),
                    ('Arya', 'noname111','collection figure');
                ";

            using (var cmd = new NpgsqlCommand(setSQL, connection))
            {
                cmd.ExecuteNonQuery();
            }


            setSQL =
                @"
                    INSERT INTO shops (email, phone)
                    VALUES
                    ('seller1@mail.ru', '392-22-22'),
                    ('seller2@gmail.com', '777-77-77'),
                    ('seller3@bk.ru', '123-45-67'),
                    ('seller4@male.ru', '666-66-66'),
                    ('seller5@gmail.com', '555-55-55');
                ";

            using (var cmd = new NpgsqlCommand(setSQL, connection))
            {
                cmd.ExecuteNonQuery();
            }


            setSQL =
                @"
                    INSERT INTO item_amount (amount, shop_id, item_id)
                    VALUES
                    (21, 1, 1),
                    (34, 2, 2),
                    (8,  3, 3),
                    (5,  4, 4),
                    (14, 5, 5);
                ";

            using (var cmd = new NpgsqlCommand(setSQL, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

    }
}
