namespace SoftwareGrid.Utilitiy.DbMigration.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SoftwareGrid.Utilitiy.DbMigration.iTestAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SoftwareGrid.Utilitiy.DbMigration.iTestAppContext";
        }

        protected override void Seed(SoftwareGrid.Utilitiy.DbMigration.iTestAppContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.Role.AddOrUpdate(
            //      p => p.RoleName,
            //      new Role { RoleName = "Employee" }
            //    );
            //
        }
    }
}
