using EShop.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Interface
{
    public interface IItemService
    {
        List<Item> GetAllItems();
        Item GetDetailsForItems(Guid? id);
        void CreateNewItem(Item p);
        void UpdateExistingItem(Item p);
        void DeleteItem(Guid id);
    }
}
