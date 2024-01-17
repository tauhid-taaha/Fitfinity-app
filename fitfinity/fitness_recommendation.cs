using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitfinity
{
    internal class fitness_recommendation
    {
        public string GenerateWorkoutSuggestion(string activityLevel, string goal, double bmi)
        {
            // Sample workout suggestions
            string workoutSuggestion = $"Based on your activity level , and BMI ({bmi}), here is a workout suggestion:\n";

            // Adjust workout suggestions based on BMI
            if (bmi < 18.5)
            {
                workoutSuggestion += "Considering your BMI, focus on strength training to build muscle mass.";
            }
            else if (bmi >= 18.5 && bmi < 25)
            {
                workoutSuggestion += "Your BMI is within the healthy range. A balanced mix of cardio and strength training is recommended.";
            }
            else
            {
                workoutSuggestion += "With a higher BMI, prioritize cardio exercises for weight management.";
            }

            // Add more logic based on activity level and goal

            return workoutSuggestion;
        }

        public string GenerateDietSuggestion(string goal, double bmr)
        {
            // Sample diet suggestions
            string dietSuggestion = $"For your goal  and BMR ({bmr}), here is a diet suggestion:\n";

            // Adjust diet suggestions based on BMR
            if (bmr < 1500)
            {
                dietSuggestion += "Considering your low BMR, focus on nutrient-dense foods and control portion sizes.\nOpt for Whole Grains Like Quinoa and BrownRice for Sustained Enerzy";
            }
            else if (bmr >= 1500 && bmr < 2000)
            {
                dietSuggestion += "With a moderate BMR, aim for a balanced diet with a mix of macronutrients\nChoose Whole Grain And Complex Carbohydrades for enerzy.";
            }
            else
            {
                dietSuggestion += "Given your higher BMR, make sure to consume enough calories to support your energy needs.\nInclude Lean Protein For Muscle Maintenance and Repair.\nConsider Incorporating Snacks Between Meals.\nInclude Healthy Fats like Avacado,Nuts,Seeds etc ";
            }

            // Add more logic based on the goal

            return dietSuggestion;
        }
    }
}
