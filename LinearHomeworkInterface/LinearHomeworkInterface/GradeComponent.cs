//This is the grading component for the page. The Homework Controller
//dumps into a text file that serves as the solution key that this file
//will utilize. The user's answer will be passed in the constructor for
//this component and is then utilized in the actual Grade() method.
//Author: Kieran Ojakangas Date: 9/12/2013
//Code reuse from: http://msdn.microsoft.com/en-us/library/vstudio/ezwyzy7b.aspx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeComponent
{
    //The class itself
    class Grader
    {
        public int solCount = 0;
        public float[] UserSolutions;
        public float[] SolutionKey;

        //default constructor for the controller
        public Grader()
        {
            //no need for code here
        }

        //The user-solution-passing constructor for the grading program. The controller passes
        //the solution values from the user through this constructor.
        public Grader(params int [] ListUserSolutions) {
            int x = 0;
            UserSolutions = new float[ListUserSolutions.Length];
            foreach (int i in ListUserSolutions)
            {
                UserSolutions[x] = i;
                x++;
            }
        }

        //set the key in the solution text with any list of numbers
        //UPDATE: Should no longer be necessary since dynamic grading has been implemented
        public void SetKey(params int [] ListActualSolutions)
        {
            int x = 0;
            SolutionKey = new float[ListActualSolutions.Length];
            foreach (int i in ListActualSolutions)
            {
                SolutionKey[x] = i;
                x++;
            }
            String keyValue = "";
            for (int i = 0; i < SolutionKey.Length; i++)
            {
                keyValue += SolutionKey[i] + "\n";
            }
            keyValue = keyValue.Substring(0,keyValue.Length-1);
            System.IO.File.WriteAllText(@"C:\Users\KOEMXE\Documents\GitHub\LinearAlgebraHW\LinearHomeworkInterface\LinearHomeworkInterface\GradeKey.txt",
                keyValue);
        }

        //Here is the actual grading method
        public String Grade(List<float> ActualSolution, params float[] ListUserSolutions)
        {
            UserSolutions = new float[ListUserSolutions.Length];
            int x = 0;
            foreach (int i in ListUserSolutions)
            {
                UserSolutions[x] = i;
                x++;
            }
            // Read each line of our solution text file into a string array. Each element 
            // of the array is one line of the file.
            // UPDATE: Dynamic grading added, so checking against text file no longer necessary
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\KOEMXE\Documents\GitHub\LinearAlgebraHW\LinearHomeworkInterface\LinearHomeworkInterface\GradeKey.txt");

            //The logic comparison section. If the user enters both solutions correctly the class
            //returns a commendation as a string, otherwise the class returns an incorrect string
            foreach (var i in UserSolutions)
            {
                if (this.solCount == ActualSolution.Count)
                {
                    this.solCount++;
                    break;
                }
                else if (System.Convert.ToInt32(ActualSolution.ElementAt(this.solCount)) == i)
                {
                    this.solCount++;
                }
                else
                {
                    return "Not good. Try again.";
                } 
            }

            if (solCount == ActualSolution.Count)
            {
                return "Very good! You got a good grade.";
            }
            else{
                return "We couldn't match up all the solutions with our key. Please verify that you" +
                    " have the correct number of solutions";
            }
            
        }
    }
}
