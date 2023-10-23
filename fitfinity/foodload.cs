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
        string filePath =@"C:\\\\Users\\\\Tauhid\\\\source\\\\repos\\\\Fitfinity-app\\\\New folder (2)\\\\fitfinity\\\\calories.txt";
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
                    // Split the line into food name and calorie using a comma
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        // Parse and store the data in the Foods class
                        Foods food = new Foods
                        {
                            Name = parts[0],
                            Calorie = double.Parse(parts[1])
                        };

                        // Add the Foods instance to the list
                        foodsList.Add(food);
                    }
                }
            }
        }

        public void PrintAllFoodNames()
        {
            foreach (var food in foodsList)
            {
                Console.WriteLine($"Food Name: {food.Name}");
            }

        }
        public double CalculateTotalCalories(List<int> selectedIndices, List<int> grams)
        {
            double totalCalories = 0;
            for (int i = 0; i < selectedIndices.Count; i++)
            {
                int index = selectedIndices[i];
                if (index >= 0 && index < foodsList.Count)
                {
                    double caloriePerGram = foodsList[index].Calorie;
                    int gram = grams[i];
                    totalCalories += caloriePerGram * gram;
                }
            }
            return totalCalories;
        }
    }

}

