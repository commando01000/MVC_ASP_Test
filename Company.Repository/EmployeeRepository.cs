using Company.Database.Access.Contexts;
using Company.Database.Access.Entities;
using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly NorthwindContext _context;

        //private readonly IUnitOfWork unitOfWork;

        // inject the unit of work here
        public EmployeeRepository(NorthwindContext context)
        {
            this._context = context;
            //this.unitOfWork = _unitOfWork;
        }
       void IEmployeeRepository.Add(Employee employee)
        {
            this._context.Employees.Add(employee);
            //this._unitOfWork.Commit();
            this._context.SaveChanges();
        }

        public ICollection<Employee> GetEmployeesByName(string name)
        {
            return this._context.Employees.Where(e => e.FirstName.ToLower().Trim().Contains(name.ToLower().Trim()) || e.LastName.ToLower().Trim().Contains(name.ToLower().Trim())).ToList();
        }
       void IEmployeeRepository.Delete(int id)
        {
            this._context.Employees.Remove(this._context.Employees.Find(id));
            this._context.SaveChanges();
        }

        IEnumerable<Employee> IEmployeeRepository.GetAll()
        {
            return this._context.Employees.ToList();
        }

        Employee IEmployeeRepository.GetById(int id)
        {
            return this._context.Employees.Find(id);
        }

        void IEmployeeRepository.Update(Employee employee)
        {
            this._context.Employees.Update(employee);
            this._context.SaveChanges();
        }
    }
}
