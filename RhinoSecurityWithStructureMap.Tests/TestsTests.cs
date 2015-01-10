using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using NHibernate;
using Rhino.Security.Interfaces;
using Xunit;

namespace RhinoSecurityWithStructureMap.Tests
{
    public class TestsTests
    {
        static ISessionFactory sessionFactory;
        static IAuthorizationService authorizationService;

        public TestsTests()
        {
            string connStr = "Server=BOJANV91-PC;Database=rhino;User Id=sa;Password=bojan123;";
            Bootstrapper.Bootstrap(connStr);

            sessionFactory = ServiceLocator.Current.GetInstance<ISessionFactory>();
            var session = sessionFactory.OpenSession();
            NHibernate.Context.CurrentSessionContext.Bind(session);

            authorizationService = ServiceLocator.Current.GetInstance<IAuthorizationService>();
        }

        [Fact]
        public void ItShouldAllowClientCreateForAdmin()
        {
            var user = sessionFactory.GetCurrentSession().QueryOver<User>().Where(x => x.Username == "Bojan").List().First();
            bool result = authorizationService.IsAllowed(user, "/Client/Create");

            Assert.True(result);
        }

        [Fact]
        public void ItShouldDenyClientDeleteForAdmin()
        {
            var user = sessionFactory.GetCurrentSession().QueryOver<User>().Where(x => x.Username == "Bojan").List().First();
            bool result = authorizationService.IsAllowed(user, "/Client/Delete");

            Assert.False(result);
        }

        ~TestsTests()
        {
            NHibernate.Context.CurrentSessionContext.Unbind(sessionFactory);
        }
    }
}
