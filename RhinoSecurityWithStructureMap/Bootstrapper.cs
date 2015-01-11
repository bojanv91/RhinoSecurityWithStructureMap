using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;
using Rhino.Security.Interfaces;
using Rhino.Security.Services;

namespace RhinoSecurityWithStructureMap
{
    public class Bootstrapper
    {
        public static void Bootstrap(string connectionString)
        {
            var container = new StructureMap.Container();
            container.Configure(cfg =>
                {
                    //NHibernate configurations 
                    cfg.For<ISessionFactory>().Singleton().Use(() => CreateSessionFactory(connectionString));
                    cfg.For<ISession>().Use(context => GetSession(context));

                    //Rhino Security configurations 
                    cfg.For<IAuthorizationService>().Use<AuthorizationService>();
                    cfg.For<IAuthorizationRepository>().Use<AuthorizationRepository>();
                    cfg.For<IPermissionsBuilderService>().Use<PermissionsBuilderService>();
                    cfg.For<IPermissionsService>().Use<PermissionsService>();
                    cfg.For(typeof(IEntityInformationExtractor<>)).Use(typeof(DefaultEntityInformationExtractor<>));
                });

            //Setting up StuctureMapServiceLocator as a CommonServiceLocator that Rhino.Security will use for DI
            Microsoft.Practices.ServiceLocation.ServiceLocator
                .SetLocatorProvider(() => new StructureMapServiceLocator(container));
        }

        private static ISessionFactory CreateSessionFactory(string connectionString)
        {
            FluentConfiguration fluentConfig = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))          //specifying connection string for Microsoft SQL Server 2012 
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Bootstrapper>())                  //specifying in which assembly NHibernate should look for entity mappings
                .CurrentSessionContext(typeof(ThreadStaticSessionContext).AssemblyQualifiedName)    //specifying the session context lifecycle to be initialized per thread
                .ExposeConfiguration(cfg =>
                {
                    Rhino.Security.Security.Configure<User>(cfg, Rhino.Security.SecurityTableStructure.Prefix);
                });

            return fluentConfig.BuildSessionFactory();
        }

        private static ISession GetSession(StructureMap.IContext context)
        {
            var sessionFactory = context.GetInstance<ISessionFactory>();
            return sessionFactory.GetCurrentSession();
        }
    }
}
