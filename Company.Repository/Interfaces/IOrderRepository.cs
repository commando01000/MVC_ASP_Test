using Comany.Database.Access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();

        ICollection<Order> GetOrdersByEmployeeId(int employeeId);
        ICollection<Order> GetOrdersByCustomerId(string customerId);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        bool Save();
    }
}
