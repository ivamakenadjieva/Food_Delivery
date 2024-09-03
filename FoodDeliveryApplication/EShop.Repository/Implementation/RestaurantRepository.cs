using EShop.Domain.Domain;
using EShop.Domain.Identity;
using EShop.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Implementation
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Restaurant> entities;

        public RestaurantRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Restaurant>();
        }
        public List<Restaurant> GetAllRestaurants()
        {
            return entities
                .Include(z => z.Items)
                .Include("Items.Item")
                .Include("Items.Item.Restaurant")
                .ToList();
        }

        public Restaurant GetDetailsForRestaurant(BaseEntity id)
        {
            return entities
                 .Include(z => z.Items)
                .Include("Items.Item")
                .Include("Items.Item.Restaurant")
                .SingleOrDefaultAsync(z => z.Id == id.Id).Result;
        }
    }
}

