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

namespace LinearHomeworkInterface
{
    public partial class StudentHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                msqcon.Open();
                String username = Context.User.Identity.Name;
                String idquery = "SELECT user.userID From user WHERE user.username=@username";
                MySqlCommand msqcmd = new MySqlCommand(idquery, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@username", username));
                MySqlDataReader idfetch = msqcmd.ExecuteReader();
                idfetch.Read();
                String userid = idfetch.GetString(0);
                idfetch.Close();

                String query = "SELECT ha.assignmentID, h.homeworkid, h.dueDate, ha.grade, ha.status, ha.userId FROM hmwkassignment AS ha JOIN homework AS h WHERE ha.homeworkId=h.homeworkid AND ha.userID = @userid ORDER BY ha.assignmentID";
                msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@userid", userid));
                MySqlDataReader assignments = null;
                assignments = msqcmd.ExecuteReader();
                while (assignments.Read())
                {
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
                }
                assignments.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [WebMethod]
        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}