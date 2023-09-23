using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitfinity
{
  
        public class Nutrition
        {
            public static double CalculateDailyCalories(string gender, double weight, double height, int age, string activityLevel)
            {
                double bmr;

                if (gender.ToLower() == "male")
                {
                    bmr = 66.5 + (13.75 * weight) + (5.003 * height) - (6.755 * age);
                }
                else
                {
                    bmr = 655.1 + (9.563 * weight) + (1.850 * height) - (4.676 * age);
                }

                double activityMultiplier;

                switch (activityLevel.ToLower())
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
        }


    }

