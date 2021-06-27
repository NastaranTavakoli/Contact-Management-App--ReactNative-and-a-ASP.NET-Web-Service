using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagementServerApp
{
    public partial class Employees : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string XmlFilePath = Path.Combine("Data", "Employee.xml");
            string XsltFilePath = Path.Combine("Data", "Employee.xslt");

            employeesXml.DocumentContent = File.ReadAllText(Server.MapPath(XmlFilePath));
            employeesXml.TransformSource = Server.MapPath(XsltFilePath);
        }
    }
}