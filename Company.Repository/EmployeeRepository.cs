﻿using Company.Database.Access.Contexts;
using Company.Database.Access.Entities;
using Company.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly NorthwindContext _context;
        public EmployeeRepository(NorthwindContext context)
        {
            this._context = context;
        }
       void IEmployeeRepository.Add(Employee employee)
        {
            this._context.Employees.Add(employee);
            this._context.SaveChanges();
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
