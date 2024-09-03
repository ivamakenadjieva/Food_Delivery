﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Domain
{
    public class Restaurant : BaseEntity
    {
        public String Name { get; set; }
        public String Address { get; set; }

        public string? RestaurantImage { get; set; }
        public ICollection<ItemInRestaurant>? Items { get; set; }
    }
}
