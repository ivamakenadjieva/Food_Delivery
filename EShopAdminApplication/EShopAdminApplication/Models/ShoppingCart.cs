
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopAdminApplication.Models
{
    public class ShoppingCart : BaseEntity
    {
        public string? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public virtual ICollection<ItemInShoppingCart>? ItemInShoppingCarts { get; set; }

    }
}
