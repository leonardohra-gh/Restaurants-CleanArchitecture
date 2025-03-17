using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurantById
{
    public class DeleteRestaurantByIdCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}