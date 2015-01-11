using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Rhino.Security.Interfaces;

namespace RhinoSecurityWithStructureMap.Tests
{
    public class TestFixture : IDisposable
    {
        public static ISessionFactory _sessionFactory;
        public static ISession _session;
        public static IAuthorizationRepository _authorizationRepository;
        public static IAuthorizationService _authorizationService;
        public static IPermissionsBuilderService _permissionsBuilderService;
        public static IPermissionsService _permissionService;
        public static User _loggedInUser;

        public TestFixture()
        {
            string connStr = "Server=BOJANV91-PC;Database=rhino;User Id=sa;Password=bojan123;";
            Bootstrapper.Bootstrap(connStr);

            _sessionFactory = ServiceLocator.Current.GetInstance<ISessionFactory>();
            _session = _sessionFactory.OpenSession();
            NHibernate.Context.CurrentSessionContext.Bind(_session);

            _authorizationRepository = ServiceLocator.Current.GetInstance<IAuthorizationRepository>();
            _authorizationService = ServiceLocator.Current.GetInstance<IAuthorizationService>();
            _permissionsBuilderService = ServiceLocator.Current.GetInstance<IPermissionsBuilderService>();
            _permissionService = ServiceLocator.Current.GetInstance<IPermissionsService>();

            //Inserting test data do database
            using (var transaction = _session.BeginTransaction())
            {
                //creating dummy user
                _loggedInUser = new User() { Username = "bojanv91" };
                _session.SaveOrUpdate(_loggedInUser);

                //creating user group 'Admin'
                _authorizationRepository.CreateUsersGroup("Admin");

                //creating operations
                _authorizationRepository.CreateOperation("/Content");
                _authorizationRepository.CreateOperation("/Content/Create");
                _authorizationRepository.CreateOperation("/Content/View");
                _authorizationRepository.CreateOperation("/Content/Delete");

                transaction.Commit();
            }

            using (var transaction = _session.BeginTransaction())
            {
                //adding the LoggedInUser to the 'Admin' users group
                _authorizationRepository.AssociateUserWith(_loggedInUser, "Admin");

                //Building 'Allow' permissions for the LoggedInUser, 
                //by default if not defined as allowed, the operation is denied
                //For the sake of this example, we say the the users that are in 'Admin' users group can
                //create and view content, but cannot delete content. 
                _permissionsBuilderService.Allow("/Content/Create").For("Admin").OnEverything().DefaultLevel().Save();
                _permissionsBuilderService.Allow("/Content/View").For("Admin").OnEverything().DefaultLevel().Save();

                //We can explicitly define 'Deny' permission, but as the default behaviour denies everything 
                //that is not defined as 'Allow', I am not going to define it. You don't trust me? 
                //That's why we have tests ;) 
                //_permissionsBuilderService.Deny("/Content/Delete").For("Admin").OnEverything().DefaultLevel().Save();

                transaction.Commit();
            }
        }

        public void Dispose()
        {
            //Clean up database
            using (var trans = _session.BeginTransaction())
            {
                _authorizationRepository.RemoveOperation("/Content/Create");
                _authorizationRepository.RemoveOperation("/Content/View");
                _authorizationRepository.RemoveOperation("/Content/Delete");
                _authorizationRepository.RemoveOperation("/Content");

                _authorizationRepository.DetachUserFromGroup(_loggedInUser, "Admin");
                _authorizationRepository.RemoveUsersGroup("Admin");

                _session.Delete(_loggedInUser);

                trans.Commit();
            }
            NHibernate.Context.CurrentSessionContext.Unbind(_sessionFactory);
        }
    }
}