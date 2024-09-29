using Company.Database.Access.Contexts;

namespace Company.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public IOrderRepository OrderRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public INorthwindContextProcedures NorthwindContextProcedures { get; set; }
        bool Commit();
    }
}
