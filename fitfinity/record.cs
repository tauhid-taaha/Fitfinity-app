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
        string filePath = @"C:\Users\DR.MEHBUB UL KADIR\Documents\spl\fitfinity\bin\Debug\record.txt";
        public List<loadrecord> recordlist = new List<loadrecord>();

        public string calorie_file { get; private set; }

        public record()
        {
            LoadDataFromFile();
        }
        public void LoadDataFromFile()
        {
            if (
               File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Split the line of date and bmi using a comma
                        string[] parts = line.Split(',');
                        if (parts.Length == 3)
                        {
                            // Parse and store the data in the LoadRecord class
                            loadrecord data = new loadrecord
                            {
                                UserName = parts[0], // Ensure the correct property is assigned
                                date = parts[1],
                                bmi = double.Parse(parts[2])
                            };

                            // Add the records instance to the list
                            recordlist.Add(data);
                        }
                    }

                }
            }

        }

        public void PrintAllRecords(string userName)
        {
            var userRecords = recordlist.Where(record => record.UserName == userName).ToList();
            if (userRecords.Count == 0)
            {
                Console.WriteLine($"No BMI records found for user: {userName}");
                return;
            }

            foreach (var data in userRecords)
            {
                Console.WriteLine($"User Name - {data.UserName} \t Date - {data.date} \t BMI - {data.bmi}");
            }

        }
        public void printname(string userName) { Console.WriteLine(userName); }
        public void RecordBMI(string username, double newBMI)
        {
            if (File.Exists(calorie_file))
            {
                // Automatically record the date and new BMI in a file
                if (username != null)
                {
                    // Automatically record the username, date, and new BMI in the file
                    using (StreamWriter writer = File.AppendText(filePath))
                    {
                        writer.WriteLine($"{username},{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},{newBMI:F2}");
                    }
                }
            }
        }


    }
}

