using Company.Database.Access.Entities;

namespace Company.Service.Interfaces
{
    public interface IOrderService
    {
        IQueryable<Order> GetAll();
        Order GetById(int id);
        void Add(Order order);
        void Update(Order order, int id);
        void Delete(int id);
        public IQueryable<Customer> getAllCustomers();
        public ICollection<Shipper> GetShippers();
    }
}
