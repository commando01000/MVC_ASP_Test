using Company.Database.Access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Interfaces
{
    public interface IOrderService
    {
        IQueryable<Order> GetAll();
        Order GetById(int id);
        void Add(Order order);
        void Update(Order order, int id);
        void Delete(int id);
    }
}
