using Comany.Database.Access.Contexts;
using Comany.Database.Access.Entities;
using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NorthwindContext _context;
        public OrderRepository(NorthwindContext context)
        {
            this._context = context;
        }

        bool IOrderRepository.CreateOrder(Order order)
        {
            this._context.Orders.Add(order);
            return this._context.SaveChanges() > 0;
        }

        bool IOrderRepository.DeleteOrder(Order order)
        {
            this._context.Orders.Remove(order);
            return this._context.SaveChanges() > 0;
        }

        ICollection<Order> IOrderRepository.GetOrders()
        {
            return this._context.Orders.ToList();
        }

        ICollection<Order> IOrderRepository.GetOrdersByCustomerId(string customerId)
        {
            return this._context.Orders.Where(x => x.CustomerId == customerId).ToList();
        }

        ICollection<Order> IOrderRepository.GetOrdersByEmployeeId(int employeeId)
        {
            
        }

        bool IOrderRepository.Save()
        {
            
        }

        bool IOrderRepository.UpdateOrder(Order order)
        {
            
        }
    }
}
