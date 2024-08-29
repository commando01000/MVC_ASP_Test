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
        private readonly  NorthwindContextProcedures _contextProcedures;
        public OrderRepository(NorthwindContext context)
        {
            this._context = context;
        }

        bool IOrderRepository.CreateOrder(Order order)
        {
            this._context.Orders.Add(order);
            return true;
        }

        bool IOrderRepository.DeleteOrder(Order order)
        {
            this._context.Orders.Remove(order);
            return true;
        }

        ICollection<Order> IOrderRepository.GetOrders()
        {
            return this._context.Orders.ToList();
        }

        ICollection<Order> IOrderRepository.CustOrdersOrdersAsync(string customerId)
        {
            return this._context.Orders
                            .Where(o => o.CustomerId == customerId)
                            .ToList();
        }

        bool IOrderRepository.UpdateOrder(Order order)
        {
            this._context.Orders.Update(order);
            return true;
        }
    }
}
