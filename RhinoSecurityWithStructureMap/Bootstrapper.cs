using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Rhino.Security.Interfaces;
using Rhino.Security.Services;
using StructureMap;

namespace RhinoSecurityWithStructureMap
{
    public class Bootstrapper
    {
        public static void Bootstrap(string connectionString)
        {
            var container = new StructureMap.Container();
            container.Configure(cfg =>
                {
                    //NHibernate
                    cfg.For<ISessionFactory>().Singleton().Use(() => CreateSessionFactory(connectionString));
                    cfg.For<ISession>().Use(context => GetSession(context));

                    //Rhino Security
                    cfg.For<IAuthorizationService>().Use<AuthorizationService>();
                    cfg.For<IAuthorizationRepository>().Use<AuthorizationRepository>();
                    cfg.For<IPermissionsBuilderService>().Use<PermissionsBuilderService>();
                    cfg.For<IPermissionsService>().Use<PermissionsService>();
                });

            //Setting up common service locator
            Microsoft.Practices.ServiceLocation.ServiceLocator
                .SetLocatorProvider(() => new StructureMapServiceLocator(container));
        }

        private static ISessionFactory CreateSessionFactory(string connectionString)
        {
            FluentConfiguration fluentConfig = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Bootstrapper>())
                .CurrentSessionContext(typeof(ThreadStaticSessionContext).AssemblyQualifiedName);

            Configuration config = fluentConfig.BuildConfiguration();
            RegisterRhinoSecurity(config);

            return fluentConfig.BuildSessionFactory();
        }

        private static ISession GetSession(IContext context)
        {
            var sessionFactory = context.GetInstance<ISessionFactory>();
            return sessionFactory.GetCurrentSession();
        }

        /// <summary>
        /// Registering Rhino.Security infrastructure to NHibernate
        /// </summary>
        /// <param name="config"></param>
        private static void RegisterRhinoSecurity(Configuration config)
        {
            Rhino.Security.Security.Configure<User>(config, Rhino.Security.SecurityTableStructure.Prefix);
        }
    }
}
