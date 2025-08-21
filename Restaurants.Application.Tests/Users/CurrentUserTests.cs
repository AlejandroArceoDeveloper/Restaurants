using Xunit;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurants.Domain.Constants;
using FluentAssertions;

namespace Restaurants.Application.Users.Tests
{
    public class CurrentUserTests
    {
        // test Method_Scenario_ExpectedBehavior
        [Fact()]
        public void IsInRoleTest_WithMatchingRole_ShouldReturnTrue()
        {
            // Arrange

            var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User]);

            // Act
            
            var isInRole = currentUser.IsInRole(UserRoles.Admin);

            // Assert

            isInRole.Should().BeTrue();
        }
    }
}