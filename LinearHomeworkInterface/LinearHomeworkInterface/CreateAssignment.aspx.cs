using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinearHomeworkInterface
{
    public partial class CreateAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Check_User();
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

        //our WebMethod for adding an assignment to the database
        [WebMethod]
        public static string AddAssignment(String ListConstraints, String ListQuestions)
        {
            //create a String array of the answers submitted from the user page
            //splits by space
            string[] lines = ListConstraints.Split('|');
            string[] questions = ListQuestions.Split('|');
            AssignComponent.Assigner entry = new AssignComponent.Assigner();
            //fetch our assignment parameters to pass with our array of questions strings
            String title = lines[0];
            int points = System.Convert.ToInt32(lines[1]);
            String dueDate = lines[2];

            return entry.Assign(title, points, dueDate, questions);
        } 
    }
}