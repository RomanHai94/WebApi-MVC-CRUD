using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class EmployeesController : ApiController
    {
        private DBModels db = new DBModels();

        public IEnumerable<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }
        public Employee GetGetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            return employee;
        }
        [HttpPost]
        public void CreateEmployee([FromBody] Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }
        [HttpPut]
        public void EditEmployee(int id, [FromBody] Employee employee)
        {
            if (id == employee.EmployeeID)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
            }
        }
        [HttpDelete]
        public void DeleteEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee != null)
            {
                db.Employees.Remove(employee);
                db.SaveChanges();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
