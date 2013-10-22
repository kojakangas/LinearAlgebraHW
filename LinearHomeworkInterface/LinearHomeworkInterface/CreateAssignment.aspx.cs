using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinearHomeworkInterface
{
    public partial class CreateAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //our WebMethod for adding an assignment to the database
        [WebMethod]
        public static string AddAssignment(String ListQuestions)
        {
            //create a String array of the answers submitted from the user page
            //splits by space
            string[] lines = ListQuestions.Split(' ');

            
            return "This button did something that is not implemented yet.";
        } 
    }
}