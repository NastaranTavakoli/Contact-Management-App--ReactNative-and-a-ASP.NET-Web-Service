using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeManagementServerApp
{
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string XmlFilePath = Path.Combine("Data", "Department.xml");
            string XsltFilePath = Path.Combine("Data", "Department.xslt");

            departmentsXml.DocumentContent = File.ReadAllText(Server.MapPath(XmlFilePath));
            departmentsXml.TransformSource = Server.MapPath(XsltFilePath);
        }
    }
}