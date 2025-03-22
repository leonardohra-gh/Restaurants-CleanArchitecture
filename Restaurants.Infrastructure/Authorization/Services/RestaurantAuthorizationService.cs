using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService(
            ILogger<RestaurantAuthorizationService> logger,
            IUserContext userContext
        ) : IRestaurantAuthorizationService
    {
        public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
        {
            var currentUser = userContext.GetCurrentUser();
            logger.LogInformation("Authorizing user {UserEmail} [{UserId}] to {Operation} for Restaurant {RestaurantName}",
                currentUser!.Email, currentUser.Id, resourceOperation, restaurant.Name);

            if (resourceOperation == ResourceOperation.Read ||
            resourceOperation == ResourceOperation.Create)
            {
                logger.LogInformation("{operation} operation - successful authorization", resourceOperation);
                return true;
            }

            if (resourceOperation == ResourceOperation.Delete && currentUser.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("Admin user, delete operation - successfull authorization");
                return true;
            }

            if ((resourceOperation == ResourceOperation.Update || resourceOperation == ResourceOperation.Delete) &&
            currentUser.Id == restaurant.OwnerId)
            {
                logger.LogInformation("Restaurant owner, {operation} operation - successfull authorization", resourceOperation);
            }

            return false;
        }
    }
}