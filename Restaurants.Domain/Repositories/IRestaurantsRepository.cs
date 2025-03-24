using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repositories
{
    public interface IRestaurantsRepository
    {
        Task<int> CountRestaurantsOwnedByUser(string userId);
        Task<Restaurant> Create(Restaurant restaurant);
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<Restaurant?> GetById(int id);
        Task Delete(Restaurant entity);
        Task SaveChangesAsync();
        Task<(IEnumerable<Restaurant>, int)> GetAllMatchignAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
    }
}