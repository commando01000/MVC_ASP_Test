using Company.Database.Access.Contexts;
using Company.Database.Access.Entities;
using Company.Repository;
using Company.Repository.Interfaces;
using Company.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _orderRepository;
        private readonly NorthwindContextProcedures _contextProcedures;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        void IOrderService.Add(Order order)
        {
            // Ensure all fields are correctly mapped from the incoming Order object
            Order userOrder = new Order
            {
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                ShipVia = order.ShipVia,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipRegion = order.ShipRegion,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry
            };

            this._orderRepository.Add(userOrder);
        }

        void IOrderService.Delete(int id)
        {
            this._orderRepository.Delete(id);
        }

        IQueryable<Order> IOrderService.GetAll()
        {
            return this._orderRepository.GetAll();
        }

        public IQueryable <Customer> getAllCustomers()
        {
           OrderRepository Order_Repo = (OrderRepository) this._orderRepository;
            return Order_Repo.getAllCustomers();
        }

        Order IOrderService.GetById(int id)
        {
            return this._orderRepository.GetById(id);
        }
        public ICollection<CustOrdersOrdersResult> getOrdersByCustomerId(string customerId)
        {
            return (ICollection<CustOrdersOrdersResult>)this._contextProcedures.CustOrdersOrdersAsync(customerId);
        }
        void IOrderService.Update(Order order, int id)
        {
            // Retrieve the existing order from the repository by ID
            var existingOrder = _orderRepository.GetById(id); 

            if (existingOrder != null && existingOrder.OrderId == id)
            {
                // Update only the fields that are allowed to be changed
                existingOrder.CustomerId = order.CustomerId;
                existingOrder.EmployeeId = existingOrder.EmployeeId;
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.RequiredDate = order.RequiredDate;
                existingOrder.ShippedDate = order.ShippedDate;
                existingOrder.ShipVia = order.ShipVia;
                existingOrder.ShipName = order.ShipName;
                existingOrder.ShipAddress = order.ShipAddress;
                existingOrder.ShipCity = order.ShipCity;
                existingOrder.ShipRegion = order.ShipRegion;
                existingOrder.ShipPostalCode = order.ShipPostalCode;
                existingOrder.ShipCountry = order.ShipCountry;

                // Save the changes back to the repository
                _orderRepository.Update(existingOrder);
            }
            else
            {
                // Handle the case where the order was not found
                throw new KeyNotFoundException("Order not found.");
            }
        }
        ICollection<Shipper> IOrderService.GetShippers()
        {
            OrderRepository Order_Repo = (OrderRepository)this._orderRepository;
            return Order_Repo.GetShippers();
        }
    }
}
