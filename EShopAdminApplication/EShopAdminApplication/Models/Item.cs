using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopAdminApplication.Models
{
    public class Item : BaseEntity
    {
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
        public Restaurant? Restaurant { get; set; }
        public Guid? RestaurantId { get; set; }
        public virtual ICollection<ItemInShoppingCart>? ItemInShoppingCarts { get; set; }
        public virtual IEnumerable<ItemInOrder>? ItemsInOrder { get; set; }
        public virtual ICollection<ItemInRestaurant>? ItemInRestaurants { get; set; }

    }
}
