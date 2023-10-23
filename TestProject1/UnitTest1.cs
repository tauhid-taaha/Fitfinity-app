using fitfinity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace TestProject1

{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]


        public void TestLoadDataFromFile()
        {
            // Arrange
            foodload foodLoader = new foodload();

            // Act
            foodLoader.LoadDataFromFile();

            // Assert
            // You can add assertions here to verify the expected behavior.
            // For example, you can check if foodsList is not empty.
            foodLoader.LoadDataFromFile(); // Load data before printing


            // Redirect Console output for testing
            Assert.AreEqual("Apple", foodLoader.foodsList[0].Name);
            Assert.AreEqual(1.0, foodLoader.foodsList[0].Calorie);
        }

        [TestMethod]

        public void test()
        {
            foodload fd = new foodload();
            List<int> selectedFoodIndices = new List<int>();

            Console.WriteLine("Available Food Options:");
           fd.PrintAllFoodNames();



            while (true)
            {
                Console.Write("Enter the number of a food item to select (0 to calculate calories or -1 to exit): ");
                if (int.TryParse(Console.ReadLine(), out int selected))
                {
                    if (selected == 0)
                    {
                        // Calculate total calories and break the loop
                        double totalCalories = fd.CalculateTotalCalories(selectedFoodIndices);
                        Console.WriteLine($"Total Calories of Selected Foods: {totalCalories}");
                        break;
                    }
                    else if (selected == -1)
                    {
                        // Exit the loop
                        break;
                    }
                    else if (selected > 0 && selected <= fd.foodsList.Count)
                    {
                        // Add the selected food index to the list
                        selectedFoodIndices.Add(selected - 1);
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }
    } 
        }
    
