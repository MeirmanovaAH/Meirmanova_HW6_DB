using Npgsql;

namespace Meirmanova_HW6_DB
{
    internal static class Creator
    {

        public static bool ChechConnection(string ConnectionString)
        {
            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void CreateTables(string ConnectionString)
        {
            using var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            CreateTables(connection);

            System.Console.WriteLine("\nТаблицы созданы\n");
        }

        private static void CreateTables(NpgsqlConnection connection)
        {

            var tableSQL =
                $@"
                    CREATE SEQUENCE items_id_seq;

                    CREATE TABLE items
                    (
                        id         BIGINT                  NOT NULL    DEFAULT NEXTVAL('items_id_seq'),
                        name       CHARACTER VARYING(20)   NOT NULL,
                        code       CHARACTER VARYING(20)   NOT NULL,
                        category   CHARACTER VARYING(25)   NOT NULL,

                        CONSTRAINT items_pkey PRIMARY KEY(id),
                        CONSTRAINT items_item_code UNIQUE(code)

                    );

                    CREATE UNIQUE INDEX items_name ON items(lower(name));
                ";

            using (var cmd = new NpgsqlCommand(tableSQL, connection))
            {
                cmd.ExecuteNonQuery();
            }



            tableSQL =
                $@"
                    CREATE SEQUENCE shops_id_seq;

                    CREATE TABLE shops
                    (
                        id          BIGINT                  NOT NULL    DEFAULT NEXTVAL('shops_id_seq'),
                        email       CHARACTER VARYING(255),
                        phone       CHARACTER VARYING(20),


                        CONSTRAINT shops_pkey PRIMARY KEY(id),
                        CONSTRAINT shops_email UNIQUE(email)

                    );
                ";

            using (var cmd = new NpgsqlCommand(tableSQL, connection))
            {
                cmd.ExecuteNonQuery();
            }



            tableSQL =
                $@"
                    CREATE SEQUENCE item_amount_id_seq;

                    CREATE TABLE item_amount
                    (
                        id       BIGINT    NOT NULL    DEFAULT NEXTVAL('item_amount_id_seq'),
                        amount   INTEGER,
                        shop_id  BIGINT    NOT NULL,
                        item_id  BIGINT    NOT NULL,   


                        CONSTRAINT item_amount_pkey PRIMARY KEY(id),
                        CONSTRAINT item_amount_fk_item_amount_item_item_id FOREIGN KEY(item_id) REFERENCES items(id) ON DELETE CASCADE,
                        CONSTRAINT item_amount_fk_item_amount_shop_id FOREIGN KEY(shop_id) REFERENCES shops(id) ON DELETE CASCADE
                    );
                ";

            using (var cmd = new NpgsqlCommand(tableSQL, connection))
            {
                cmd.ExecuteNonQuery();
            }


        }

    }
}
