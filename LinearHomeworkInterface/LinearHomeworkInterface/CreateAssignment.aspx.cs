using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinearHomeworkInterface.components;

namespace LinearHomeworkInterface
{
    public partial class CreateAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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