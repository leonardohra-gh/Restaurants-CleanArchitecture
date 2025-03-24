using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistance;

namespace Restaurants.Infrastructure.Repositories
{
    internal class RestaurantsRepository(RestaurantDbContext dbContext) : IRestaurantsRepository
    {
        public async Task<int> CountRestaurantsOwnedByUser(string userId)
        {
            return await dbContext.Restaurants
                .CountAsync(r => r.OwnerId == userId);
        }

        public async Task<Restaurant> Create(Restaurant restaurant)
        {
            dbContext.Restaurants.Add(restaurant);
            await dbContext.SaveChangesAsync();
            return restaurant;
        }

        public async Task Delete(Restaurant entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await dbContext.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchignAsync(
            string? searchPhrase,
            int pageSize,
            int pageNumber,
            string? sortBy,
            SortDirection sortDirection
        )
        {
            var searchPhraseLower = searchPhrase?.ToLower() ?? "";
            var baseQuery = dbContext.Restaurants.Where(
                r => r.Name.ToLower().Contains(searchPhraseLower)
                || r.Description.ToLower().Contains(searchPhraseLower)
            );

            var totalCount = await baseQuery.CountAsync();

            if (sortBy != null)
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name), r => r.Name},
                    {nameof(Restaurant.Description), r => r.Description},
                    {nameof(Restaurant.Category), r => r.Category}
                };
                var selectedColumn = columnsSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
            }

            var restaurants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();

            return (restaurants, totalCount);
        }

        public async Task<Restaurant?> GetById(int id)
        {
            var restaurant = await dbContext.Restaurants
                .Include(r => r.Dishes)
                .FirstOrDefaultAsync(x => x.Id == id);
            return restaurant;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}