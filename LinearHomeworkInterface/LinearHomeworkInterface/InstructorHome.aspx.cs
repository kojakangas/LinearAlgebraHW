using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinearHomeworkInterface
{
    public partial class InstructorHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                msqcon.Open();
                String query = "SELECT h.title, h.dueDate FROM homework AS h";
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                MySqlDataReader assignments = null;
                assignments = msqcmd.ExecuteReader();
                StringBuilder sb = new StringBuilder();
                while (assignments.Read())
                {
                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append(assignments.GetString(0));
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append(assignments.GetString(1));
                    sb.Append("</td>");

                    sb.Append("</tr>");
                }
                ltData.Text = sb.ToString();
                assignments.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}