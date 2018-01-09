using SoftwareGrid.Model.iTestApp.DocumentManagement;
using SoftwareGrid.Model.iTestApp.QuestionManagement;
using SoftwareGrid.Model.iTestApp.SecurityManagement;
using SoftwareGrid.Model.iTestApp.TestManagement;
using SoftwareGrid.Model.iTestApp.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SoftwareGrid.Utilitiy.DbMigration
{
    public class iTestAppContext : DbContext
    {
        public iTestAppContext()
            : base(ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString)
        {
        }
        
        #region Quiz

        public DbSet<FavoriteTest> FavoriteTest { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<TestCategory> TestCategory { get; set; }
        public DbSet<TestTaken> TestTaken { get; set; }
        public DbSet<TestTakenDetail> TestTakenDetail { get; set; }
        public DbSet<TestWiseQuestion> TestWiseQuestion { get; set; }

        public DbSet<Question> Question { get; set; }
        public DbSet<QuestionAnswerOption> QuestionAnswerOption { get; set; }
        public DbSet<QuestionCategory> QuestionCategory { get; set; }

        public DbSet<DocumentInformation> DocumentInformation { get; set; }
        public DbSet<LastGlobalIdInformation> LastGlobalIdInformation { get; set; }

        #endregion

        #region SecurityManagement

        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyBranch> CompanyBranch { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserLoginInformation> UserLoginInformation { get; set; }

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
    //public class DbInitializer : DropCreateDatabaseAlways<iTestAppContext>
    //public class DbInitializer : CreateDatabaseIfNotExists<iTestAppContext>
    public class DbInitializer : DropCreateDatabaseIfModelChanges<iTestAppContext>
    {
        private static void CreateUserWithRole(string username, string password, string email, int roleId, iTestAppContext context)
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

        protected override void Seed(iTestAppContext context)
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
