using Company.Database.Access.Entities;
using Company.Repository;
using Company.Repository.Interfaces;
using Company.Service.Helper;
using Company.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        void IEmployeeService.Add(Employee employee)
        {
            // Validation: Ensure employee is not null
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
            }

            // Additional validation: Ensure mandatory fields are filled (e.g., FirstName, LastName, etc.)
            if (string.IsNullOrWhiteSpace(employee.FirstName))
            {
                throw new ArgumentException("First Name is required.", nameof(employee.FirstName));
            }

            if (string.IsNullOrWhiteSpace(employee.LastName))
            {
                throw new ArgumentException("Last Name is required.", nameof(employee.LastName));
            }

            // Map Employee entity (manual mapping if needed)
            var employeeEntity = new Employee
            {
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Notes = employee.Notes,
                ReportsTo = employee.ReportsTo,
                PhotoPath = DocumentSettings.UploadFile("Files/Images", employee.Photo),
            };

            // Call the repository to add the employee
            _employeeRepository.Add(employeeEntity);
        }

        void IEmployeeService.Delete(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Employee> IEmployeeService.GetAll()
        {
            var employees = _employeeRepository.GetAll();
            return employees;
        }

        Employee IEmployeeService.GetById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        ICollection<Employee> IEmployeeService.GetEmployeesByName(string name)
        {
            EmployeeRepository EmpRepo = (EmployeeRepository)_employeeRepository;
            return EmpRepo.GetEmployeesByName(name);
        }

        void IEmployeeService.Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
