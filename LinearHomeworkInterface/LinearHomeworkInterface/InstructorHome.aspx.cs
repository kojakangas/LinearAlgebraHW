using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}