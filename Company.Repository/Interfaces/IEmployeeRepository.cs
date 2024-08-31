using Company.Database.Access.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository.Interfaces
{
    public interface IEmployeeRepository
    {
       public IEnumerable<Employee> GetAll();
       public Employee GetById(int id);
       public void Add(Employee employee);
       public void Update(Employee employee);
       public void Delete(int id);
    }
}
