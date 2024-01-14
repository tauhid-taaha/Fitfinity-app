using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitfinity
{
    public class Foods
    {
        public string Name { get; set; }
        public double Weight { get; set; } // Add this property
        public double Calorie { get; set; }
        public string MealType { get; set; }
        public string FoodType { get; set; } // Add this property
        public Foods() { }
        public Foods(string name, double calories, double weight, string mealType)
        {
            Name = name;
            Calorie = calories;
            MealType = mealType;
            Weight = weight;
        }
        public Foods(string name, double calories, double weight, string mealType, string foodType)
        {
            Name = name;
            Calorie = calories;
            MealType = mealType;
            Weight = weight;
            FoodType = foodType;
        }
        public Foods(string name, double calories,  string mealType)
        {
            Name = name;
            Calorie = calories;
            MealType = mealType;
           
        }
        public Foods(string name, double calories, string mealType, string foodType)
        {
            Name = name;
            Calorie = calories;
            MealType = mealType;
            FoodType = foodType;
        }
    }
   
}
