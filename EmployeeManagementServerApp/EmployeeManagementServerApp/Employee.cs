using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagementServerApp
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string DepartmentId { get; set; }
        public Address Address { get; set; }
    }
}