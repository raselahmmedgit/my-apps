using rabapp.Service.Quiz.SecurityManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabapp.Utility.DbMigration
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Start...");

                QuizDbContext quizDbContext = new QuizDbContext();

                InitializeAndSeedDb();

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

                using (var context = new QuizDbContext())
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
