using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using NHibernate.Context;
using Rhino.Security.Interfaces;

namespace RhinoSecurityWithStructureMap
{
    class Program
    {
        static ISessionFactory sessionFactory;
        static IAuthorizationRepository authorizationRepository;
        static IAuthorizationService authorizationService;
        static IPermissionsBuilderService permissionsBuilderService;
        static IPermissionsService permissionService;

        static void Main(string[] args)
        {
            string connStr = "Server=BOJANV91-PC;Database=rhino;User Id=sa;Password=bojan123;";
            Bootstrapper.Bootstrap(connStr);

            sessionFactory = ServiceLocator.Current.GetInstance<ISessionFactory>();
            var session = sessionFactory.OpenSession();
            NHibernate.Context.CurrentSessionContext.Bind(session);

            authorizationRepository = ServiceLocator.Current.GetInstance<IAuthorizationRepository>();
            authorizationService = ServiceLocator.Current.GetInstance<IAuthorizationService>();
            permissionsBuilderService = ServiceLocator.Current.GetInstance<IPermissionsBuilderService>();
            permissionService = ServiceLocator.Current.GetInstance<IPermissionsService>();

            //SetupInitData();    //run this only once to setup the data

            NHibernate.Context.CurrentSessionContext.Unbind(sessionFactory);
        }

        static void SetupInitData()
        {
            var session = ServiceLocator.Current.GetInstance<ISession>();

            using (var transaction = session.BeginTransaction())
            {
                var user = new User() { Username = "Bojan" };
                session.SaveOrUpdate(user);

                authorizationRepository.CreateUsersGroup("Admin");
                authorizationRepository.CreateOperation("/Client");
                authorizationRepository.CreateOperation("/Client/Create");
                authorizationRepository.CreateOperation("/Client/Delete");

                transaction.Commit();
            }

            using (var transaction = session.BeginTransaction())
            {
                var user = session.QueryOver<User>().Where(x => x.Username == "Bojan").List().First();
                authorizationRepository.AssociateUserWith(user, "Admin");
                permissionsBuilderService.Allow("/Client/Create").For("Admin").OnEverything().DefaultLevel().Save();
                permissionsBuilderService.Deny("/Client/Delete").For("Admin").OnEverything().DefaultLevel().Save();

                transaction.Commit();
            }
        }
    }
}
