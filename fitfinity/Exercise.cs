using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitfinity


{
    public class Exercise
    {
        public static int CalculateStepsPerMile(double minutesPerMile, double height, string gender,double mile)
        {
            double stepsPerMile;
            double totalsteps;
            

            if (gender.Equals("Male", StringComparison.OrdinalIgnoreCase))
            {
                stepsPerMile = 1084 + (143.6 * minutesPerMile) - (13.5 * height * 0.394);
                totalsteps = stepsPerMile * mile;
            }
            else if (gender.Equals("Female", StringComparison.OrdinalIgnoreCase))
            {
                stepsPerMile = 1949 + (63.4 * minutesPerMile) - (14.1 * height * 0.394);
                totalsteps = stepsPerMile * mile;


            }
            else
            {
               
                throw new ArgumentException("Invalid gender. Supported genders are 'Male' and 'Female'.");
            }

            return (int)Math.Round(stepsPerMile * mile);

        }

        public static string DetermineActivityLevel(double totalSteps)
        {
            if (totalSteps < 5000)
            {
                return "Inactive";
            }
            else if (totalSteps >= 5000 && totalSteps < 7500)
            {
                return "Light";
            }
            else if (totalSteps >= 7500 && totalSteps < 10000)
            {
                return "Moderate";
            }
            else if (totalSteps >= 10000 && totalSteps < 12500)
            {
                return "Active";
            }
            else
            {
                return "Very Active";
            }
        }
    }
}


