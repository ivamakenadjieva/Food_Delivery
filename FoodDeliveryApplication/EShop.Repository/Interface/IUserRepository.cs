using EShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<Customer> GetAll();
        Customer Get(string? id);
        void Insert(Customer entity);
        void Update(Customer entity);
        void Delete(Customer entity);
    }
}
