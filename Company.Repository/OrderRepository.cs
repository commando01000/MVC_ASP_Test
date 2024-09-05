using Company.Database.Access.Contexts;
using Company.Database.Access.Entities;
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
        public OrderRepository(NorthwindContext context, NorthwindContextProcedures contextProcedures)
        {
            this._context = context;
            this._contextProcedures = contextProcedures;
        }

        void IOrderRepository.Add(Order order)
        {
            this._context.Orders.Add(order);
            this._context.SaveChanges();
        }

        ICollection<CustOrdersOrdersResult> getOrdersByCustomerId(string customerId)
        {
            //return this._context.Orders
            //                .Where(o => o.CustomerId == customerId)
            //                .ToList();

            return (ICollection<CustOrdersOrdersResult>)this._contextProcedures.CustOrdersOrdersAsync(customerId);
        }

        void IOrderRepository.Delete(int id)
        {
            this._context.Orders.Remove(this._context.Orders.Find(id));
            this._context.SaveChanges();
        }

        IEnumerable<Order> IOrderRepository.GetAll()
        {
            return this._context.Orders.ToList();
        }

        Order IOrderRepository.GetById(int id)
        {
            return this._context.Orders.Find(id);
        }

        void IOrderRepository.Update(Order order)
        {
            this._context.Orders.Update(order);
            this._context.SaveChanges();
        }
    }
}
