using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interfaces
{
    public interface IRestaurantAuthorizationService
    {
        bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
    }
}