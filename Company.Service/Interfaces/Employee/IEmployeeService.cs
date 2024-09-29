using Company.Database.Access.Entities;

namespace Company.Service.Interfaces
{
    public interface IEmployeeService
    {
        public IEnumerable<Employee> GetAll();
        public Employee GetById(int id);
        public void Add(Employee employee);
        public void Update(Employee employee);
        public void Delete(int id);
        public ICollection<Employee> GetEmployeesByName(string name);
    }
}
