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
    public class RhinoTests : IUseFixture<TestFixture>
    {
        public void SetFixture(TestFixture data)
        {
        }

        [Fact]
        public void it_should_allow_content_creation()
        {
            bool result = TestFixture._authorizationService.IsAllowed(TestFixture._loggedInUser, "/Content/Create");
            Assert.True(result);
        }

        [Fact]
        public void it_should_allow_content_viewing()
        {
            bool result = TestFixture._authorizationService.IsAllowed(TestFixture._loggedInUser, "/Content/View");
            Assert.True(result);
        }

        [Fact]
        public void it_should_deny_content_deletition()
        {
            bool result = TestFixture._authorizationService.IsAllowed(TestFixture._loggedInUser, "/Content/Delete");
            Assert.False(result);
        }
    }
}
