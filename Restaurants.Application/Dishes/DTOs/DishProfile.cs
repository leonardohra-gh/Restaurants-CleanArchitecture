using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDishCommand;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.DTOs
{
    public class DishProfile : Profile
    {
        public DishProfile()
        {
            CreateMap<Dish, DishDTO>();
            CreateMap<CreateDishCommand, Dish>();
        }
    }
}