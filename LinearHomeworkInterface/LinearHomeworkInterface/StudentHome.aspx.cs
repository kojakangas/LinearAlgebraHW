using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Text;

namespace LinearHomeworkInterface
{
    public partial class StudentHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Check_User();

            String username = Context.User.Identity.Name;

            StringBuilder sb = new StringBuilder();
            sb.Append("<h1>Welcome, ");
            sb.Append(username);
            sb.Append("</h1>");
            headerltData.Text = sb.ToString();

            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                msqcon.Open();
                String idquery = "SELECT user.userID From user WHERE user.username=@username";
                MySqlCommand msqcmd = new MySqlCommand(idquery, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@username", username));
                MySqlDataReader idfetch = msqcmd.ExecuteReader();
                idfetch.Read();
                String userid = idfetch.GetString(0);
                idfetch.Close();

                String query = "SELECT ha.assignmentID, h.homeworkid, h.dueDate, ha.grade, ha.status, ha.userId, h.title FROM hmwkassignment AS ha JOIN homework AS h WHERE ha.homeworkId=h.homeworkid AND ha.userID = @userid ORDER BY ha.assignmentID";
                msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@userid", userid));
                MySqlDataReader assignments = null;
                assignments = msqcmd.ExecuteReader();
                sb = new StringBuilder();
                while (assignments.Read())
                {
                    /*
                    HtmlTableRow tRow = new HtmlTableRow();
                    HtmlTableCell hwName = new HtmlTableCell();
                    hwName.InnerText = assignments.GetString(0);
                    tRow.Controls.Add(hwName);
                    HtmlTableCell hwDueDate = new HtmlTableCell();
                    hwDueDate.InnerText = assignments.GetString(2);
                    tRow.Controls.Add(hwDueDate);
                    HtmlTableCell hwGrade = new HtmlTableCell();
                    hwGrade.InnerText = assignments.GetString(3);
                    tRow.Controls.Add(hwGrade);
                    assignmentTable.Rows.Add(tRow);
                    HtmlTableCell hwStatus = new HtmlTableCell();
                    hwStatus.InnerText = assignments.GetString(4);
                    tRow.Controls.Add(hwStatus);
                    assignmentTable.Rows.Add(tRow);
                    */

                    sb.Append("<tr>");
                    sb.Append("<td style=\"text-align: left;\">");
                    sb.Append(assignments.GetString(6));
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align: center;\">");
                    sb.Append(assignments.GetString(2));
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align: center;\">");
                    sb.Append(assignments.GetString(3));
                    sb.Append("</td>");
                    sb.Append("<td style=\"text-align: center;\">");
                    sb.Append(assignments.GetString(4));
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

            if (t == null)
            {
                Server.Transfer("Default.aspx", true);
            }
            else if (user[0].Equals("I"))
            {
                Server.Transfer("InstructorHome.aspx", true);
            }
        }
    }
}