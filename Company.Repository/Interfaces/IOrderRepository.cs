using Company.Database.Access.Entities;

namespace Company.Repository.Interfaces
{
    public interface IOrderRepository
    {
        IQueryable<Order> GetAll();
        Order GetById(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
    }
}
