using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace pizzafutar
{
    internal class Adatbazis
    {
        readonly string connectionString = "server=localhost;database=pizzeria;uid=root;pwd=;";

        public List<Pizza> Pizzak { get; } = new();
        public List<Cim> Cimek { get; } = new();

        public List<Rendeles> Feltolt()
        {
            List<Rendeles> lista = new();
            Dictionary<int, Pizza> pizzak = new();
            Dictionary<int, Cim> cimek = new();
            try
            {
                using (MySqlConnection conn = new(connectionString))
                {
                    conn.Open();
                    Console.WriteLine("Database connection opened.");
                    string query = "SELECT * FROM pizza";
                    using (MySqlCommand command = new(query, conn))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                Pizza p = new(id, reader.GetString("nev"), reader.GetInt32("meret"), reader.GetInt32("ar"));
                                pizzak[id] = p;
                                Pizzak.Add(p);
                            }
                        }
                    }
                    query = "SELECT * FROM cim";
                    using (MySqlCommand command = new(query, conn))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                Cim c = new(id, reader.GetString("nev"), reader.GetString("utca"), reader.GetString("hsz"));
                                cimek[id] = c;
                                Cimek.Add(c);
                            }
                        }
                    }
                    query = "SELECT * FROM rendeles";
                    using (MySqlCommand command = new(query, conn))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                Pizza p = pizzak[reader.GetInt32("pizzaid")];
                                Cim c = cimek[reader.GetInt32("cimid")];
                                Rendeles r = new(id, p, reader.GetInt32("darab"), c, reader.GetTimeSpan("szallitas"));
                                lista.Add(r);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lista;
        }
    }
}
