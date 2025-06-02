using System.Collections.Generic;
using System.Reflection.Emit;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;
using static System.Net.Mime.MediaTypeNames;

namespace RPGNew
{
    internal class DatabaseService
    {
        public void Initialize()
        {
            using (var connection = new SqliteConnection("Data Source=game.db"))
            {
                connection.Open();
                var query = connection.CreateCommand();
                query.CommandText =
                    @"
                    CREATE TABLE IF NOT EXISTS characters (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    name TEXT,
                    level INTEGER,
                    health INTEGER,
                    damage INTEGER
                    )";

                query.ExecuteNonQuery();
                connection.Close();

                Console.WriteLine("Utworzono tabelę!");
            }
        }
        public void SaveCharacter(Character character)
        {
            using (var connection = new SqliteConnection("Data Source=game.db"))
            {
                connection.Open();
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT COUNT(*) FROM characters WHERE name = @name";
                selectCmd.Parameters.AddWithValue("@name", character.Name);

                int count = Convert.ToInt32(selectCmd.ExecuteScalar());

                var saveCmd = connection.CreateCommand();
                if (count == 0)
                {
                    saveCmd.CommandText = "INSERT INTO characters(name, level, health, damage) VALUES(@name, @level, @health, @damage)";
                    Console.WriteLine("Insert!");
                }
                else
                {
                    saveCmd.CommandText = "UPDATE characters SET level = @level, health = @health, damage = @damage WHERE name = @name";
                    Console.WriteLine("Update!");
                }

                saveCmd.Parameters.AddWithValue("@name", character.Name);
                saveCmd.Parameters.AddWithValue("@level", character.Level);
                saveCmd.Parameters.AddWithValue("@health", character.Health);
                saveCmd.Parameters.AddWithValue("@damage", character.Damage);

                saveCmd.ExecuteNonQuery();
                connection.Close();

                Console.WriteLine("Zapisano dane postaci!");
            }
        }
        public Character LoadLastSave()
        {
            using (var connection = new SqliteConnection("Data Source=game.db"))
            {
                connection.Open();
                var query = connection.CreateCommand();
                query.CommandText = "SELECT * FROM characters ORDER BY id DESC LIMIT 1";

                var reader = query.ExecuteReader();

                if (reader.Read())
                {
                    Console.WriteLine("Wczytano ostatni zapis: ");
                    Console.WriteLine($"ID: {reader["id"]}, " +
                                      $"Name: {reader["name"]}, " +
                                      $"Level: {reader["level"]}, " +
                                      $"HP: {reader["health"]}, " +
                                      $"DMG: {reader["damage"]}");
                    Console.WriteLine("-----");

                    var character = new Character
                    {
                        Name = reader["name"].ToString(),
                        Level = Convert.ToInt32(reader["level"]),
                        Health = Convert.ToInt32(reader["health"]),
                        Damage = Convert.ToInt32(reader["damage"])
                    };

                    return character;
                }

                return null;
            }
        }
    }
}
