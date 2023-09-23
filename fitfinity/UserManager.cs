﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitfinity
{
    internal class UserManager
    {
        private List<User> users;
        private User currentUser;

        public UserManager()
        {
            users = new List<User>();
            LoadUsersFromFile();

        }

        private void LoadUsersFromFile()
        {
            if (File.Exists("users.txt"))
            {
                using (StreamReader reader = File.OpenText("users.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] userData = line.Split(':');

                        // Check if userData has enough elements before accessing them
                        if (userData.Length == 6)
                        {
                            string username = userData[0];
                            string password = userData[1];
                            double weight = double.Parse(userData[2]);
                            double height = double.Parse(userData[3]);
                            string gender = userData[4];
                            double age = double.Parse(userData[5]);

                            User newUser = new User(username, password, gender, height, weight,age);
                            users.Add(newUser);
                        }
                        else
                        {
                            Console.WriteLine("");
                        }
                    }
                }
            }
        }
        public bool CreateUser(string username, string password)
        {
            // Check if the user already exists
            if (users.Exists(u => u.Username == username))
            {
                Console.WriteLine("Username already exists. Please choose a different one.");
                return false;
            }
            Console.Write("Enter your height (cm): ");
            double height = double.Parse(Console.ReadLine());

            Console.Write("Enter your weight (kg): ");
            double weight = double.Parse(Console.ReadLine());

            Console.Write("Enter your gender: ");
            string gender = Console.ReadLine();

            Console.Write("Enter your age: ");
            double age =double.Parse( Console.ReadLine());



            // Create a new user and add them to the list
            User newUser = new User(username, password, gender, height, weight, age);
            users.Add(newUser);

            // Save user information to a text file
            SaveUserToFile(newUser);

            Console.WriteLine("User created successfully. You can now log in.");
            return true;
        }

        public bool AuthenticateUser(string username, string password)
        {
            User user = users.Find(u => u.Username == username);

            if (user != null && user.Password == password)
            {
                currentUser = user; // Set the current user;
                Console.WriteLine("Login successful. Welcome, " + user.Username + "!");
                return true;
            }

            Console.WriteLine("Login failed. Please check your username and password.");
            return false;
        }

        private void SaveUserToFile(User user)
        {
            using (StreamWriter writer = File.AppendText("users.txt"))
            {
                writer.WriteLine($"{user.Username}:{user.Password}:{user.Weight}:{user.Height}:{user.Gender}");
            }
        }

      
        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Calculate BMI");
                Console.WriteLine("2. Calculate daily calories");

                Console.WriteLine("3. Log Out");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        // Implement BMI calculation logic here
                        double bmi = CalculateBMI(currentUser.Height, currentUser.Weight);
                        Console.WriteLine($"Your BMI is: {bmi:F2}");
                        break;

                        case "2":
                        Console.Write("Enter your age: ");
                        int age = int.Parse(Console.ReadLine());
                        Console.WriteLine("Choose your activity level: ");
                        Console.WriteLine("1. Inactive");
                        Console.WriteLine("2. Light");
                        Console.WriteLine("3. Moderate");
                        Console.WriteLine("4. Active");
                        Console.WriteLine("5. Very Active");
                        string activityChoice = Console.ReadLine();
                        string activityLevel = "sedentary";

                        switch (activityChoice)
                        {
                            case "1":
                                activityLevel = "Inactive";
                                break;
                            case "2":
                                activityLevel = "light";
                                break;
                            case "3":
                                activityLevel = "moderate";
                                break;
                            case "4":
                                activityLevel = "active";
                                break;
                            case "5":
                                activityLevel = "very active";
                                break;
                        }
                        double calories = Nutrition.CalculateDailyCalories(currentUser.Gender, currentUser.Weight, currentUser.Height, age, activityLevel);
                        Console.WriteLine($"Your daily calorie needs are: {calories:F2} calories");
                        break;


                    case "3":
                        currentUser = null; // Log out the current user
                        Console.WriteLine("Logged out. Goodbye!"); 
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose a valid option.");
                        break;
                }

                Console.WriteLine();
            }
          
    }
        double CalculateBMI(double height, double weight)
        {
            // Calculate BMI using the formula: BMI = weight (kg) / (height (m) * height (m))
            double heightInMeters = height / 100.0; // Convert height from cm to meters
            double bmi = weight / (heightInMeters * heightInMeters);
            Console.WriteLine($"Debug: Height (cm): {height}, Height (m): {heightInMeters}, Weight (kg): {weight}, BMI: {bmi}");
            return bmi;
        }
    }
}

