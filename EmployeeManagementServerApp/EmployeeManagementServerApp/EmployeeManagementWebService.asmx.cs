using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml.Linq;

namespace EmployeeManagementServerApp
{
    /// <summary>
    /// Summary description for EmployeeManagementWebService
    /// </summary>
    [WebService(Namespace = "https://www.nas-tavakoli.org/employees")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class EmployeeManagementWebService : System.Web.Services.WebService
    {

        public static string GetFilePath(string fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);
            return Path.GetFullPath(filePath);
        }

        XDocument document = XDocument.Load(GetFilePath("Employee.xml"));

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public List<Employee> GetEmployees()
        {
            XNamespace ns = document.Root.GetDefaultNamespace();
            List<Employee> employeesList = document.Root.Descendants(ns + "Employee")
                .Select(e => new Employee()
                {
                    Id = (int)e.Attribute("Id"),
                    Name = (string)e.Element(ns + "Name"),
                    Phone = (string)e.Element(ns + "Phone"),
                    DepartmentId = (string)e.Element(ns + "DepartmentId"),
                    Address = new Address()
                    {
                        Street = (string)e.Element(ns + "Address").Element(ns + "Street"),
                        City = (string)e.Element(ns + "Address").Element(ns + "City"),
                        State = (string)e.Element(ns + "Address").Element(ns + "State"),
                        Zip = (string)e.Element(ns + "Address").Element(ns + "ZIP"),
                        Country = (string)e.Element(ns + "Address").Element(ns + "Country")
                    }
                }).ToList();
            return employeesList;
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public Employee GetEmployee(int id)
        {
            XNamespace ns = document.Root.GetDefaultNamespace();
            Employee employee = document.Root.Descendants(ns + "Employee")
                          .Where((e) => (int)e.Attribute("Id") == id)
                          .Select((e) => new Employee()
                          {
                              Id = (int)e.Attribute("Id"),
                              Name = (string)e.Element(ns + "Name"),
                              Phone = (string)e.Element(ns + "Phone"),
                              DepartmentId = (string)e.Element(ns + "DepartmentId"),
                              Address = new Address()
                              {
                                  Street = (string)e.Element(ns + "Address").Element(ns + "Street"),
                                  City = (string)e.Element(ns + "Address").Element(ns + "City"),
                                  State = (string)e.Element(ns + "Address").Element(ns + "State"),
                                  Zip = (string)e.Element(ns + "Address").Element(ns + "ZIP"),
                                  Country = (string)e.Element(ns + "Address").Element(ns + "Country")
                              }
                          }).FirstOrDefault();

            if (employee == null)
            {
                Context.Response.StatusCode = 404;
                return null;
            }
            Context.Response.StatusCode = 200;
            return employee;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void AddEmployee(string name, string phone, int departmentId, string street, string city, string state, string zip, string country)
        {

            XNamespace ns = document.Root.GetDefaultNamespace();
            int lastId = (from e in document.Root.Descendants(ns + "Employee")
                          select (int)e.Attribute("Id"))
                            .Max();
            XElement employeeElement = new XElement(ns + "Employee");
            XElement addressElement = new XElement(ns + "Address");
            addressElement.Add(
                new XElement(ns + "Street", street),
                new XElement(ns + "City", city),
                new XElement(ns + "State", state),
                new XElement(ns + "ZIP", zip),
                new XElement(ns + "Country", country)
            );

            employeeElement.SetAttributeValue("Id", lastId + 1);
            employeeElement.Add(
                new XElement(ns + "Name", name),
                new XElement(ns + "Phone", phone),
                new XElement(ns + "DepartmentId", departmentId),
                addressElement
            );

            document.Root.Add(employeeElement);
            document.Save(GetFilePath("Employee.xml"));

            Context.Response.StatusCode = 201;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void UpdateEmployee(int id, string name, string phone, int? departmentId, string street, string city, string state, string zip, string country)
        {

            XNamespace ns = document.Root.GetDefaultNamespace();
            XElement employee = document.Root.Descendants(ns + "Employee")
                .Where((e) => (int)e.Attribute("Id") == id)
                .FirstOrDefault();

            XElement address = employee.Element(ns + "Address");

            if (employee == null)
            {
                Context.Response.StatusCode = 404;
                return;
            }
            if (name != "")
            {
                employee.SetElementValue(ns + "Name", name);
            }
            if (phone != "")
            {
                employee.SetElementValue(ns + "Phone", phone);
            }
            if (departmentId != null)
            {
                employee.SetElementValue(ns + "DepartmentId", departmentId);
            }
            if (street != "")
            {
                address.SetElementValue(ns + "Street", street);
            }
            if (city != "")
            {
                address.SetElementValue(ns + "City", city);
            }
            if (state != "")
            {
                address.SetElementValue(ns + "State", state);
            }
            if (zip != "")
            {
                address.SetElementValue(ns + "ZIP", zip);
            }
            if (country != "")
            {
                address.SetElementValue(ns + "Country", country);
            }
            document.Save(GetFilePath("Employee.xml"));
            Context.Response.StatusCode = 200;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = false, ResponseFormat = ResponseFormat.Json)]
        public void DeleteEmployee(int id)
        {
            XNamespace ns = document.Root.GetDefaultNamespace();
            XElement employee = document.Root.Descendants(ns + "Employee")
                .Where((e) => (int)e.Attribute("Id") == id)
                .FirstOrDefault();

            if (employee == null)
            {
                Context.Response.StatusCode = 404;
                return;
            }
            employee.Remove();
            document.Save(GetFilePath("Employee.xml"));
            Context.Response.StatusCode = 200;
        }
    }
}
