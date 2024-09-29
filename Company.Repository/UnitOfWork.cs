using Company.Database.Access.Contexts;
using Company.Repository.Interfaces;

namespace Company.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IOrderRepository OrderRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public INorthwindContextProcedures NorthwindContextProcedures { get; set; }

        private readonly NorthwindContext _context;
        private INorthwindContextProcedures _contextProcedures;
        public UnitOfWork(NorthwindContext _context, NorthwindContextProcedures _contextProcedures)
        {
            this._context = _context;
            this._contextProcedures = _contextProcedures;
            OrderRepository = new OrderRepository(_context, _contextProcedures);
            EmployeeRepository = new EmployeeRepository(_context);
        }

        bool IUnitOfWork.Commit()
        {
            this._context.SaveChanges();
            return true;
        }
    }
}
