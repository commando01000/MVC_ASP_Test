using Company.Database.Access.Contexts;
using Company.Database.Access.Entities;
using Company.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
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

        public IQueryable<Customer> getAllCustomers()
        {
            return this._context.Customers.Include(c => c.Orders).AsQueryable();
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

        IQueryable<Order> IOrderRepository.GetAll()
        {
            return this._context.Orders.Include(o => o.Customer).Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation).AsQueryable();
        }

        Order IOrderRepository.GetById(int id)
        {
            return this._context.Orders.Include(o => o.Customer).Include(o => o.Employee)
                .Include(o => o.ShipViaNavigation).AsQueryable().FirstOrDefault(o => o.OrderId == id);
        }

       public ICollection<Shipper> GetShippers()
        {
            return this._context.Shippers.ToList();
        }

        void IOrderRepository.Update(Order order)
        {
            this._context.Orders.Update(order);
            this._context.SaveChanges();
        }
    }
}
