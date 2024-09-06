using Company.Database.Access.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
