using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
                            int age = int.Parse(userData[5]);

                            User newUser = new User(username, password, gender, height, weight, age);
                            users.Add(newUser);
                        }
                        else
                        {
                            Console.Write("");
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

            Console.WriteLine("Select your gender:");
            Console.WriteLine("1. Male");
            Console.WriteLine("2. Female");
            Console.WriteLine("3. Other");

            string genderChoice = Console.ReadLine();
            string gender;

            switch (genderChoice)
            {
                case "1":
                    gender = "Male";
                    break;
                case "2":
                    gender = "Female";
                    break;
                case "3":
                    Console.Write("other");
                    gender = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Invalid gender choice. Please select a valid option.");
                    return false;
            }

            Console.Write("Enter your age: ");
            int age = int.Parse(Console.ReadLine());

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

            if ((user != null && (user.Password == password)))
            {
                currentUser = user; // Set the current user;
                Console.WriteLine("Login successful. Welcome, " + user.Username + "!");

               /// Record "previous BMI" for the first BMI count
                if (string.IsNullOrEmpty(user.BMIRecordFilePath))
                {
                    
                }

                return true;
            }

            Console.WriteLine("Login failed. Please check your username and password.");
            return false;
        }

        private void SaveUserToFile(User user)
        {
            using (StreamWriter writer = File.AppendText("users.txt"))
            {
                writer.WriteLine($"{user.Username}:{user.Password}:{user.Weight}:{user.Height}:{user.Gender}:{user.age}");
            }
        }

        public void ShowMenu()

        {

            double bmi_for = 0;
            double bmr_for = 0;
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Calculate BMI");
                Console.WriteLine("2. Calculate BMR");

                Console.WriteLine("3. Calculate steps per mile and determine Activity Level");
                Console.WriteLine("4. Calculate daily calories");
                Console.WriteLine("5. Calculate Daily Calorie Intake");
                Console.WriteLine("6. Set Goals");
                Console.WriteLine("7. See Details");

                string choice = Console.ReadLine();
                record rc = new record();
                

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Choose an option:");
                        Console.WriteLine("1. Current BMI");
                        Console.WriteLine("2. Calculate BMI with new weight");

                        string bmiOption = Console.ReadLine();

                        switch (bmiOption)
                        {
                            case "1":
                                double currentBMI = CalculateBMI(currentUser.Height, currentUser.Weight);
                                Console.WriteLine($"Your current BMI is: {currentBMI:F2}");
                                bmi_for = currentBMI;
                                RecordBMI("Previous BMI", currentBMI);
                                break;

                            case "2":
                                Console.Write("Enter the date (e.g., MM/DD/YYYY): ");
                                string date = Console.ReadLine();
                                Console.Write("Enter your new weight (kg): ");
                                double newWeight = double.Parse(Console.ReadLine());
                                double newBMI = CalculateBMI(currentUser.Height, newWeight);
                                UpdateWeight(date, newWeight, newBMI);
                                break;

                            default:
                                Console.WriteLine("Invalid option. Please choose a valid option.");
                                break;
                        }
                        break;

                    case "2":
                        Console.WriteLine("Calculating BMR...");
                        double bmr = Nutrition.CalculateBmr(currentUser.Gender, currentUser.Weight, currentUser.Height, currentUser.age);
                        bmr_for = bmr;
                        Console.WriteLine($"Your Basal Metabolic Rate (BMR) is: {bmr:F2} calories");
                        break;



                    case "5":

                        Dictionary<string, List<int>> mealData = new Dictionary<string, List<int>>();
                        foodload fd = new foodload();

                        while (true)
                        {
                            Console.WriteLine("Select a meal to input food details:");
                            Console.WriteLine("1. Breakfast");
                            Console.WriteLine("2. Lunch");
                            Console.WriteLine("3. Snacks");
                            Console.WriteLine("4. Dinner");
                            Console.WriteLine("5. Calculate Overall Daily Calories");
                            Console.WriteLine("6. Exit");

                            string mealType = Console.ReadLine();

                            switch (mealType)
                            {
                                case "1":
                                    mealType = "breakfast";
                                    break;
                                case "2":
                                    mealType = "lunch";
                                    break;
                                case "3":
                                    mealType = "snacks";
                                    break;
                                case "4":
                                    mealType = "dinner";
                                    break;
                                    // ... (unchanged code)
                            }

                            switch (mealType.ToLower())
                            {
                                case "breakfast": // Breakfast
                                case "lunch": // Lunch
                                case "snacks": // Snacks
                                case "dinner": // Dinner
                                    List<int> selectedFoodIndices = new List<int>();
                                    List<int> grams = new List<int>();

                                    Console.WriteLine($"Available Food Options for {mealType}:");
                                    fd.PrintFoodNames(mealType);

                                    while (true)
                                    {
                                        Console.Write("Enter the number of a food item to select (0 to calculate calories or -1 to exit): ");
                                        if (int.TryParse(Console.ReadLine(), out int selected))
                                        {
                                            if (selected == 0)
                                            {
                                                // Calculate total calories for the meal and store in the mealData dictionary
                                                double totalCalories = fd.CalculateTotalCalories(mealType,selectedFoodIndices, grams);
                                                mealData[mealType] = mealData.ContainsKey(mealType)
                                                    ? mealData[mealType].Concat(new[] { (int)totalCalories }).ToList()
                                                    : new List<int> { (int)totalCalories };
                                                Console.WriteLine($"Total Calories for {mealType}: {totalCalories}");
                                                break;
                                            }
                                            else if (selected == -1)
                                            {
                                                // Exit the loop
                                                break;
                                            }
                                            else if (fd.IsValidFoodIndex(mealType, selected))
                                            {
                                                selectedFoodIndices.Add(selected - 1);
                                                Console.Write("Enter the number of grams of this food item: ");
                                                if (int.TryParse(Console.ReadLine(), out int gram))
                                                {
                                                    grams.Add(gram);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Invalid input for grams. Please try again.");
                                                }
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
                                    break;

                                case "5": // Calculate Overall Daily Calories
                                    double overallCalories = mealData.Values.SelectMany(list => list).Sum();
                                    Console.WriteLine($"Overall Daily Calories: {overallCalories}");
                                    break;

                                case "6": // Exit the program
                                    ShowMenu();
                                    return ;

                                default:
                                    Console.WriteLine("Enter a valid option.");
                                    break;
                            }
                        }

                    case "4":

                        Console.WriteLine("Choose your activity level: ");
                        Console.WriteLine("1. Inactive");
                        Console.WriteLine("2. Light");
                        Console.WriteLine("3. Moderate");
                        Console.WriteLine("4. Active");
                        Console.WriteLine("5. Very Active");
                        string activityChoice = Console.ReadLine();
                        string activityLevel = Console.ReadLine();

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
                        double calories = Nutrition.CalculateDailyCalories(currentUser.Gender, currentUser.Weight, currentUser.Height, currentUser.age, activityLevel);
                        Console.WriteLine($"Your daily calorie needs are: {calories:F2} calories");
                        break;


                    case "6":
                        Console.WriteLine("Choose your activity level: ");
                        Console.WriteLine("1. Inactive");
                        Console.WriteLine("2. Light");
                        Console.WriteLine("3. Moderate");
                        Console.WriteLine("4. Active");
                        Console.WriteLine("5. Very Active");
                        string activityChoice2 = Console.ReadLine();

                        fitness_recommendation fr= new fitness_recommendation();


                        Console.WriteLine("Choose your goal:");
                        Console.WriteLine("1. Weight Loss");
                        Console.WriteLine("2. Underweight");
                        Console.WriteLine("3. Keep Healthy");

                        string goalChoice = Console.ReadLine();

                        if (int.TryParse(goalChoice, out int selectedGoal))
                        {
                            string workoutSuggestion = fr.GenerateWorkoutSuggestion(activityChoice2, goalChoice, bmi_for);
                            string dietSuggestion = fr.GenerateDietSuggestion(goalChoice, bmr_for);

                            Console.WriteLine("\nPersonalized Recommendations:");
                            Console.WriteLine(workoutSuggestion);
                            Console.WriteLine(dietSuggestion);
                        }
                        else
                        {
                            Console.WriteLine("Invalid goal choice. Please select a valid option.");
                        }
                        break;



                    case "7":
                        rc.PrintAllRecords();
                        return;

                    case "8":
                        currentUser = null; 
                        Console.WriteLine("Logged out. Goodbye!"); 
                        return;
                    case "3":
                        Console.Write("Enter your gender (Male/Female): ");
                        string gender = Console.ReadLine();

                        Console.Write("Enter your height in inches: ");
                        if (double.TryParse(Console.ReadLine(), out double heightInches))
                        {
                            Console.Write("Enter your pace in minutes per mile: ");
                            if (double.TryParse(Console.ReadLine(), out double paceMinutesPerMile))
                            {
                                Console.Write("Enter how many miles you have walked: ");
                                if (double.TryParse(Console.ReadLine(), out double milesWalked))
                                {
                                    // Calculate daily steps based on the provided information
                                    int totalSteps = Exercise.CalculateStepsPerMile(paceMinutesPerMile, heightInches, gender, milesWalked);
                                    Console.WriteLine($"Your daily steps: {totalSteps}");

                                    // Determine activity level based on the calculated daily steps
                                    String ActivityLevel = Exercise.DetermineActivityLevel(totalSteps);
                                    Console.WriteLine($"Your activity level: {ActivityLevel}");
                                }
                                else




                                {
                                    Console.WriteLine("Invalid miles walked value. Please enter a valid number.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid pace value. Please enter a valid number.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid height value. Please enter a valid number.");
                        }
                        break;

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

        void UpdateWeight(string date, double newWeight, double newBMI)
        {
            // Find the user in the list
            User userToUpdate = users.Find(u => u.Username == currentUser.Username);

            if (userToUpdate != null)
            {
                // Update the user's weight and save the changes to the file
                userToUpdate.Weight = newWeight;
                SaveUsersToFile();

                // Record the date and new BMI in an auto-generated text file
                RecordBMI(date, newBMI);

                Console.WriteLine($"Weight updated successfully on {date}. New weight: {newWeight} kg");
            }
            else
            {
                Console.WriteLine("User not found. Weight update failed.");
            }
        }

        private void SaveUsersToFile()
        {
            // Save all users to the file
            File.WriteAllLines("users.txt", users.Select(u => $"{u.Username}:{u.Password}:{u.Weight}:{u.Height}:{u.Gender}:{u.age}"));
        }

        private void RecordBMI(string date, double newBMI)
        {
            string filePath = @"C:\Users\Tauhid\Downloads\SPL\SPL\fitfinity\bin\Debug\record.txt";

            // Record the date and new BMI in a file
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine($"{date},{newBMI:F2}");
            }
        }
    }
}