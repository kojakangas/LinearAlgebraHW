﻿using MySql.Data.MySqlClient;
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

            //fetch username
            String username = Context.User.Identity.Name;

            //set name for page header
            String headername = username;
            HttpCookie cookie = Request.Cookies["LINALGHW"];
            FormsAuthenticationTicket t = null;
            string[] user = null;

            if (cookie != null)
            {
                t = FormsAuthentication.Decrypt(cookie.Value);
                user = t.UserData.ToString().Split(' ');
                if ((!user[1].Equals(null)) && (!user[2].Equals(null)))
                {
                    headername = user[1] + " " + user[2];
                }
            }

            //make header
            StringBuilder sb = new StringBuilder();
            sb.Append("<h1>Welcome, ");
            sb.Append(headername);
            sb.Append("</h1>");
            headerltData.Text = sb.ToString();

            //set up connection, do stuff
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                //fetch user id from username
                msqcon.Open();
                String idquery = "SELECT user.userID From user WHERE user.username=@username";
                MySqlCommand msqcmd = new MySqlCommand(idquery, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@username", username));
                MySqlDataReader idfetch = msqcmd.ExecuteReader();
                idfetch.Read();
                String userid = idfetch.GetString(0);
                idfetch.Close();

                String hmwkquery = "UPDATE linearhmwkdb.homework SET status = 'Complete' WHERE dueDate <= NOW()";
                msqcmd = new MySqlCommand(hmwkquery, msqcon);
                MySqlDataReader hmwkqrycmd = msqcmd.ExecuteReader();
                hmwkqrycmd.Read();
                hmwkqrycmd.Close();

                String hmwkassignments = "UPDATE linearhmwkdb.hmwkassignment hw SET status = 'Late' WHERE (SELECT status from linearhmwkdb.homework where homeworkId = hw.homeworkId) = 'Complete' && status != 'Complete'";
                msqcmd = new MySqlCommand(hmwkassignments, msqcon);
                MySqlDataReader hmwkassqrycmd = msqcmd.ExecuteReader();
                hmwkassqrycmd.Read();
                hmwkassqrycmd.Close();

                //fetch current homework assignments
                String query = "SELECT distinct h.title, h.dueDate, ha.grade, ha.status, ha.assignmentId, h.points, ha.currentQuestion, q.type, h.homeworkid FROM hmwkassignment AS ha JOIN homework AS h JOIN question AS q WHERE ha.homeworkId=h.homeworkid AND ha.userID = @userid AND q.number = ha.currentQuestion AND q.homeworkid = h.homeworkid ORDER BY h.dueDate DESC";
                msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@userid", userid));
                MySqlDataReader assignments = null;
                assignments = msqcmd.ExecuteReader();
                //build contents of table to display assignments
                sb = new StringBuilder();
                //initialize point totals for assignments
                float studentTotal = 0;
                int homeworkTotal = 0;
                while (assignments.Read())
                {
                    //increment overal totals
                    studentTotal += float.Parse(assignments.GetString(2));
                    homeworkTotal += int.Parse(assignments.GetString(5));
                    //find out if current assignment is available to work
                    bool available = false;
                    /*if(0>System.DateTime.Compare(System.DateTime.Now,assignments.GetDateTime(1))){*/
                    if ((assignments.GetString(3).Equals("Assigned") || assignments.GetString(3).Equals("In Progress")))
                    {
                        available = true;
                    }

                    sb.Append("<tr>");
                    //title, is a link if available to work currently
                    sb.Append("<td style=\"text-align: left;\">");
                    if (available)
                    {
                        if (assignments.GetString(7) == "SoE")
                        {
                            sb.Append("<a class=\"homeworkLink\" href = \"QuestionPage.aspx?assign=" + assignments.GetString(4) + "&question=" + assignments.GetString(6) + "\">");
                        }

                        if (assignments.GetString(7) == "I")
                        {
                            sb.Append("<a class=\"homeworkLink\" href = \"QuestionInverse.aspx?assign=" + assignments.GetString(4) + "&question=" + assignments.GetString(6) + "\">");
                        }

                        if (assignments.GetString(7) == "RR")
                        {
                            sb.Append("<a class=\"homeworkLink\" href = \"ReducedRow.aspx?assign=" + assignments.GetString(4) + "&question=" + assignments.GetString(6) + "\">");
                        }

                        if (assignments.GetString(7) == "ID")
                        {
                            sb.Append("<a class=\"homeworkLink\" href = \"QuestionLinearDependence.aspx?assign=" + assignments.GetString(4) + "&question=" + assignments.GetString(6) + "\">");
                        }
                            
                        sb.Append(assignments.GetString(0));
                        sb.Append("</a>");
                    }
                    else
                    {
                        sb.Append(assignments.GetString(0));
                    }
                    sb.Append("</td>");
                    //due date
                    sb.Append("<td style=\"text-align: center;\">");
                    sb.Append(assignments.GetString(1));
                    sb.Append("</td>");
                    //status
                    sb.Append("<td style=\"text-align: center;\">");
                    sb.Append(assignments.GetString(3));
                    sb.Append("</td>");
                    //grade
                    sb.Append("<td style=\"text-align: center;\">");
                    sb.Append(assignments.GetString(2));
                    sb.Append('/' + assignments.GetString(5));
                    sb.Append("</td>");

                    sb.Append("</tr>");
                }
                sb.Append("<tr>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style=\"text-align: center; font-weight: bold;\">");
                sb.Append("Total:");
                sb.Append("</td>");
                sb.Append("<td style=\"text-align: center; font-weight: bold;\">");
                sb.Append(studentTotal.ToString("0.00"));
                sb.Append('/' + homeworkTotal.ToString());
                sb.Append("</td>");
                sb.Append("</tr>");

                ltData.Text = sb.ToString();
                assignments.Close();
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