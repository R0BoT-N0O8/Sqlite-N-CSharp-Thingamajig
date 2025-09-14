using Microsoft.Data.Sqlite;

namespace Sqlite_N_CSharp_Thingamajig;

class Program
{
    static void Main(string[] args)
    {
        var db = new Database();

        Console.WriteLine("Choose mode:");
        Console.WriteLine("1: Run Default preset commands");
        Console.WriteLine("2: Run Manual mode (UNFINISHED!)");
        Console.WriteLine("3: List the database");
        Console.Write("> ");

        string? choice = Console.ReadLine()?.Trim();

        if (choice == "1")
        {
            RunDefault(db);
        }
        else if (choice == "2")
        {
            RunManual(db);
        }
        else if (choice == "3")
        {
            ListTables();
        }
        else
        {
            Console.WriteLine("Invalid choice, exiting.");
        }
    }

    static void RunDefault(Database db)
    {
        Console.WriteLine("blegh :P");

        db.AddOwner("SomeRandomRobotOWO", "358-1234567");
        db.AddPet("Duracell", "12v Battery", 1);
        Console.WriteLine(db.GetOwnerPhoneByPetName("Duracell"));
        db.UpdateOwnerPhone(1, "040-7654321");
        Console.WriteLine(db.GetOwnerPhoneByPetName("Duracell"));
    }

    static void RunManual(Database db)
    {
        Console.WriteLine("..Oops, sorry! Unfinished due to lack of time. My bad TwT");
        Console.WriteLine("\nI'd planned for this to allow you to run commands yourself without");
        Console.WriteLine("needing you to modify the code yourself..");
        Console.WriteLine("\n..But, y'know, i didn't have time to make it.");
        Console.WriteLine("This is mostly because the entire week i was meant to work on this,");
        Console.WriteLine("On multiple days, i didn't have much free time.");
        Console.WriteLine("\nMostly because of;");
        Console.WriteLine("* Having to attend a Birthday Party for 8 hours..");
        Console.WriteLine("* Spending too much time on my own projects...");
        Console.WriteLine("* ...Forgetting about this thing entirely..");
        Console.WriteLine("\n..So, yeah. Sorry about the lack of additional features.");
        Console.WriteLine("Please just uh.. ignore option 2 altogether xD");
        Console.WriteLine("\nBai! .w.");
        System.Threading.Thread.Sleep(10000);
        Console.WriteLine("Running preset commands in 3.");
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine("Running preset commands in 2..");
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine("Running preset commands in 1...");
        System.Threading.Thread.Sleep(1000);

        RunDefault(db);
    }
    static void ListTables()
    {
        using (var connection = new SqliteConnection("Data Source=DataBaserrr.db"))
        {
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name != 'sqlite_sequence';";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string tableName = reader.GetString(0);
                    Console.WriteLine($"-- {tableName}");

                    var rowCommand = connection.CreateCommand();
                    rowCommand.CommandText = $"SELECT * FROM {tableName};";

                    using (var rowReader = rowCommand.ExecuteReader())
                    {
                        if (!rowReader.HasRows)
                        {
                            Console.WriteLine("   (No rows)");
                            continue;
                        }

                        while (rowReader.Read())
                        {
                            for (int i = 0; i < rowReader.FieldCount; i++)
                            {
                                string colName = rowReader.GetName(i);
                                object? value = rowReader.GetValue(i);
                                Console.WriteLine($"- {colName}: {value}");
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }

            connection.Close();
        }
    }
}