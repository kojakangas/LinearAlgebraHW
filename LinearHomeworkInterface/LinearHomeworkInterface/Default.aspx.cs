using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Security;

namespace LinearHomeworkInterface
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            
                msqcon.Open();
                MySqlDataReader usernameCount = null;
                String query = "Select * from user where username = 'ssigman'";
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                usernameCount = msqcmd.ExecuteReader();


                msqcon.Close();
            
        }

        //our WebMethod for checking the user's solution(s)
        [WebMethod]
        public static bool CheckUsername(String username)
        {
            bool isUsernameFree = false;

            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                msqcon.Open();
                MySqlDataReader usernameCount = null;
                String query = "Select * from user where username = '" + username + "'";
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                usernameCount = msqcmd.ExecuteReader();

                if (!usernameCount.HasRows)
                {
                    isUsernameFree = true;
                }

                msqcon.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return isUsernameFree;
        }

        [WebMethod]
        public static void CreateAccount(String UserDetails)
        {
            string[] details = UserDetails.Split(' ');

            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                //here the access code determines the role of the new account
                String newaccrole = null;
                if (details[4].Equals("DU2014"))
                {
                    newaccrole = "student";
                }
                else if (details[4].Equals("DU1337"))
                {
                    newaccrole = "instructor";
                }
                //instance variable needed to check our homework table
                int hwcheck = 0;
                //instance variable needed to check our homework assignments table
                int hwscheck = 0;
                //instance variable needed to check our user table
                int users = 0;
                //assignment id stuff
                int aid;
                //user id stuff
                int usid;
                msqcon.Open();
                //check how many rows we have in our users table
                MySqlCommand msqcheck = new MySqlCommand("SELECT COUNT(*) FROM user", msqcon);
                MySqlDataReader numusbook = msqcheck.ExecuteReader();
                numusbook.Read();
                users = System.Convert.ToInt32(numusbook["COUNT(*)"]);
                numusbook.Close();
                //if we have users already
                if (users > 0)
                {
                    String check = "SELECT userId FROM user ORDER BY userId DESC LIMIT 1";
                    msqcheck = new MySqlCommand(check, msqcon);
                    MySqlDataReader usbook = msqcheck.ExecuteReader();
                    usbook.Read();
                    usid = System.Convert.ToInt32(usbook["userId"]) + 1;
                    usbook.Close();
                }
                //otherwise we do not have users and we are creating our first user
                else
                {
                    usid = 1;
                }
                //according to the role assigned to the account we modulate the query to enter that kind of account into the database
                String query = null;
                if (newaccrole.Equals("student"))
                {
                    query = "insert into user (userId, username, first, last, password, role) values (@userId, @Username, @First, @Last, SHA(@Password), 'S')";
                }
                else if (newaccrole.Equals("instructor"))
                {
                    query = "insert into user (userId, username, first, last, password, role) values (@userId, @Username, @First, @Last, SHA(@Password), 'I')";
                }
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@userId", usid));
                msqcmd.Parameters.Add(new MySqlParameter("@Username", details[0]));
                msqcmd.Parameters.Add(new MySqlParameter("@First", details[1]));
                msqcmd.Parameters.Add(new MySqlParameter("@Last", details[2]));
                msqcmd.Parameters.Add(new MySqlParameter("@Password", details[3]));
                msqcmd.ExecuteNonQuery();
                //if we made a student account
                if (newaccrole.Equals("student"))
                {
                    //we now check how many rows we have in our homework table
                    msqcheck = new MySqlCommand("SELECT COUNT(*) FROM homework", msqcon);
                    MySqlDataReader numhwsbook = msqcheck.ExecuteReader();
                    numhwsbook.Read();
                    hwcheck = System.Convert.ToInt32(numhwsbook["COUNT(*)"]);
                    numhwsbook.Close();
                    //and check how many rows we have in our hmwkassignments table
                    msqcheck = new MySqlCommand("SELECT COUNT(*) FROM hmwkassignment", msqcon);
                    MySqlDataReader numhwsabook = msqcheck.ExecuteReader();
                    numhwsabook.Read();
                    hwscheck = System.Convert.ToInt32(numhwsabook["COUNT(*)"]);
                    numhwsabook.Close();
                    //if we have homework assignments
                    if (hwcheck > 0)
                    {
                        //first we need to fetch the latest homeworkid
                        query = "SELECT * FROM homework ORDER BY homeworkid DESC LIMIT 1";
                        msqcmd = new MySqlCommand(query, msqcon);
                        MySqlDataReader hwidbook = msqcmd.ExecuteReader();
                        hwidbook.Read();
                        //set hwid to the latest homeworkid from homework table
                        int hwid = System.Convert.ToInt32(hwidbook["homeworkid"]);
                        //close our DataReader for this operation
                        hwidbook.Close();
                        //if there are homework assignments
                        if (hwscheck > 0)
                        {
                            query = "SELECT * FROM hmwkassignment ORDER BY assignmentId DESC LIMIT 1";
                        
                            msqcmd = new MySqlCommand(query, msqcon);
                            MySqlDataReader aidbook = msqcmd.ExecuteReader();
                            aidbook.Read();
                            aid = (System.Convert.ToInt32(aidbook["assignmentId"]) + 1);
                            aidbook.Close();
                        }
                        //else there aren't any homework assignments
                        else
                        {
                            aid = 1;
                        }
                        query = "SELECT status FROM homework";
                        msqcmd = new MySqlCommand(query, msqcon);
                        MySqlDataReader statbook = msqcmd.ExecuteReader();
                        statbook.Read();
                        String[] status = new String[hwid];
                        for (int j = 0; j < hwid; j++)
                        {
                            status[j] = System.Convert.ToString(statbook["status"]);
                            statbook.Read();

                            if (status[j] == "Complete")
                            {
                                status[j] = "Late";
                            }
                        }
                        statbook.Close();
                        query = "SELECT * FROM user ORDER BY userId DESC LIMIT 1";
                        msqcmd = new MySqlCommand(query, msqcon);
                        MySqlDataReader uidbook = msqcmd.ExecuteReader();
                        uidbook.Read();
                        int userid = System.Convert.ToInt32(uidbook["userId"]);
                        uidbook.Close();

                        int count = hwid;
                        hwid = 1;
                        int i = 0;
                        //assign ALL the assignments to the new student!
                        while (i < count)
                        {
                            query = "INSERT INTO hmwkassignment (assignmentId, homeworkId, grade, status, currentQuestion, userId) values (" + aid + ", " + hwid + ", 0, '" + status[i] + "', 1, " + userid + ")";
                            msqcmd = new MySqlCommand(query, msqcon);
                            msqcmd.ExecuteNonQuery();
                            i++;
                            aid++;
                            hwid++;
                        }
                    }
                }
                msqcon.Close();
            }
            catch (Exception)
            {
                throw;
            }


        }

        [WebMethod]
        public static string SignIn(String Username, String Password)
        {
            string url = "";
            string connStr = ConfigurationManager.ConnectionStrings["linearhmwkdb"].ConnectionString;
            MySqlConnection msqcon = new MySqlConnection(connStr);
            try
            {
                msqcon.Open();
                MySqlDataReader user = null;
                String query = "SELECT * FROM user where username = @Username AND password = SHA(@Password)";
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@Username", Username));
                msqcmd.Parameters.Add(new MySqlParameter("@Password", Password));
                user = msqcmd.ExecuteReader();

                if (user.HasRows)
                {
                    user.Read();
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                        Username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        false,
                        user["role"].ToString() + " " + user["first"] + " " + user["last"],
                        FormsAuthentication.FormsCookiePath);

                    string encTicket = FormsAuthentication.Encrypt(ticket);

                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

                    HttpContext.Current.Response.Cookies.Add(cookie);


                    if (user["role"].ToString().Equals("I"))
                    {
                       url = "InstructorHome.aspx";
                    }
                    else
                    {
                        url = "StudentHome.aspx";
                    }
                }

                msqcon.Close();
            }
            catch (Exception)
            {
                throw;
            }

            return url;
        }


        [WebMethod]
        public static void SignOut()
        {
            FormsAuthentication.SignOut();
            if (HttpContext.Current.Request.Cookies["LINALGHW"] != null)
            {
                HttpContext.Current.Response.Cookies["LINALGHW"].Expires = DateTime.Now.AddDays(-1);
            }
        }
    }
}