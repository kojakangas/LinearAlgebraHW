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

<<<<<<< HEAD
=======
        //our solution array for the user solutions being
        //passed into the grading method
        public float[] UserSolutions;

        //This is the given range that will provide a correct answer
        public float acceptableRange = .001F;

>>>>>>> 6c9f0910f216b4c2defe5145bc4abe054bd7acad
        //default constructor for the controller
        public Grader()
        {
            //no need for code here
        }

        //Here is the grading method
        public String Grade(List<float> ActualSolution, float[] ListUserSolutions)
        {
            //The logic comparison section. If the user enters both solutions correctly the class
            //returns a commendation as a string, otherwise the class returns an incorrect string

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
                //if our actual solution for the current solution is the same as the current
                //user solution being checked
<<<<<<< HEAD
                if (!(ActualSolution.ElementAt(this.solCount).Equals(i)))
=======
                else if (Math.Abs(ActualSolution.ElementAt(this.solCount)-i) < acceptableRange)
                {
                    //increment our public solution counter
                    this.solCount++;
                }

                //otherwise
                else
>>>>>>> 6c9f0910f216b4c2defe5145bc4abe054bd7acad
                {
                    //we have found a wrong answer, so the user must try again (currently we only
                    //aim to check if the user solution is perfect, we're grading any work shown
                    //along the way for this sprint)
                    return "Not good. Try again.";
                }

                else
                {
                    //increment our current answer counter to compare the next cells
                    this.solCount++;
                }
            }

            //inform the user they have a *perfect* answer,
            //since the loop found no mismatches against the
            //solutions of the generated matrix
            return "Very good! You got a good grade.";

            }
        
        }    
      }
            
 }
