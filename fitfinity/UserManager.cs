using System;
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
            //newadd

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
            int age =int.Parse( Console.ReadLine());



            // Create a new user and add them to the list
            User newUser = new User( username,password,gender,height,weight,age);
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
        List<Foods> foodsList = new List<Foods>();

        public void ShowMenu()

        {
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Calculate BMI");
                Console.WriteLine("2. Calculate BMR");

                Console.WriteLine("3. Calculate steps per mile and determine Activity Level");
                Console.WriteLine("4. Calculate daily calories");
                Console.WriteLine("5. Calculate Daily Calorie Intake");
                Console.WriteLine("6.Set Goals");

                Console.WriteLine("7. Log Out");
                
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": //
                        // Implement BMI calculation logic here
                        double bmi = CalculateBMI(currentUser.Height, currentUser.Weight);
                        Console.WriteLine($"Your BMI is: {bmi:F2}");
                        string classification = ClassifyBMI(bmi);
                        Console.WriteLine($"BMI Classification: {classification}");
                        

                       string ClassifyBMI(double bm)
                        {
                            if (bmi < 16)
                            {
                                return "Severe Thinness";
                            }
                            else if (bmi >= 16 && bmi < 17)
                            {
                                return "Moderate Thinness";
                            }
                            else if (bmi >= 17 && bmi < 18.5)
                            {
                                return "Mild Thinness";
                            }
                            else if (bmi >= 18.5 && bmi < 25)
                            {
                                return "Normal";
                            }
                            else if (bmi >= 25 && bmi < 30)
                            {
                                return "Overweight";
                            }
                            else if (bmi >= 30 && bmi < 35)
                            {
                                return "Obese Class I";
                            }
                            else if (bmi >= 35 && bmi < 40)
                            {
                                return "Obese Class II";
                            }
                            else
                            {
                                return "Obese Class III";
                            }
                        }

                        break;
                        //newadd

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
                                case "1": // Breakfast
                                case "2": // Lunch
                                case "3": // Snacks
                                case "4": // Dinner
                                    List<int> selectedFoodIndices = new List<int>();
                                    List<int> grams = new List<int>();

                                    Console.WriteLine($"Available Food Options for {mealType}:");
                                    fd.PrintAllFoodNames();

                                    while (true)
                                    {
                                        Console.Write("Enter the number of a food item to select (0 to calculate calories or -1 to exit): ");
                                        if (int.TryParse(Console.ReadLine(), out int selected))
                                        {
                                            if (selected == 0)
                                            {
                                                // Calculate total calories for the meal and store in the mealData dictionary
                                                double totalCalories = fd.CalculateTotalCalories(selectedFoodIndices, grams);
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
                                            else if (selected > 0 && selected <= fd.foodsList.Count)
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

                

                            
                    case "6":
                        Console.WriteLine("Choose your goal:");
                        Console.WriteLine("1. Weight Loss");
                        Console.WriteLine("2. Underweight");
                        Console.WriteLine("3. Keep Healthy");

                        string goalChoice = Console.ReadLine();

                        if (int.TryParse(goalChoice, out int selectedGoal))
                        {
                            switch (selectedGoal)
                            {
                                case 1: 
                                    Console.WriteLine("Workout Suggestions for Weight Loss:");
                                    
                                    Console.WriteLine("1. Cardio workouts (e.g., running, cycling)");
                                    Console.WriteLine("2. Strength training (e.g., weight lifting)");
                                    break;
                                case 2:
                                    Console.WriteLine("Workout Suggestions for Underweight:");
                                    Console.WriteLine("1. Strength training (e.g., weight lifting)");
                                    Console.WriteLine("2. High-calorie diet");
                                    break;
                                case 3: 
                                    Console.WriteLine("Workout Suggestions for Maintaining Health:");
                                   
                                    Console.WriteLine("1. Balanced diet");
                                    Console.WriteLine("2. Regular exercise (e.g., 30 minutes of cardio daily)");
                                    break;
                                default:
                                    Console.WriteLine("Invalid goal choice. Please select a valid option.");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid goal choice. Please select a valid option.");
                        }
                        break;





                    case "7":
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

                    case "2":
                        Console.WriteLine("Calculating BMR...");
                        double bmr = Nutrition.CalculateBmr (currentUser.Gender, currentUser.Weight, currentUser.Height, currentUser.age);
                        Console.WriteLine($"Your Basal Metabolic Rate (BMR) is: {bmr:F2} calories");
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
    }
}

