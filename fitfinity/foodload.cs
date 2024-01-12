using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitfinity
{
    public class foodload
    {
        string filePath = @"C:\Users\Tauhid\Downloads\SPL\SPL\fitfinity\bin\Debug\calories.txt";
        public List<Foods> foodsList = new List<Foods>();

        public foodload()
        {
            LoadDataFromFile();
        }

        public void LoadDataFromFile()
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        Foods food = new Foods
                        {
                            Name = parts[0],
                            Calorie = double.Parse(parts[1]),
                            MealType = parts[2].Trim().ToLower() // Assuming meal type is stored as "breakfast", "lunch", "snacks", or "dinner"
                        };

                        foodsList.Add(food);
                    }
                }
            }
            Console.WriteLine("Loaded Food Items:");
            foreach (var food in foodsList)
            {
                Console.WriteLine($"Name: {food.Name}, Calorie: {food.Calorie}, MealType: {food.MealType}");
            }
        }

        public void PrintFoodNames(string mealType)
        {
            Console.WriteLine($"MealType to search: {mealType}");
            List<Foods> selectedList = foodsList.Where(food => food.MealType == mealType.ToLower()).ToList();

            for (int i = 0; i < selectedList.Count; i++)
            {
                Console.WriteLine($"Index: {i + 1}, Food Name: {selectedList[i].Name}");
            }
        }

        public bool IsValidFoodIndex(string mealType, int index)
        {
            List<Foods> selectedList = foodsList.Where(food => food.MealType == mealType.ToLower()).ToList();
            return index > 0 && index <= selectedList.Count;
        }

        public  double GetCalorie(string mealType, int index)
        {
            List<Foods> selectedList = foodsList.Where(food => food.MealType == mealType.ToLower()).ToList();
            return index >= 0 && index < selectedList.Count ? selectedList[index].Calorie : 0;
        }

        public  string GetFoodName(string mealType, int index)
        {
            List<Foods> selectedList = foodsList.Where(food => food.MealType == mealType.ToLower()).ToList();
            return index >= 0 && index < selectedList.Count ? selectedList[index].Name : "";
        }

        public double CalculateTotalCalories(string mealType, List<int> selectedIndices, List<int> grams)
        {
            double totalCalories = 0;
            List<Foods> selectedList = foodsList.Where(food => food.MealType == mealType.ToLower()).ToList();

            for (int i = 0; i < selectedIndices.Count; i++)
            {
                int index = selectedIndices[i];
                if (index >= 0 && index < selectedList.Count)
                {
                    double caloriePerGram = selectedList[index].Calorie;
                    int gram = grams[i];
                    totalCalories += caloriePerGram * gram;
                }
            }
            return totalCalories;
        }

    }
}

