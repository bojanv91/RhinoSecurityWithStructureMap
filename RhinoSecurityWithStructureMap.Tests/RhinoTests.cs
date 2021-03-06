﻿using System;
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
        private readonly IAuthorizationService _authorizationService;
        private readonly User _loggedInUser;

        public RhinoTests() 
        {
            _authorizationService = ServiceLocator.Current.GetInstance<IAuthorizationService>();
            _loggedInUser = TestFixture._loggedInUser;
        }

        [Fact]
        public void it_should_allow_content_creation()
        {
            bool result = _authorizationService.IsAllowed(_loggedInUser, "/Content/Create");
            Assert.True(result);
        }

        [Fact]
        public void it_should_allow_content_viewing()
        {
            bool result = _authorizationService.IsAllowed(_loggedInUser, "/Content/View");
            Assert.True(result);
        }

        [Fact]
        public void it_should_deny_content_deletition()
        {
            bool result = _authorizationService.IsAllowed(_loggedInUser, "/Content/Delete");
            Assert.False(result);
        }

        public void SetFixture(TestFixture data)
        {
        }
    }
}
