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
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _itemRepository;
        private readonly IRepository<ItemInShoppingCart> _itemInShoppingCartRepository;
        private readonly IUserRepository _userRepository;

        public ItemService (IRepository<Item> itemRepository, IRepository<ItemInShoppingCart> itemInShoppingCartRepository, IUserRepository userRepository)
        {
            _itemRepository = itemRepository;
            _itemInShoppingCartRepository = itemInShoppingCartRepository;
            _userRepository = userRepository;
        }

        public void CreateNewItem(Item p)
        {
            _itemRepository.Insert(p);
        }

        public void DeleteItem(Guid id)
        {
            var product = _itemRepository.Get(id);
            _itemRepository.Delete(product);
        }

        public List<Item> GetAllItems()
        {
            return _itemRepository.GetAll().ToList();
        }

        public Item GetDetailsForItems(Guid? id)
        {
            var product = _itemRepository.Get(id);
            return product;
        }

        public void UpdateExistingItem(Item p)
        {
            _itemRepository.Update(p);
        }
    }
}
