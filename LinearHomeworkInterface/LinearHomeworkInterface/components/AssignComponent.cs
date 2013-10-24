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
        public String Assign(int rows, int col, int min, int max, int freeVar, int inconsistent)
        {
            if (rows == 2 && col == 3)
            {
                if (freeVar == 0)
                {
                    if (inconsistent == 0)
                    {
                        MySqlConnection msqcon = new MySqlConnection("server=localhost;User Id=root;Password=r00tr00tr00tr00tr00t;database=linearhmwkdb");
                        try
                        {
                            msqcon.Open();
                            MySqlCommand msqcom = new MySqlCommand("INSERT INTO question (questionid, number, wording, pointValue, homeworkId, parameterId, solutionId) " + 
                            "Values (7, 4, 'SUCK', 1, 1, 1, 1)",msqcon);
                            msqcom.ExecuteNonQuery();
                            msqcon.Close();
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                        return "That's a nice problem.";
                    }
                    else
                    {
                        return "How mean!";
                    }
                }
                else
                {
                    if (inconsistent == 0)
                    {
                        return "I guess that problem's okay.";
                    }
                    else
                    {
                        return "You're awful.";
                    }
                }
            }
            else
            {
                return "Your question contains invalid rows and columns for a suitable question.";
            }
        }
    }
}