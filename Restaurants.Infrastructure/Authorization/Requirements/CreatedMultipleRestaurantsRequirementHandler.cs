using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleRestaurantsRequirementHandler(
        IRestaurantsRepository restaurantsRepository,
        IUserContext userContext
    ) : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            CreatedMultipleRestaurantsRequirement requirement)
        {
            var currentUser = userContext.GetCurrentUser();
            var quantityRestaurantsCreated = await restaurantsRepository.CountRestaurantsOwnedByUser(currentUser!.Id);
            if (quantityRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
                context.Succeed(requirement);
            else
                context.Fail();
        }
    }
}