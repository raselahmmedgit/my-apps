using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using rabapp.Model.Quiz.DocumentManagement;
using rabapp.Model.Quiz.QuestionManagement;
using rabapp.Model.Quiz.SecurityManagement;
using rabapp.Model.Quiz.TestManagement;
using rabapp.Service.Common.Helper;

namespace rabapp.Utility.DbMigration
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext()
            : base(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString)
        {
        }

        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString; }
        }

        #region Quiz

        public DbSet<DocumentInformation> DocumentInformation { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionAnswerOption> QuestionAnswerOption { get; set; }
        public DbSet<QuestionCategory> QuestionCategory { get; set; }
        public DbSet<FavoriteTest> FavoriteTest { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<TestCategory> TestCategory { get; set; }
        public DbSet<TestQuestion> TestQuestion { get; set; }
        public DbSet<TestTaken> TestTaken { get; set; }
        public DbSet<TestTakenDetails> TestTakenDetails { get; set; }
        public DbSet<TestWiseQuestion> TestWiseQuestion { get; set; }

        #endregion

        #region SecurityManagement

        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyBranch> CompanyBranch { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ContactJobTiteRatingStaffingRate>()
            //        .Map(e => e.ToTable("ContactJobTiteRatingStaffingRate"));
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }

    #region Initial data

    // Change the base class as follows if you want to drop and create the database during development:
    //public class DbInitializer : DropCreateDatabaseAlways<QuizDbContext>
    //public class DbInitializer : CreateDatabaseIfNotExists<QuizDbContext>
    public class DbInitializer : DropCreateDatabaseIfModelChanges<QuizDbContext>
    {
        private static void CreateUserWithRole(string username, string password, string email, int roleId, QuizDbContext context)
        {
            var user = new User { UserName = username, Password = password, Email = email, CreatedBy = 1, CreatedDate = DateTime.UtcNow, UpdatedBy = 1, UpdatedDate = DateTime.UtcNow, DeletedBy = 1, DeletedDate = DateTime.UtcNow };
            context.User.Add(user);
            context.SaveChanges();
            
            // Add the role.
            var existUser = context.User.Find(user.UserId);
            var existRole = context.Role.Find(roleId);

            var userRole = new UserRole { UserId = existUser.UserId, RoleId = existRole.RoleId };
            context.UserRole.Add(userRole);
            context.SaveChanges();
        }

        protected override void Seed(QuizDbContext context)
        {
            // Create default roles.
            var roles = new List<Role>
                            {
                                new Role {RoleName = "Admin"},
                                new Role {RoleName = "Employee"},
                                new Role {RoleName = "User"}
                            };

            roles.ForEach(r => context.Role.Add(r));
            context.SaveChanges();

            // Create some users.
            CreateUserWithRole("Admin", "@123456", "admin@gmail.com", Convert.ToInt32(AppRoles.Admin), context);
            CreateUserWithRole("Employee", "@123456", "employee@gmail.com", Convert.ToInt32(AppRoles.Employee), context);
            CreateUserWithRole("User", "@123456", "user@gmail.com", Convert.ToInt32(AppRoles.User), context);
            CreateUserWithRole("Rasel", "@123456", "raselahmmed@gmail.com", Convert.ToInt32(AppRoles.User), context);

        }
    }

    #endregion
}
