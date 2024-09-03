
using EShop.Domain.Domain;
using EShop.Domain.DTO;
using EShop.Repository.Implementation;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<ItemInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ItemInOrder> _productInOrderRepository;


        public ShoppingCartService (IRepository<ItemInOrder> _productInOrderRepository, IRepository<Order> _orderRepository, IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<ItemInShoppingCart> productInShoppingCartRepository )
        {
            this._productInOrderRepository = _productInOrderRepository;
            this._orderRepository = _orderRepository;
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            
        }
        public bool AddToShoppingConfirmed(ItemInShoppingCart model, string userId)
        {

            var loggedInUser = _userRepository.Get(userId);

            var userShoppingCart = loggedInUser.ShoppingCart;

            if (userShoppingCart.ItemInShoppingCarts == null)
                userShoppingCart.ItemInShoppingCarts = new List<ItemInShoppingCart>(); ;

            userShoppingCart.ItemInShoppingCarts.Add(model);
            _shoppingCartRepository.Update(userShoppingCart);
            return true;
        }

        public bool deleteItemFromShoppingCart(string userId, Guid productId)
        {
            if (productId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.ShoppingCart;
                var product = userShoppingCart.ItemInShoppingCarts.Where(x => x.ItemId == productId).FirstOrDefault();

                userShoppingCart.ItemInShoppingCarts.Remove(product);

                _shoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            return false;

        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var loggedInUser = _userRepository.Get(userId);

            var userShoppingCart = loggedInUser?.ShoppingCart;
            var allProduct = userShoppingCart?.ItemInShoppingCarts?.ToList();

            var totalPrice = allProduct.Select(x => (x.Item.Price * x.Quantity)).Sum();

            ShoppingCartDto dto = new ShoppingCartDto
            {
                Items = allProduct,
                TotalPrice = totalPrice
            };
            return dto;
        }

        public bool order(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.ShoppingCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    CustomerId = userId,
                    Customer = loggedInUser
                };

                _orderRepository.Insert(order);

                List<ItemInOrder> productInOrder = new List<ItemInOrder>();

                var lista = userShoppingCart.ItemInShoppingCarts.Select(
                    x => new ItemInOrder
                    {
                        Id = Guid.NewGuid(),
                        ItemId = x.Item.Id,
                        Item = x.Item,
                        OrderId = order.Id,
                        Order = order,
                        Quantity = x.Quantity
                    }
                    ).ToList();


                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 1; i <= lista.Count(); i++)
                {
                    var currentItem = lista[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Item.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Item.ItemName + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Item.Price);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());

                productInOrder.AddRange(lista);

                foreach (var product in productInOrder)
                {
                    _productInOrderRepository.Insert(product);
                }

                loggedInUser.ShoppingCart.ItemInShoppingCarts.Clear();
                _userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }
        
    }
}

