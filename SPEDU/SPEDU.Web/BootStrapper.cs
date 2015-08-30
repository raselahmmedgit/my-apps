using System;
using System.IO;
using System.Web.Http;
using System.Data.Entity;
using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using SPEDU.Data;
using SPEDU.Data.Infrastructure;
using SPEDU.Data.Repositories;
using SPEDU.Business.Application;

namespace SPEDU.Web
{
    public static class BootStrapper
    {
        public static void Run()
        {
            try
            {
                //log4net.Config.XmlConfigurator.Configure(new FileInfo(System.Web.HttpContext.Current.Server.MapPath("~/Web.config")));

                InitializeAndSeedDb();

                SetIocContainer();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private static void InitializeAndSeedDb()
        {
            try
            {
                // Initializes and seeds the database.
                Database.SetInitializer(new DbInitializer());

                using (var context = new AppDbContext())
                {
                    context.Database.Initialize(force: true);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private static void SetIocContainer()
        {
            try
            {
                //Implement Autofac

                var configuration = GlobalConfiguration.Configuration;
                var builder = new ContainerBuilder();

                // Register MVC controllers using assembly scanning.
                builder.RegisterControllers(Assembly.GetExecutingAssembly());

                // Register MVC controller and API controller dependencies per request.
                builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();
                builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();
                //builder.RegisterGeneric(typeof(RepositoryBase<>)).InstancePerLifetimeScope();
                builder.RegisterGeneric(typeof(Repository<>)).InstancePerLifetimeScope();

                // Register service
                builder.RegisterAssemblyTypes(typeof(UserRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerDependency();

                var container = builder.Build();

                //for MVC Controller Set the dependency resolver implementation.
                var resolverMvc = new AutofacDependencyResolver(container);
                System.Web.Mvc.DependencyResolver.SetResolver(resolverMvc);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}