using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitfinity
//newadd
{

    public class Nutrition
    {
        public static double CalculateDailyCalories(string gender, double weight, double height, int age, string activityLevel)
        {
            double bmr;

            if (gender.Length == 4 && gender[0] == 'M' && gender[1] == 'a' && gender[2] == 'l' && gender[3] == 'e')




            {
                bmr = 66.5 + (13.75 * weight) + (5.003 * height) - (6.755 * age);
            }
            else
            {
                bmr = 655.1 + (9.563 * weight) + (1.850 * height) - (4.676 * age);
            }

            double activityMultiplier;

            switch (activityLevel)
            {
                case "inactive":
                    activityMultiplier = 1.2;
                    break;
                case "light":
                    activityMultiplier = 1.375;
                    break;
                case "moderate":
                    activityMultiplier = 1.55;
                    break;
                case "active":
                    activityMultiplier = 1.725;
                    break;
                case "very active":
                    activityMultiplier = 1.9;
                    break;
                default:
                    activityMultiplier = 1.2;
                    break;
            }

            return bmr * activityMultiplier;

        }
        public static double CalculateBmr(string gender, double weight, double height, int age)
        {
            double bmr;

            if (gender.Length == 4 && gender[0] == 'M' && gender[1] == 'a' && gender[2] == 'l' && gender[3] == 'e')




            {
                bmr = 66.5 + (13.75 * weight) + (5.003 * height) - (6.755 * age);
            }
            else
            {
                bmr = 655.1 + (9.563 * weight) + (1.850 * height) - (4.676 * age);
            }

            return bmr;
        }
        public static double CalculateIdealWeight (string gender, double height)
        {
            double idealWeight;

            if (gender.Equals("Male", StringComparison.OrdinalIgnoreCase))
            {
                idealWeight = 50 + 2.3 * ((height / 2.54) - 60);
            }
            else if (gender.Equals("Female", StringComparison.OrdinalIgnoreCase))
            {
                idealWeight = 45.5 + 2.3 * ((height / 2.54) - 60);
            }
            else
            {
                // Handle other genders or throw an exception if needed
                throw new ArgumentException("Invalid gender. Supported genders are 'Male' and 'Female'.");
            }

            return idealWeight;
        }


    }
}

