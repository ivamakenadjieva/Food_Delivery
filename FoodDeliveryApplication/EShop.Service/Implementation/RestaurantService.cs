using EShop.Domain.Domain;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRepository<Restaurant> _restaurantRepository;
        private readonly IRepository<ItemInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;

        public RestaurantService(IRepository<Restaurant> productRepository, IRepository<ItemInShoppingCart> productInShoppingCartRepository, IUserRepository userRepository)
        {
            _restaurantRepository = productRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _userRepository = userRepository;
        }

        public void CreateNewRestaurant(Restaurant p)
        {
            _restaurantRepository.Insert(p);
        }

        public void DeleteRestaurant(Guid id)
        {
            var product = _restaurantRepository.Get(id);
            _restaurantRepository.Delete(product);
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return _restaurantRepository.GetAll().ToList();
        }

        public Restaurant GetDetailsForRestaurant(Guid? id)
        {
            var product = _restaurantRepository.Get(id);
            return product;
        }

        public void UpdateExistingRestaurant(Restaurant p)
        {
            _restaurantRepository.Update(p);
        }
    }
}
