﻿using EShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Domain
{
    public class Order : BaseEntity
    {
        public String CustomerId { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<ItemInOrder> ItemsInOrder { get; set; }
    }
}
