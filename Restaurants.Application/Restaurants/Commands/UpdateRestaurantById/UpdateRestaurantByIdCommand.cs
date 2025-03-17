using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurantById
{
    public class UpdateRestaurantByIdCommand() : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool HasDelivery { get; set; }

    }
}