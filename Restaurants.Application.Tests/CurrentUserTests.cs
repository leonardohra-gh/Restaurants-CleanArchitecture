using FluentAssertions;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Tests;

public class CurrentUsersTests
{
    // TestMethod_Scenario_ExpectedResult
    [Theory()]
    [InlineData(UserRoles.Admin)]
    [InlineData(UserRoles.User)]
    public void IsInRoleTest_WithMatchingRole_ShouldReturnTrue(string roleName)
    {
        //Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act
        var isInRole = currentUser.IsInRole(roleName);

        // Assert
        isInRole.Should().BeTrue();
    }

    [Fact()]
    public void IsInRoleTest_WithNoMatchingRole_ShouldReturnFalse()
    {
        //Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act
        var isInRole = currentUser.IsInRole(UserRoles.Owner);

        // Assert
        isInRole.Should().BeFalse();
    }


    [Fact()]
    public void IsInRoleTest_WithMatchingRoleCase_ShouldReturnFalse()
    {
        //Arrange
        var currentUser = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

        // Act
        var isInRole = currentUser.IsInRole(UserRoles.Admin.ToLower());

        // Assert
        isInRole.Should().BeFalse();
    }
}
