using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinearHomeworkInterface
{
    public partial class InstructorHome : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Check_User();

            if (Page.IsPostBack)
            {

            }

            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                msqcon.Open();
                //fetch data for assignments table
                String query = "SELECT h.title, h.dueDate FROM homework AS h";
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                MySqlDataReader assignments = null;
                assignments = msqcmd.ExecuteReader();
                //build table
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

                query = "SELECT u.first, u.last, u.userId FROM user AS u WHERE u.role='S' ORDER BY u.last";
                msqcmd = new MySqlCommand(query, msqcon);
                MySqlDataReader students = msqcmd.ExecuteReader();

                sb = new StringBuilder();
                while (students.Read())
                {
                    sb.Append("<option value=\"" + students.GetString(2) + "\">");
                    sb.Append(students.GetString(1) + ", " + students.GetString(0));
                    sb.Append("</option>");
                }
                StudentListLiteral.Text = sb.ToString();
                students.Close();
                /*
                //student grade junk
                msqcon.Open();
                query = "SELECT h.title, ha.grade FROM hmwkassignment AS ha JOIN homework AS h WHERE ha.homeworkId=h.homeworkid AND ha.userID = @userid ORDER BY h.homeworkid";
                msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@userid", studentNameDropdown.Value));
                MySqlDataReader studentGrades = null;
                assignments = msqcmd.ExecuteReader();
                //build table
                sb = new StringBuilder();
                while (studentGrades.Read())
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
                //StudentGradeLiteral.Text = sb.ToString();
                studentGrades.Close();
                */

                msqcon.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected void Check_User()
        {
            HttpCookie cookie = Request.Cookies["LINALGHW"];
            FormsAuthenticationTicket t = null;
            string[] user = null;

            if (cookie != null)
            {
                t = FormsAuthentication.Decrypt(cookie.Value);
                user = t.UserData.ToString().Split(' ');
            }

            if (user == null)
            {
                Server.Transfer("Default.aspx", true);
            }
            else if (user[0].Equals("S"))
            {
                Server.Transfer("StudentHome.aspx", true);
            }
        }

        [WebMethod]
        protected static void UpdateStudentGradeTable(String userId)
        {
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}