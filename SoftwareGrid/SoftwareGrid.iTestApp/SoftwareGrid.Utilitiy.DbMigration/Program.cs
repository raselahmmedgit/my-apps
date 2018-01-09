using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareGrid.Utilitiy.DbMigration
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Start...");

                iTestAppContext iTestAppContext = new iTestAppContext();

                if (!iTestAppContext.Database.Exists())
                {
                    Console.WriteLine("Database is exists.");
                }
                else
                {
                    Console.WriteLine("Database is not exists.");
                }

                //InitializeAndSeedDb();

                Console.WriteLine("Done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private static void InitializeAndSeedDb()
        {
            try
            {
                // Initializes and seeds the database.
                Database.SetInitializer(new DbInitializer());

                using (var context = new iTestAppContext())
                {
                    context.Database.Initialize(force: true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
