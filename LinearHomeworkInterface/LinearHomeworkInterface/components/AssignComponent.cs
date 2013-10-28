using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace AssignComponent
{
    class Assigner
    {
        public Assigner() {
            //no need for code
        }

        //Here is the logic to assign the problems through SQL connection strings
        public String Assign(String title, int points, String dueDate, String[] listOfQuestions)
        {
            int hwid = 0;
            int i = 0;
            int aid = 0;
            int numstudents = 0;
            //if (rows == 2 && col == 3)
            //{
            //    if (freeVar == 0)
            //    {
            //        if (inconsistent == 0)
            //        {
            //            MySqlConnection msqcon = new MySqlConnection("server=localhost;User Id=root;Password=r00tr00tr00tr00tr00t;database=linearhmwkdb");
            //            try
            //            {
            //                msqcon.Open();
            //                MySqlCommand msqcom = new MySqlCommand("INSERT INTO question (questionid, number, wording, pointValue, homeworkId, parameterId, solutionId) " + 
            //                "Values (7, 4, 'SUCK', 1, 1, 1, 1)",msqcon);
            //                msqcom.ExecuteNonQuery();
            //                msqcon.Close();
            //            }
            //            catch (Exception)
            //            {
            //                throw;
            //            }
            //            return "That's a nice problem.";
            //        }
            //        else
            //        {
            //            return "How mean!";
            //        }
            //    }
            //    else
            //    {
            //        if (inconsistent == 0)
            //        {
            //            return "I guess that problem's okay.";
            //        }
            //        else
            //        {
            //            return "You're awful.";
            //        }
            //    }
            //}
            //else
            //{
            //    return "Your question contains invalid rows and columns for a suitable question.";
            //}
            MySqlConnection msqcon = new MySqlConnection("server=localhost;User Id=root;Password=r00tr00tr00tr00tr00t;database=linearhmwkdb");
            try
            {
                //open the connection
                msqcon.Open();
                //first we need to fetch the latest homeworkid so we can increment it properly later
                MySqlCommand msqcheck = new MySqlCommand("SELECT * FROM homework ORDER BY homeworkid DESC LIMIT 1", msqcon);
                MySqlDataReader hwidbook = msqcheck.ExecuteReader();
                hwidbook.Read();
                hwid = System.Convert.ToInt32(hwidbook["homeworkid"]);
                hwidbook.Close();
                //then we need to fetch the latest assignmentid so we can increment it properly later
                msqcheck = new MySqlCommand("SELECT * FROM hmwkassignment ORDER BY assignmentId DESC LIMIT 1", msqcon);
                MySqlDataReader aidbook = msqcheck.ExecuteReader();
                aidbook.Read();
                aid = System.Convert.ToInt32(aidbook["assignmentId"]);
                aidbook.Close();
                //we need to know how many students we have so that we can add the assignment to every student
                msqcheck = new MySqlCommand("SELECT COUNT(*) FROM user WHERE role = 'S'", msqcon);
                MySqlDataReader numstudentbook = msqcheck.ExecuteReader();
                numstudentbook.Read();
                numstudents = System.Convert.ToInt32(numstudentbook["COUNT(*)"]);
                numstudentbook.Close();
                //now we can declare our userid array
                int[] studentid = new int[numstudents];
                //now we need to fetch the userid for each cell in our new array
                msqcheck = new MySqlCommand("SELECT userid FROM user WHERE role = 'S'", msqcon);
                MySqlDataReader studentidbook = msqcheck.ExecuteReader();
                i = 0;
                while (studentidbook.Read())
                {
                    studentid[i] = System.Convert.ToInt32(studentidbook["userid"]);
                    i++;
                }
                studentidbook.Close();
                //insert the new assigned homework into the homework table with values passed from the instructor
                MySqlCommand msqcom = new MySqlCommand("INSERT INTO homework (homeworkid, title, points, dueDate) " +
                "Values (" + (hwid + 1) + ", '" + title + "', " + points + ", '" + dueDate + "')", msqcon);
                msqcom.ExecuteNonQuery();
                //now assign the homework assignment to each student
                for (int j = 0; j < studentid.Length; j++)
                {
                    msqcom = new MySqlCommand("INSERT INTO hmwkassignment (assignmentId, homeworkId, grade, status, currentQuestion, userId) " +
                    "Values (" + (aid + 1) + ", " + (hwid + 1) + ", " + 0 + ", 'Assigned'," + 1 + ", " + studentid[j] + ")", msqcon);
                    msqcom.ExecuteNonQuery();
                    aid++;
                }
                //now we need to check what type of questions each of these are and insert them in the appropriate tables
                //for each question from our table on the Create Assignment page...
                for (int j = 0; j < listOfQuestions.Length; j++)
                {
                    //if we are dealing with a System of Equations question
                    if (listOfQuestions[j].Contains("System of Equations"))
                    {
                        //split our question into separate strings and convert them to
                        //the appropriate parameters
                        string questionStr = listOfQuestions[j];
                        String[] variables = questionStr.Split(',');
                        int questionid = System.Convert.ToInt32(variables[0]);
                        string type = variables[1];
                        int rows = System.Convert.ToInt32(variables[2]);
                        int cols = System.Convert.ToInt32(variables[3]);
                        int min = System.Convert.ToInt32(variables[4]);
                        int max = System.Convert.ToInt32(variables[5]);
                        int freeVar = System.Convert.ToInt32(variables[6]);
                        int inconsistent;
                        if (variables[7].Equals("yes"))
                        {
                            inconsistent = 1;
                        }
                        else
                        {
                            inconsistent = 0;
                        }
                        //then we need to insert the question into the soe table and give
                        //it the other appropriate assignment values
                        //start with reading latest SoEindex to put question into SoE table
                        int SoEIndex;
                        msqcheck = new MySqlCommand("SELECT * FROM soe ORDER BY SoEindex DESC LIMIT 1", msqcon);
                        MySqlDataReader SoEbook = msqcheck.ExecuteReader();
                        SoEbook.Read();
                        SoEIndex = System.Convert.ToInt32(SoEbook["SoEindex"]);
                        SoEbook.Close();
                        //now we need to decrement the assignmentid back for each assignment we processed for each student
                        aid = aid-studentid.Length;
                        //now insert the question into the SoE table for each student (hence the assignmentId)
                        for (int k = 0; k < studentid.Length; k++)
                        {
                            msqcom = new MySqlCommand("INSERT INTO soe (SoEindex, questionId, rows, columns, min, max, freeVars, inconsistent, assignmentId) " +
                            "Values (" + (SoEIndex + 1) + ", " + questionid + ", " + rows + ", " + cols + ", " + min + ", " + max + ", " + freeVar + ", " + inconsistent + ", " + aid + ")", msqcon);
                            msqcom.ExecuteNonQuery();
                            aid++;
                            SoEIndex++;
                        }
                        //now we've assigned everything, so we move out of our if and let the last statement close the connection
                    }
                    else
                    {
                        //currently our implementation for the other types of questions are
                        //not supported, so they will come in due time.
                    }
                }
                //close our connection since we're now finished with assignment insertion
                msqcon.Close();
            }
            catch (Exception error)
            {
                return "An error occurred while creating the assignment: " + error
                    + "The out of bound index was: " + (i);
            }
            return "Homework successfully assigned.";
        }
    }
}