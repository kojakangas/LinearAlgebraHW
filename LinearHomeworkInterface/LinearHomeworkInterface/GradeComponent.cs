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
        //public instance variables//

        //our solution counter for the grading method
        public int solCount = 0;

        //our solution array for the user solutions being
        //passed into the grading method
        public float[] UserSolutions;

        //our solution array for the actual solutions of our
        //generated matrix
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

        //Here is the grading method
        public String Grade(List<float> ActualSolution, params float[] ListUserSolutions)
        {
            //create our own local array with the same length as the array being passed
            //to this method from our code behind controller (user's solutions)
            UserSolutions = new float[ListUserSolutions.Length];

            //create a counter
            int x = 0;

            //for each element in the solutions being passed from our user through the controller
            foreach (int i in ListUserSolutions)
            {
                //add this element to the corresponding place in our local array to grade with
                UserSolutions[x] = i;

                //increment our counter
                x++;
            }

            // Read each line of our solution text file into a string array. Each element 
            // of the array is one line of the file.
            // UPDATE: Dynamic grading added, so checking against text file no longer necessary
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Users\KOEMXE\Documents\GitHub\LinearAlgebraHW\LinearHomeworkInterface\LinearHomeworkInterface\GradeKey.txt");

            //The logic comparison section. If the user enters both solutions correctly the class
            //returns a commendation as a string, otherwise the class returns an incorrect string
            //For each element in our local array, check:
            foreach (var i in UserSolutions)
            {
                //if our public solution counter equals the length of the actual list of solutions
                //from our generated matrix
                if (this.solCount == ActualSolution.Count)
                {
                    //increment our counter another time so that the program can inform the user
                    //that he/she has entered too many solutions
                    this.solCount++;

                    //break out of this loop, we know we have too many solutions
                    break;
                }

                //else if our actual solution for the current solution is the same as the current
                //user solution being checked
                else if (ActualSolution.ElementAt(this.solCount) == i)
                {
                    //increment our public solution counter
                    this.solCount++;
                }

                //otherwise
                else
                {
                    //we have found a wrong answer, so the user must try again (currently we only
                    //aim to check if the user solution is perfect, we're grading any work shown
                    //along the way for this sprint)
                    return "Not good. Try again.";
                } 
            }

            //if our public solution counter is equal to the actual solution counter after we have
            //exited our loop
            if (solCount == ActualSolution.Count)
            {
                //inform the user they have a *perfect* answer
                return "Very good! You got a good grade.";
            }

            //otherwise
            else{
                //we inform the user he/she either has too many or too few solutions (for all relevant
                //purposes in our sprint we decided to let the student decide how many solutions he/she
                //may be short of or vice versa)
                return "We couldn't match up all the solutions with our key. Please verify that you" +
                    " have the correct number of solutions";
            }
            
        }
    }
}
