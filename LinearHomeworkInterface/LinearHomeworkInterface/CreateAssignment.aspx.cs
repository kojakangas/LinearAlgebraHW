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
        public static string AddAssignment(String ListQuestions)
        {
            //create a String array of the answers submitted from the user page
            //splits by space
            string[] lines = ListQuestions.Split('|');
            string[] variables = null;
            AssignComponent.Assigner entry = new AssignComponent.Assigner();
            int rows;
            int cols;
            int min;
            int max;
            int freeVar;
            int inconsistent;
            for (int i = 0; i < lines.Length; i++)
            {
                string questionStr = lines[i];
                variables = questionStr.Split(',');
                rows = System.Convert.ToInt32(variables[2]);
                cols = System.Convert.ToInt32(variables[3]);
                min = System.Convert.ToInt32(variables[4]);
                max = System.Convert.ToInt32(variables[5]);
                freeVar = System.Convert.ToInt32(variables[6]);
                inconsistent = 0;
                entry.Assign(rows, cols, min, max, freeVar, inconsistent);
            }
            
            return "Check your database...";
        } 
    }
}