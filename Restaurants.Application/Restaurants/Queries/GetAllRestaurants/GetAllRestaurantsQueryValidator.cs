using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
    {
        private readonly int[] allowedPagesSizes = [5, 10, 15, 30];

        public GetAllRestaurantsQueryValidator()
        {
            RuleFor(r => r.PageNumber)
                .GreaterThanOrEqualTo(1);

            RuleFor(r => r.PageSize)
                .Must(value => allowedPagesSizes.Contains(value))
                .WithMessage($"Page size must be in [{string.Join(", ", allowedPagesSizes)}]");
        }
    }
}