using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private readonly int[] allowedPagesSizes = [5, 10, 15, 30];
        private readonly string[] allowedSortByColumnNames = [
            nameof(RestaurantDTO.Name),
            nameof(RestaurantDTO.Category),
            nameof(RestaurantDTO.Description)
        ];

        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(r => r.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Must(value => allowedPagesSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(", ", allowedPagesSizes)}]");

            RuleFor(r => r.SortBy)
                .Must(value => allowedSortByColumnNames.Contains(value))
                .When(q => q.SortBy != null)
                .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
        }
    }
}