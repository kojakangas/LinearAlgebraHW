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
                msqcon.Open();
                String query = "insert into user (username, first, last, password, role) values (@Username, @First, @Last, SHA(@Password), 'S')";
                MySqlCommand msqcmd = new MySqlCommand(query, msqcon);
                msqcmd.Parameters.Add(new MySqlParameter("@Username", details[0]));
                msqcmd.Parameters.Add(new MySqlParameter("@First", details[1]));
                msqcmd.Parameters.Add(new MySqlParameter("@Last", details[2]));
                msqcmd.Parameters.Add(new MySqlParameter("@Password", details[3]));
                msqcmd.ExecuteNonQuery();

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
                    FormsAuthentication.SetAuthCookie(Username, false);
                    if (user["role"].ToString().Equals("I"))
                    {
                       url = "/InstructorHome.aspx";
                    }
                    else
                    {
                        url = "/StudentHome.aspx";
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
    }
}