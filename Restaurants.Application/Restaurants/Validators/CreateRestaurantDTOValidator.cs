using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Restaurants.Application.Restaurants.DTOs;

namespace Restaurants.Application.Restaurants.Validators
{
    public class CreateRestaurantDTOValidator : AbstractValidator<CreateRestaurantDTO>
    {
        private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];
        public CreateRestaurantDTOValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);

            RuleFor(dto => dto.Description)
                .NotEmpty()
                .WithMessage("Description is required");

            RuleFor(dto => dto.Category)
                .Must(validCategories.Contains)
                .WithMessage("Invalid category. Please choose from the valid categories.");

            // Another possibility
            // RuleFor(dto => dto.Category)
            //     .Custom(ValidateCategory);

            // Another possibility:
            // RuleFor(dto => dto.Category)
            //     .Custom((value, context) =>
            //     {
            //         var isValidCategory = validCategories.Contains(value);
            //         if (!isValidCategory)
            //         {
            //             context.AddFailure("Category", "Invalid category. Please choose from the valid categories.");
            //         }
            //     });

            RuleFor(dto => dto.ContactEmail)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");

            RuleFor(dto => dto.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Please provide a valid postal code (XX-XXX)");
        }

        // private void ValidateCategory(string value, ValidationContext<CreateRestaurantDTO> context)
        // {
        //     var isValidCategory = validCategories.Contains(value);
        //     if (!isValidCategory)
        //     {
        //         context.AddFailure("Category", "Invalid category. Please choose from the valid categories.");
        //     }
        // }
    }
}