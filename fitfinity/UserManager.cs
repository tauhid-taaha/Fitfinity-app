﻿using Fitfinity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace fitfinity
{
    internal class UserManager
    {
        private List<User> users;
        private User currentUser;

        Dictionary<string, Dictionary<string, List<int>>> userData = new Dictionary<string, Dictionary<string, List<int>>>();
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
                ExerciseTracker exerciseTracker = new ExerciseTracker(currentUser.Weight);


                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Login successful. Welcome, " + user.Username + "!");
                Console.ResetColor();
                
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
            if (!userData.ContainsKey(currentUser.Username))
            {
                // If not, create an entry for the user with an empty daily calorie dictionary
                userData[currentUser.Username] = new Dictionary<string, List<int>>();
            }
            string name = "███████╗██╗████████╗███████╗██╗███╗   ██╗██╗████████╗██╗   ██╗\r\n██╔════╝██║╚══██╔══╝██╔════╝██║████╗  ██║██║╚══██╔══╝╚██╗ ██╔╝\r\n█████╗  ██║   ██║   █████╗  ██║██╔██╗ ██║██║   ██║    ╚████╔╝ \r\n██╔══╝  ██║   ██║   ██╔══╝  ██║██║╚██╗██║██║   ██║     ╚██╔╝  \r\n██║     ██║   ██║   ██║     ██║██║ ╚████║██║   ██║      ██║   \r\n╚═╝     ╚═╝   ╚═╝   ╚═╝     ╚═╝╚═╝  ╚═══╝╚═╝   ╚═╝      ╚═╝   \r\n                                                              ";
            string motto = "┬ ┬┌─┐┬ ┬┬─┐  ┌─┐┬┌┬┐┌┐┌┌─┐┌─┐┌─┐  ┌┬┐┬─┐┌─┐┌─┐┬┌─┌─┐┬─┐\r\n└┬┘│ ││ │├┬┘  ├┤ │ │ │││├┤ └─┐└─┐   │ ├┬┘├─┤│  ├┴┐├┤ ├┬┘\r\n ┴ └─┘└─┘┴└─  └  ┴ ┴ ┘└┘└─┘└─┘└─┘   ┴ ┴└─┴ ┴└─┘┴ ┴└─┘┴└─";
            void DisplayTitle2()
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(name);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(motto);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.ResetColor();
            }

            double bmi_for = 0;
            double bmr_for = 0;
            while (true)
            {


               
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Calculate BMI");
                Console.WriteLine("2. Calculate BMR");
                Console.WriteLine("3. Calculate daily steps manually And Determine Activity Level");
                Console.WriteLine("4. Calculate daily calories need");
                Console.WriteLine("5. Calculate Daily Calorie Intake");
                Console.WriteLine("6. Calculate Ideal Weight");
                Console.WriteLine("7. Set Goals");
                Console.WriteLine("8. See Previous BMI History");
                Console.WriteLine("9. Meal Planner");
                Console.WriteLine("10.User Profile");
                Console.WriteLine("11.Start Exercise Tracking");
                Console.WriteLine("12.Medical FAQ");

                Console.ResetColor();

                string choice = Console.ReadLine();
                record rc = new record();


                switch (choice)
                {
                    case "1":
                        Console.Clear   ();
                        DisplayTitle2 ();
                        Console.WriteLine("Choose an option:");
                        Console.WriteLine("1. Current BMI");
                        Console.WriteLine("2. Calculate BMI with new weight");

                        string bmiOption = Console.ReadLine();

                        switch (bmiOption)
                        {
                            case "1":
                                double currentBMI = CalculateBMI(currentUser.Height, currentUser.Weight);
                                Console.ForegroundColor= ConsoleColor.Cyan;
                                Console.WriteLine($"Your current BMI is: {currentBMI:F2}");
                                bmi_for = currentBMI;
                                Console.ResetColor ();
                                // RecordBMI("Previous BMI", currentBMI);
                                rc.RecordBMI(currentUser.Username, currentBMI);
                                break;

                            case "2":
                                Console.Write("Enter your new weight (kg): ");
                                double newWeight = double.Parse(Console.ReadLine());
                                double newBMI = CalculateBMI(currentUser.Height, newWeight);

                                BmiRecord newBmiRecord = new BmiRecord(currentUser.Username, newBMI);
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"New BMI: {newBMI:F2}, Recorded on: {newBmiRecord.RecordDate}");
                                Console.ResetColor();
                                rc.RecordBMI(currentUser.Username, newBMI);
                                break;

                            default:
                                Console.WriteLine("Invalid option. Please choose a valid option.");
                                break;
                        }
                        Console.ResetColor();
                        break;

                    case "2":Console.Clear ();
                        DisplayTitle2();
                        Console.WriteLine("Calculating BMR...");
                        double bmr = Nutrition.CalculateBmr(currentUser.Gender, currentUser.Weight, currentUser.Height, currentUser.age);
                        bmr_for = bmr;
                        Console.ForegroundColor = ConsoleColor.Cyan;

                        Console.WriteLine($"Your Basal Metabolic Rate (BMR) is: {bmr:F2} calories");
                        Console.ResetColor();
                        break;

                       

                    case "5":

                        Console.Clear();
                        DisplayTitle2();
                       
                       
                        Dictionary<string, List<int>> mealData = new Dictionary<string, List<int>>();
                        foodload fd = new foodload();
                        string record_file = @"C:\Users\DR.MEHBUB UL KADIR\Documents\spl\fitfinity\bin\Debug\calorietracker.txt";

                        while (true)
                        { Console.ForegroundColor= ConsoleColor.Cyan;
                            Console.WriteLine("Select a meal to input food details:");
                            Console.WriteLine("1. Breakfast");
                            Console.WriteLine("2. Lunch");
                            Console.WriteLine("3. Snacks");
                            Console.WriteLine("4. Dinner");
                            Console.WriteLine("5. Calculate Overall Daily Calories");
                            Console.WriteLine("6.VIEW PAST RECORD");
                            Console.WriteLine("7. Exit");
                            Console.ResetColor ();
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
                                                double totalCalories = fd.CalculateTotalCalories(mealType, selectedFoodIndices, grams);
                                                mealData[mealType] = mealData.ContainsKey(mealType)
                                                    ? mealData[mealType].Concat(new[] { (int)totalCalories }).ToList()
                                                    : new List<int> { (int)totalCalories };
                                                Console.WriteLine($"Total Calories for {mealType}: {totalCalories}");
                                                Console.Clear();
                                                DisplayTitle2();
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
                                                Console.ForegroundColor= ConsoleColor.Cyan;
                                                Console.Write("Enter the number of grams of this food item: ");
                                                Console.ResetColor();
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
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.Write($"Overall Daily Calories: ");
                                    Console.ForegroundColor=ConsoleColor.Red;
                                    Console.WriteLine(overallCalories );

                                    SaveDailyCaloriesData(currentUser.Username, DateTime.Now.ToString("yyyy-MM-dd"),(int)overallCalories);
                                    Console.WriteLine();
                                    Console.ResetColor();
                                    break;


                                case "6":
                                    ViewPastDailyCalories (currentUser.Username);
                                    Console.WriteLine();
                                    Console.ResetColor ();
                                    break;

                                case "7": // Exit the program
                                    Console.Clear();
                                    DisplayTitle2();
                                    ShowMenu();
                                    return;

                                default:
                                    Console.WriteLine("Enter a valid option.");
                                    break;
                            }
                          
                             void ViewPastDailyCalories(string username)
                            {
                                Console.Write("Enter the number of past days to view calories: ");
                                if (int.TryParse(Console.ReadLine(), out int days))
                                {
                                    LoadPastDailyCalories(username, days);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid number of days.");
                                }
                            }
                            



                            void SaveDailyCaloriesData(string username, string date, int overallCalories)
                            {
                                using (StreamWriter writer = new StreamWriter(record_file, append: true))
                                {
                                    writer.WriteLine($"{username}, {date}, Overall Daily Calories: {overallCalories}");
                                }
                            }


                            void LoadPastDailyCalories(string username, int days)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"Viewing daily overall calories for the past {days} days for {username}:");
                                Console.ResetColor();

                                if (userData.ContainsKey(username))
                                {
                                    Dictionary<string, List<int>> userMealData = userData[username];

                                    List<string> lines = File.ReadLines(record_file).Reverse().ToList();

                                    int count = 0;
                                    foreach (string line in lines)
                                    {
                                        string[] parts = line.Split(',');

                                        if (parts.Length >= 3)
                                        {
                                            string storedUsername = parts[0].Trim();
                                            string storedDate = parts[1].Trim();

                                            if (storedUsername == username)
                                            {
                                                // Assume the overall calories are stored after "Overall Daily Calories:"
                                                int startIndex = line.IndexOf("Overall Daily Calories:") + "Overall Daily Calories:".Length;
                                                string overallCaloriesString = line.Substring(startIndex).Trim();

                                                if (int.TryParse(overallCaloriesString, out int overallCalories))
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                                    Console.WriteLine($"{storedUsername}, {storedDate}, Overall Daily Calories: {overallCalories}");
                                                    Console.ResetColor();

                                                    count++;

                                                    if (count >= days)
                                                    {
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (count == 0)
                                    {
                                        Console.WriteLine($"No records found for user {username} in the past {days} days.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"User {username} not found.");
                                }
                            }





                        }
                    case "11":
                        ExerciseTracker exerciseTracker = new ExerciseTracker(currentUser.Weight);
                        exerciseTracker.StartExerciseTracker();
                        break;
                    // Add the following case in the UserManager class

                    // Add the following case in the UserManager class

                    case "12":
                        MedicalFAQ medicalFAQ = new MedicalFAQ();
                        string selectedQuestion = "";

                        do
                        {
                            
                            medicalFAQ.DisplayFAQ();
                            Console.Write("\nEnter the number of the question you want to view, type 'back' to go back to the main menu, or 'prev' to go back to the previous questions: ");
                            string input = Console.ReadLine();

                            if (input.ToLower() == "back")
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\nGoing back to the main menu.");
                                Thread.Sleep(3000);
                                Console.ResetColor();
                                Console.Clear();
                                DisplayTitle2();
                                break; // Exit the do-while loop to return to the main menu
                            }
                            else if (input.ToLower() == "prev" && !string.IsNullOrEmpty(selectedQuestion))
                            {
                                selectedQuestion = "";
                                Console.WriteLine("\nGoing back to the previous questions.");
                            }
                            else if (int.TryParse(input, out int questionNumber))
                            {
                                Console.Clear();
                                DisplayTitle2();
                                medicalFAQ.AnswerQuestion(questionNumber.ToString());
                                selectedQuestion = questionNumber.ToString();
                            }
                            else
                            {
                                Console.WriteLine("\nInvalid input. Please enter a valid question number, 'back', or 'prev'.");
                            }

                        } while (true);

                        break;




                    case "4":
                        Console.Clear();
                        DisplayTitle2();

                        Console.WriteLine("Choose your activity level: ");
                        Console.WriteLine("1. Inactive: Little to no exercise");
                        Console.WriteLine("2. Light: Light exercise/sports 1-3 days/week");
                        Console.WriteLine("3. Moderate: Moderate exercise/sports 3-5 days/week");
                        Console.WriteLine("4. Active: Active - Hard exercise/sports 6-7 days a week");
                        Console.WriteLine("5. Very Active: Very Active - Very hard exercise/sports & physical job or 2x training");
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
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Your daily calorie needs are: {calories:F2} calories");
                        Console.ResetColor();
                        break;


                    case "7":
                        Console.ForegroundColor= ConsoleColor.Cyan;
                        Console.WriteLine("Choose your activity level: ");
                        Console.ResetColor();
                        Console.WriteLine("1. Inactive: Little to no exercise");
                        Console.WriteLine("2. Light: Light exercise/sports 1-3 days/week");
                        Console.WriteLine("3. Moderate: Moderate exercise/sports 3-5 days/week");
                        Console.WriteLine("4. Active: Active - Hard exercise/sports 6-7 days a week");
                        Console.WriteLine("5. Very Active: Very Active - Very hard exercise/sports & physical job or 2x training");
                        string activityChoice2 = Console.ReadLine();

                        fitness_recommendation fr = new fitness_recommendation();


                        
                        Console.WriteLine("Choose your goal:");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("1. Weight Loss");
                        Console.WriteLine("2. Weight Gain");
                        string goalChoice = Console.ReadLine();

                        if (int.TryParse(goalChoice, out int selectedGoal))
                        {
                            // Get the weight and age from the current user
                            double currentWeight = currentUser.Weight;


                            Console.Write("Enter the duration of your goal in days: ");
                            int goalDurationDays = int.Parse(Console.ReadLine());

                            double targetWeight = Nutrition.CalculateIdealWeight(currentUser.Gender, currentUser.Height);

                            // Check if the target weight is not less than current weight for weight loss
                            if (selectedGoal == 1 && targetWeight >= currentWeight)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Sorry, it's not healthy for you to lose weight as you are already underweight.");
                                Console.ResetColor();
                                break;
                            }

                            // Check if the target weight is not greater than current weight for weight gain
                            if (selectedGoal == 2 && targetWeight <= currentWeight)
                            {   Console.ForegroundColor= ConsoleColor.Red;
                                Console.WriteLine("Sorry, it's not healthy for you to gain weight as you are already overweight.");
                                Console.ResetColor();
                                break;
                            }
                            Console.Clear();
                            DisplayTitle2();
                            double weightChangePerWeek = Math.Abs((currentWeight - targetWeight) / (goalDurationDays / 7.0));
                            double dailyCaloricDeficit = weightChangePerWeek * 7700 / 7.0;
                            Console.ForegroundColor=ConsoleColor.Red;
                            Console.WriteLine($"Your target weight: {targetWeight:F2} kg");
                            Console.WriteLine($"To achieve your goal in {goalDurationDays} days:");
                            Console.ForegroundColor=ConsoleColor.Yellow;
                            if (selectedGoal == 1)
                            {
                                Console.WriteLine($"You need to lose approximately {weightChangePerWeek:F2} kg per week.");
                            }
                            else
                            {
                                Console.WriteLine($"You need to gain approximately {weightChangePerWeek:F2} kg per week.");
                            }
                           ;
                            Console.WriteLine($"Maintain a daily caloric deficit of {dailyCaloricDeficit:F2} calories.");
                            Console.ResetColor();
                            Console.WriteLine();
                            // Suggested diet and exercise plans (based on BMR and goal)
                            fitness_recommendation fR = new fitness_recommendation();
                            double bmi= CalculateBMI(currentUser.Height, currentUser.Weight);
                            string workoutSuggestion = fR.GenerateWorkoutSuggestion(currentUser.ActivityLevel, goalChoice,bmi);
                            string dietSuggestion = fR.GenerateDietSuggestion(goalChoice, Nutrition.CalculateBmr(currentUser.Gender, currentUser.Weight, currentUser.Height, currentUser.age));
                            if(bmi < 18.5 ){
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("WARNING : YOUR BMI IS : ");
                                Console.ForegroundColor=ConsoleColor.Yellow;
                                Console.WriteLine(bmi );
                                Console.ForegroundColor= ConsoleColor.Red;
                                Console.WriteLine("Which Indicates Underweight.You Should Gain Some Weight or Consult a Doctor");
                                Console.ResetColor();
                                    
                                    
                                    }
                            else if(bmi > 23.5 && bmi < 25)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("WARNING : YOUR BMI IS : ");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(bmi);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Which Refers To Obesity.Please maintain a healthy Lifestyle");
                                Console.ResetColor();


                            }
                            else if (bmi>25 && bmi < 30)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("WARNING : YOUR BMI IS : ");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(bmi);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Which Refers To OverWeight.Please Maintain a Balanced Diet and Proper Workout Plans to Lose Some Weight");
                                Console.ResetColor();
                            }
                            else if (bmi > 35) {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("WARNING : YOUR BMI IS : ");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(bmi);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Which Falls On The Border of Normal Weight and OverWeight.Maintain A Healthy Lifestyle");
                                Console.ResetColor();
                            }
                            Console.ForegroundColor= ConsoleColor.Cyan;
                            Console.WriteLine("\nPersonalized Recommendations:");
                            Console.WriteLine(workoutSuggestion);
                            Console.WriteLine();
                            Console.WriteLine(dietSuggestion);
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine("Invalid goal choice. Please select a valid option.");
                        }
                        break;


                    case "8":
                        Console.ForegroundColor = ConsoleColor.Red;
                        rc.PrintAllRecords(currentUser.Username);
                        Console.ResetColor();
                        break;
                    case "6":
                        Console.Clear();
                        DisplayTitle2();

                        Console.WriteLine("Calculating Ideal Weight:");
                        double idealWeight = Nutrition.CalculateIdealWeight(currentUser.Gender, currentUser.Height);
                        Console.ForegroundColor= ConsoleColor.DarkYellow;
                        Console.WriteLine($"Your Ideal Weight is: {idealWeight:F2} kg");
                        Console.ResetColor();
                        break;



                    case "10": // User Profile
                        DisplayUserProfile(currentUser);
                        break;

                        // Add the following method to your UserManager class
                        void DisplayUserProfile(User user)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("User Profile");
                            Console.WriteLine($"Name:             {user.Username}");
                            Console.WriteLine($"Age:              {user.age}");
                            Console.WriteLine($"Height:           {user.Height} cm");
                            Console.WriteLine($"BMI:              {CalculateBMI(user.Height, user.Weight):F2}");
                            Console.WriteLine($"BMR:              {Nutrition.CalculateBmr(user.Gender, user.Weight, user.Height, user.age):F2} calories");
                            Console.WriteLine($"Current Weight:   {user.Weight} kg");
                            Console.ResetColor();
                            Console.WriteLine("Options:");
                            Console.WriteLine("1. Update Weight");
                            Console.WriteLine("2. Log Out");
                            Console.WriteLine("3. Go Back");

                            string userProfileOption = Console.ReadLine();

                            switch (userProfileOption)
                            {
                                case "1":
                                    Console.Write("Enter your new weight (kg): ");
                                    if (double.TryParse(Console.ReadLine(), out double newWeight))
                                    {
                                        user.Weight = newWeight;
                                        SaveUserToFile(user);
                                        Console.WriteLine("Weight updated successfully.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input for weight. Please enter a valid number.");
                                    }
                                    break;

                                case "2":
                                    currentUser = null;
                                    Console.WriteLine("Logged out. Goodbye!");
                                    break;

                                case "3":
                                    // Go back to the main menu
                                    break;

                                default:
                                    Console.WriteLine("Invalid option. Please enter a valid choice.");
                                    break;
                            }
                        }


                    case "9":

                        string filePath = @"C:\Users\DR.MEHBUB UL KADIR\Documents\spl\fitfinity\bin\Debug\calories2.txt";
                       
                        Dictionary<string, Foods> foodDatabase = ReadFoodDatabase(filePath);

                        Console.Write("Enter your daily calorie goal for BREAKFAST: ");
                        if (double.TryParse(Console.ReadLine(), out double breakfastCalorieGoal))
                        {
                            Console.Write("Enter your daily calorie goal for LUNCH: ");
                            if (double.TryParse(Console.ReadLine(), out double lunchCalorieGoal))
                            {
                                Console.Write("Enter your daily calorie goal for SNACKS: ");
                                if (double.TryParse(Console.ReadLine(), out double snacksCalorieGoal))
                                {
                                    Console.Write("Enter your daily calorie goal for DINNER: ");
                                    if (double.TryParse(Console.ReadLine(), out double dinnerCalorieGoal))
                                    {
                                        Dictionary<string, List<Foods>> mealPlan = GenerateRandomMealPlan(foodDatabase, breakfastCalorieGoal, lunchCalorieGoal, snacksCalorieGoal, dinnerCalorieGoal);

                                        Console.Clear();
                                        DisplayTitle2();
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.WriteLine("\nYour Random Meal Plan:");
                                        foreach (var entry in mealPlan)
                                        {   Console.ForegroundColor= ConsoleColor.Red;
                                            Console.WriteLine($"\n{entry.Key.ToUpper()}:");
                                            Console.ResetColor();
                                            foreach (var foodItem in entry.Value)
                                            {Console.ForegroundColor= ConsoleColor.Yellow;
                                                Console.WriteLine($"{foodItem.Name}, {foodItem.Weight} grams, {foodItem.Calorie} calories, {foodItem.FoodType}");
                                            }
                                        }
                                        Console.ResetColor();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid input for dinner calorie goal. Please enter a valid number.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input for snacks calorie goal. Please enter a valid number.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid input for lunch calorie goal. Please enter a valid number.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid input for breakfast calorie goal. Please enter a valid number.");
                        }

                        break;
                    case "3":
                        // Use the current user's gender and height
                        string gender = currentUser.Gender;
                        double height = currentUser.Height; // Convert height from cm to inches

                        Console.Write($"Enter how much time you took to walk a mile in minutes (Use Watch): ");
                        if (double.TryParse(Console.ReadLine(), out double minutesPerMile))
                        {
                            Console.Write("Enter how many miles you have walked: ");
                            if (double.TryParse(Console.ReadLine(), out double milesWalked))
                            {
                                // Calculate daily steps based on the provided information
                                int totalSteps = Exercise.CalculateStepsPerMile(minutesPerMile, height, gender, milesWalked);
                                Console.Clear();
                                DisplayTitle2();
                                Console.ForegroundColor= ConsoleColor.Cyan;
                                Console.WriteLine($"Your daily steps: {totalSteps}");
                                Console.WriteLine("Manual step counting, without the use of sensors, may sometimes lead to inaccuracies in results.");
                               
                                // Determine activity level based on the calculated daily steps
                                string ActivityLevel = Exercise.DetermineActivityLevel(totalSteps);
                                Console.WriteLine($"Your activity level: {ActivityLevel}");
                                Console.ResetColor();
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
            Console.ForegroundColor= ConsoleColor.Cyan;

            Console.WriteLine($"Height (cm): {height}, Height (m): {heightInMeters}, Weight (kg): {weight}, BMI: {bmi}");
            return bmi;
            Console.ResetColor();
        }



        private void SaveUsersToFile()
        {
            // Save all users to the file
            File.WriteAllLines("users.txt", users.Select(u => $"{u.Username}:{u.Password}:{u.Weight}:{u.Height}:{u.Gender}:{u.age}"));
        }

        private void RecordBMI(string date, double newBMI)
        {
            string filePath = @"C:\Users\DR.MEHBUB UL KADIR\Documents\spl\fitfinity\bin\Debug\record.txt";

            // Record the date and new BMI in a file
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine($"{date},{newBMI:F2}");
            }
        }


        static Dictionary<string, Foods> ReadFoodDatabase(string filePath)
        {
            Dictionary<string, Foods> foodDatabase = new Dictionary<string, Foods>();

            foreach (string line in File.ReadLines(filePath))
            {
                string[] parts = line.Split(',');
                if (parts.Length == 4) // Adjust the length to match your new file format
                {
                    string name = parts[0].Trim();
                    double calorie = double.Parse(parts[1]);
                    string mealType = parts[2].Trim().ToLower();
                    string foodType = parts[3].Trim(); // Assuming the food type is the fourth element

                    foodDatabase[name] = new Foods(name, calorie, mealType, foodType);
                }
            }

            return foodDatabase;
        }
        static Dictionary<string, List<Foods>> GenerateRandomMealPlan(Dictionary<string, Foods> foodDatabase, double breakfastCalorieGoal, double lunchCalorieGoal, double snacksCalorieGoal, double dinnerCalorieGoal)
        {
            Dictionary<string, List<Foods>> mealPlan = new Dictionary<string, List<Foods>>()
    {
        {"breakfast", new List<Foods>()},
        {"lunch", new List<Foods>()},
        {"snacks", new List<Foods>()},
        {"dinner", new List<Foods>()}
    };

            Random random = new Random();
            int maxAttempts = 100; // Maximum number of attempts to find a suitable combination

            foreach (var mealType in mealPlan.Keys)
            {
                double calorieGoal = 0;

                switch (mealType)
                {
                    case "breakfast":
                        calorieGoal = breakfastCalorieGoal;
                        break;
                    case "lunch":
                        calorieGoal = lunchCalorieGoal;
                        break;
                    case "snacks":
                        calorieGoal = snacksCalorieGoal;
                        break;
                    case "dinner":
                        calorieGoal = dinnerCalorieGoal;
                        break;
                }

                for (int attempt = 0; attempt < maxAttempts; attempt++)
                {
                    mealPlan[mealType].Clear(); // Clear previous attempts

                    // Define the target ratio of calories for each food type
                    double proteinRatio = 1.0 / 3.0;
                    double carbRatio = 1.0 / 6.0;
                    double vegetableRatio = 1.0 / 2.0;

                    // Calculate the target calories for each food type
                    double proteinCalories = calorieGoal * proteinRatio;
                    double carbCalories = calorieGoal * carbRatio;
                    double vegetableCalories = calorieGoal * vegetableRatio;

                    // Add a random protein item
                    AddRandomFoodItem(mealPlan[mealType], foodDatabase, "Protein", proteinCalories, random);

                    // Add a random carb item
                    AddRandomFoodItem(mealPlan[mealType], foodDatabase, "Carb", carbCalories, random);

                    // Add a random vegetable item
                    AddRandomFoodItem(mealPlan[mealType], foodDatabase, "Vegetable", vegetableCalories, random);

                    // Check if the generated combination meets the criteria
                    if (IsTargetRatioMet(mealPlan[mealType], proteinRatio, carbRatio, vegetableRatio))
                    {
                        break; // Valid combination found
                    }
                }
            }

            return mealPlan;
        }

        static void AddRandomFoodItem(List<Foods> mealList, Dictionary<string, Foods> foodDatabase, string foodType, double targetCalories, Random random)
        {
            List<Foods> availableFoodItems = foodDatabase.Values
                .Where(food => food.FoodType == foodType)
                .ToList();

            if (availableFoodItems.Count > 0)
            {
                int randomIndex = random.Next(availableFoodItems.Count);
                Foods randomFoodItem = availableFoodItems[randomIndex];

                double maxWeight = targetCalories / randomFoodItem.Calorie;
                double minWeight = Math.Max(0, maxWeight * 0.95); // 5% below the max
                double suggestedWeight = random.NextDouble() * (maxWeight - minWeight) + minWeight;

                // Add the food item to the meal plan with the calculated weight
                mealList.Add(new Foods(randomFoodItem.Name, randomFoodItem.Calorie, suggestedWeight, randomFoodItem.MealType, randomFoodItem.FoodType));
            }
        }

        private static string ReadUserInfo(string username)
        {
            // Specify the path to the user.txt file
            string filePath = @"C:\Users\DR.MEHBUB UL KADIR\Documents\spl\fitfinity\bin\Debug\users.txt";

            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Find the line with the user information based on the username
                foreach (string line in lines)
                {
                    string[] userValues = line.Split(':');
                    if (userValues[0].Equals(username, StringComparison.OrdinalIgnoreCase))
                    {
                        return line;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found. Please make sure the user.txt file exists.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return null;
        }
    
    static bool IsTargetRatioMet(List<Foods> mealList, double proteinRatio, double carbRatio, double vegetableRatio)
        {
            double proteinCalories = 0;
            double carbCalories = 0;
            double vegetableCalories = 0;

            foreach (var food in mealList)
            {
                switch (food.FoodType)
                {
                    case "Protein":
                        proteinCalories += food.Calorie * food.Weight;
                        break;
                    case "Carb":
                        carbCalories += food.Calorie * food.Weight;
                        break;
                    case "Vegetable":
                        vegetableCalories += food.Calorie * food.Weight;
                        break;
                }
            }

            // Check if the actual ratios are close to the target ratios
            double epsilon = 0.01; // Adjust as needed
            return Math.Abs(proteinCalories / mealList.Sum(food => food.Calorie) - proteinRatio) < epsilon &&
                   Math.Abs(carbCalories / mealList.Sum(food => food.Calorie) - carbRatio) < epsilon &&
                   Math.Abs(vegetableCalories / mealList.Sum(food => food.Calorie) - vegetableRatio) < epsilon;
        }





        static void PrintMealPlan(List<Foods> meal)
        {
            foreach (var foodItem in meal)
            {
                Console.WriteLine($"{foodItem.Name}, {foodItem.Weight} grams, {foodItem.Calorie} calories, {foodItem.FoodType}");
            }
        }
        static bool CheckMacroRatios(List<Foods> meal, double proteinRatio, double carbRatio, double vegetableRatio)
        {
            var proteinWeight = meal.Where(food => food.FoodType == "Protein").Sum(food => food.Weight);
            var carbWeight = meal.Where(food => food.FoodType == "Carb").Sum(food => food.Weight);
            var vegetableWeight = meal.Where(food => food.FoodType == "Vegetable").Sum(food => food.Weight);

            return Math.Abs(proteinWeight / carbWeight - proteinRatio / carbRatio) < 0.1
                && Math.Abs(proteinWeight / vegetableWeight - proteinRatio / vegetableRatio) < 0.1;
        }

        string calorie_file= @"C:\Users\DR.MEHBUB UL KADIR\Documents\spl\fitfinity\bin\Debug\calorietracker.txt";
       public void LoadDailyCaloriesData(string username)
        {
            if (File.Exists(calorie_file))
            {
                string[] calorieLines = File.ReadAllLines(calorie_file);

                foreach (string calorieLine in calorieLines)
                {
                    string[] calorieParts = calorieLine.Split(',');
                    if (calorieParts.Length == 3)
                    {
                        string storedUsername = calorieParts[0].Trim();
                        if (storedUsername == username)
                        {
                            string date = calorieParts[1].Trim();
                            string[] mealDataParts = calorieParts[2].Split(';');
                            Dictionary<string, List<int>> userDailyCalories = new Dictionary<string, List<int>>();

                            foreach (string mealDataPart in mealDataParts)
                            {
                                string[] mealParts = mealDataPart.Split(':');
                                if (mealParts.Length == 2)
                                {
                                    string mealType2 = mealParts[0].Trim();
                                    List<int> caloriesList = mealParts[1].Split(',').Select(int.Parse).ToList();
                                    userDailyCalories[mealType2] = caloriesList;
                                }
                            }

                            // Update the userData dictionary with the loaded data for the current user
                            userData[username] = userDailyCalories;

                            // Exit the loop after loading the data for the current user
                            break;
                        }
                    }
                }
            }
        }
        static void DisplayMealPlan(string mealType, Dictionary<string, List<Foods>> mealPlan)
        {
            Console.WriteLine($"\nYour Random {mealType} Meal Plan:");
            foreach (var entry in mealPlan)
            {
                Console.WriteLine($"\n{entry.Key.ToUpper()}:");
                foreach (var foodItem in entry.Value)
                {
                    Console.WriteLine($"{foodItem.Name}, {foodItem.Weight} grams, {foodItem.Calorie} calories, {foodItem.FoodType}");
                }
            }
        }
    }
}