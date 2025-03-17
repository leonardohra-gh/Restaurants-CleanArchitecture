using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurantById
{
    public class UpdateRestaurantByIdCommandValidator : AbstractValidator<UpdateRestaurantByIdCommand>
    {
        public UpdateRestaurantByIdCommandValidator()
        {
            RuleFor(c => c.Name)
                .Length(3, 100);
        }
    }
}