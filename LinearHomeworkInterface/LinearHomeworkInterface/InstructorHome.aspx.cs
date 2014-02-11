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
using System.Globalization;

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
                String query = "SELECT h.title, h.dueDate, h.homeworkid FROM homework AS h";
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                MySqlDataReader assignments = null;
                assignments = msqcmd.ExecuteReader();
                //build table
                StringBuilder sb = new StringBuilder();
                while (assignments.Read())
                {
                    char[] sep = { ' ' };
                    String[] splitDate = assignments.GetString(1).Split(sep);
                    DateTime dueDate = DateTime.Parse(splitDate[0]);

                    sb.Append("<tr>");
                    sb.Append("<td>");
                    sb.Append(assignments.GetString(0));
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append("<input type=\"text\" onkeypress=\"return validateNoInput(event)\" id=\"" + assignments.GetString(2) + "\" style=\"width: 80px; padding: 0px; margin-bottom: 0px;\" value=\"" + dueDate.ToString("yyyy-MM-dd") + "\" class=\"datepicker\">");
                    sb.Append("</td>");
                    sb.Append("<td>");
                    sb.Append("<a id=\"delete\" name=\"" + assignments.GetString(2) + "\" style=\"cursor: pointer;\">Delete</a>");
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
        public static String[][] UpdateStudentGradeTable(String UserID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                msqcon.Open();
                String query = "SELECT h.title, ha.grade, h.points, ha.status FROM hmwkassignment AS ha JOIN homework AS h WHERE ha.homeworkId=h.homeworkid AND ha.userID = @userid ORDER BY h.homeworkid";
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@userid", UserID));
                MySqlDataReader studentGrades = null;
                studentGrades = msqcmd.ExecuteReader();
                //build table
                StringBuilder sb = new StringBuilder();
                if(studentGrades.HasRows){
                    while (studentGrades.Read())
                    {
                        sb.Append(studentGrades.GetString(0));
                        sb.Append(',');
                        sb.Append(studentGrades.GetString(3));
                        sb.Append(',');
                        sb.Append(studentGrades.GetString(1));
                        sb.Append('/'+studentGrades.GetString(2));
                        sb.Append(';');
                    }
                    studentGrades.Close();
                    String result = sb.ToString();
                    result = result.Remove(result.Length - 1);
                    String[] strings = result.Split(';');
                    String[][] strings2 = new String[strings.Length][];
                    for (int i = 0; i < strings.Length; i++)
                    {
                        strings2[i] = strings[i].Split(',');
                    }
                    return strings2;
                }
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [WebMethod]
        public static void UpdateDueDate(String DueDate, String homeworkid)
        {
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                msqcon.Open();
                String query = "UPDATE homework set dueDate = '" + DueDate + " 23:59:59' where homeworkid= " + homeworkid;
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}