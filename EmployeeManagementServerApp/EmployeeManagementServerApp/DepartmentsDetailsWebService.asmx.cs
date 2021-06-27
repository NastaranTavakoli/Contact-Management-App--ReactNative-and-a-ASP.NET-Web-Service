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
    /// Summary description for DepartmentsDetailsWebService
    /// </summary>
    [WebService(Namespace = "https://www.nas-tavakoli.org/Movie")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DepartmentsDetailsWebService : System.Web.Services.WebService
    {

        public static string GetFilePath(string fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", fileName);
            return Path.GetFullPath(filePath);
        }

        XDocument document = XDocument.Load(GetFilePath("Department.xml"));

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public List<Department> GetDepartments()
        {
            XNamespace ns = document.Root.GetDefaultNamespace();
            List<Department> departmentsList = document.Root.Descendants(ns + "Department")
                .Select(d => new Department()
                {
                    Id = (int)d.Attribute("Id"),
                    Name = (string)d.Element(ns + "Name"),
                }).ToList();

            return departmentsList;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public Department GetDepartmentById(int id)
        {
            XNamespace ns = document.Root.GetDefaultNamespace();
            Department department = document.Root.Descendants(ns + "Department")
                          .Where((d) => (int)d.Attribute("Id") == id)
                          .Select((d) => new Department()
                          {
                              Id = (int)d.Attribute("Id"),
                              Name = (string)d.Element(ns + "Name"),
                          }).FirstOrDefault();

            if (department == null)
            {
                Context.Response.StatusCode = 404;
                return null;
            }
            Context.Response.StatusCode = 200;
            return department;
        }
    }
}
