using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Data.Linq;
using MySql.Data.MySqlClient;
using System.Web.Security;
using System.Configuration;

namespace LinearHomeworkInterface
{
    public partial class QuestionPage : System.Web.UI.Page
    {
        public static List<float> solution = null;
        public static List<string> textSolution = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Check_User();

            int n = 3;
            int m = 4;
            int min = -2;
            int max = 2;
            //int freeVars = 0;
            //Boolean inconsistent = false;
            
            MatrixBuilder.MatrixOperations mb = new MatrixBuilder.MatrixOperations();
            Random rand = new Random();
            solution = new List<float>();

            float[] answer = new float[n];
            for (int i = 0; i < n; i++)
            {
                answer[i] = rand.Next(min, max);
                solution.Add(answer[i]);
            }

            float[,] matrix = mb.generateUniqueSolutionMatrix(n, m, min, max, answer);

            //Displays the equations using MATHJAX by building a parsable string
            for (int i = 0; i < n; i++)
            {
                char[] a = new char[1];
                String expression = new String(a);
                for (int j = 0; j < m; j++)
                {
                    if (j == 0)
                    {
                        expression = "$${";
                        expression += matrix[i, j];
                        expression += "x_";
                        expression += j + 1;
                        expression += " ";
                    }
                    else if (j < (m - 2))
                    {
                        if (matrix[i, j] >= 0) expression += "+ ";
                        else expression += " ";
                        expression += matrix[i, j];
                        expression += "x_";
                        expression += j + 1;
                        expression += " ";
                    }
                    else if (j == (m - 2))
                    {
                        if (matrix[i, j] >= 0) expression += "+ ";
                        else expression += " ";
                        expression += matrix[i, j];
                        expression += "x_";
                        expression += j + 1;
                        expression += "} = ";
                    }
                    else if (j == (m - 1))
                    {
                        expression += matrix[i, j];
                        expression += "$$";
                    }
                }
                question.Text = question.Text + expression;
            }
        }
       
        //our WebMethod for checking the user's solution(s)
        [WebMethod]
        public static string GradeAnswer(String ListPassingSolutions)
        {
            //create a String array of the answers submitted from the user page
            //splits by space
            string[] lines = ListPassingSolutions.Split(' ');

            //create a float array to represent our solutions being passed into
            //this method
            float[] UserSolutions = new float[lines.Length];

            //create a counter
            int x = 0;

            //create a string array which will pass our free variables to the grader
            string[] freeVar = new string[lines.Length];

            //for all the elements being passed in our array of user strings
            for (int i = 0; i < lines.Length; i++)
            {
                //if our current element is a free variable or blank
                if(lines[i].Equals("f") || lines[i].Equals(null)) {
                    //add it to the free variables string array
                    freeVar[x] = lines[i];

                    //increment our counter
                    x++;
                }

                else {
                    //convert each string into an integer and add it to our integer
                    //array for the grading component
                    UserSolutions[x] = System.Convert.ToInt32(lines[i]);

                    //add an empty string to the free variables string array
                    freeVar[x] = " ";

                    //increment our counter
                    x++;
                }
            }
            //create our grading object to grade the user's answers
            GradeComponent.Grader grader = new GradeComponent.Grader();

            //have this created object grade the user's solutions against
            //the answers of our generated matrix
            return grader.Grade(solution, UserSolutions, textSolution, freeVar);
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