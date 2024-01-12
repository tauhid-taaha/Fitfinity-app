using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitfinity
{
    public class record
    {
        //here the bmi and date will be saved...
        string filePath = @"C:\Users\Tauhid\Downloads\SPL\SPL\fitfinity\bin\Debug\record.txt";
        public List<loadrecord> recordlist = new List<loadrecord>();

        public record()
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
                    // Split the line of date and bmi using a comma
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        // Parse and store the data in the LoadRecord class
                        loadrecord data = new loadrecord
                        {
                            date = parts[0],
                            bmi = double.Parse(parts[1])
                        };

                        // Add the records instance to the list
                        recordlist.Add(data);
                    }
                }
            }
        }

        public void PrintAllRecords()
        {
            foreach (var data in recordlist)
            {
                Console.WriteLine($"Date - {data.date} \t BMI - {data.bmi}");
            }

        }
        public double CalculateTotalCalories(List<int> selectedIndices, List<int> grams)
        {
            double totalCalories = 0;
            for (int i = 0; i < selectedIndices.Count; i++)
            {
                int index = selectedIndices[i];
                if (index >= 0 && index < recordlist.Count)
                {
                    double caloriePerGram = recordlist[index].bmi;
                    int gram = grams[i];
                    totalCalories += caloriePerGram * gram;
                }
            }
            return totalCalories;
        }
    }

}

