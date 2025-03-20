using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishValidator()
        {
            RuleFor(cmd => cmd.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0.");

            RuleFor(cmd => cmd.KiloCalories)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Calories can't be a negative number.");
        }
    }
}