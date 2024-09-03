using EShop.Domain.Domain;
using EShop.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteItemFromShoppingCart(string userId, Guid itemId);
        bool order(string userId);
        bool AddToShoppingConfirmed(ItemInShoppingCart model, string userId);
    }
}
