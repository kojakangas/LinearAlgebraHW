//This is the grading component for the page. The Homework Controller
//dynamically generates a solution key on which the system of equations
//solved by the student is based. The student's answer will be passed
//in the Grade() method as well as the solutions on which the equations
//are created and then displayed.
//Author: Kieran Ojakangas Date: 9/26/2013
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

        //our check answer counter for the grading method
        public int checkAnswer = 0;

        //This is the given range that will provide a correct answer
        public float acceptableRange = .001F;

        //default constructor for the controller
        public Grader()
        {
            //no need for code here
        }

        //Here is the grading method
        public String Grade(List<float> ActualSolution, float[] ListUserSolutions, List<string> ActualTextVariables, String[] UserTextVariables)
        {
            //The logic comparison section. If the user enters both solutions correctly the class
            //returns a commendation as a string, otherwise the class returns a string prompting the student
            //to try again.

            //if the number of user solutions is less or greater than the number of actual solutions
            if (ListUserSolutions.Length > ActualSolution.Count ||
                ListUserSolutions.Length < ActualSolution.Count)
            {
                //we inform the user he/she either has too many or too few solutions (for all relevant
                //purposes in our sprint we decided to let the student decide how many solutions he/she
                //may be short of or vice versa)
                return "We couldn't match up all the solutions with our key. Please verify that you" +
                    " have the correct number of solutions";
            }

            //otherwise
            else {
            //For each element in our local array, check:
            foreach (float i in ListUserSolutions)
            {
                //first check to see if our current answer from either the user key
                //or actual answer key is a free or leading variable
                if (UserTextVariables[checkAnswer].Equals("f") || ActualTextVariables[checkAnswer].Equals("f")
                    || UserTextVariables[checkAnswer].Equals("l") || ActualTextVariables[checkAnswer].Equals("l"))
                {
                    //if the user free or leading variable matches the actual current free or leading variable,
                    //add to the count and move on
                    if(UserTextVariables[checkAnswer].Equals(ActualTextVariables.ElementAt(checkAnswer))) {
                        checkAnswer++;
                    }
                    //otherwise stop the loop- there is no reason to check further at this point since we
                    //have an incorrect answer correspondence and if we tried checking through our else if
                    //the program would crash
                    else {
                        return "Not good... try again!";
                    }
                }

                //then after that check (if false we know we're dealing with an integer)...
                //if our actual solution for the current solution is in the acceptable threshold of
                //the current user solution being checked
                else if (Math.Abs(ActualSolution.ElementAt(this.checkAnswer)-i) < acceptableRange)
                {
                    //increment our public check answer counter
                    this.checkAnswer++;
                }

                //otherwise
                else
                {
                    //we have found a wrong answer, so the user must try again (currently we only
                    //aim to check if the user solution is perfect, we're not grading any work shown
                    //along the way for this sprint)
                    return "Not good. Try again.";
                }

            }

            //inform the user they have an acceptable answer,
            //since the loop found no solution exceeding the threshold against the
            //solutions of the generated matrix
            return "Very good! You got a good grade.";

            }
        
        }    
      }
            
 }
