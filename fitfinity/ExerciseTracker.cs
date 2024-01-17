using fitfinity;
using System;
using System.Threading;

namespace Fitfinity
{
    internal class ExerciseTracker
    {
        public double Weight { get;  set; }

        public ExerciseTracker(double weight)
        {
            Weight = weight;
        }
        
        public void StartExerciseTracker()
        {
            
            Console.WriteLine("Welcome to Fitfinity Exercise Tracker!");

            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nChoose your exercise level:");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("1. Beginner");
                Console.WriteLine("2. Intermediate");
                Console.WriteLine("3. Advanced");
                Console.WriteLine("4. Push-ups");
                Console.ResetColor();
                Console.Write("Enter the number corresponding to your fitness level: ");
                if (int.TryParse(Console.ReadLine(), out int fitnessLevel) && fitnessLevel >= 1 && fitnessLevel <= 4)
                {
                    if (fitnessLevel == 4)
                    {
                        TrackPushUps();
                        continue;
                    }

                    Console.WriteLine($"\nGreat choice! Now, let's pick an exercise from your selected level:");

                    string[] exercises;
                    Console.ForegroundColor= ConsoleColor.Cyan;
                    switch (fitnessLevel)
                    { 
                        case 1:
                            exercises = new string[] { "Jumping Jacks", "Bodyweight Squats", "Lunges" };
                            break;
                        case 2:
                            exercises = new string[] { "Plank with Shoulder Taps", "Kettlebell Swings", "Jump Lunges" };
                            break;
                        case 3:
                            exercises = new string[] { "Burpees", "Mountain Climbers", "Box Jumps" };
                            break;
                        default:
                            exercises = new string[] { };
                            break;
                    }

                    Console.WriteLine("Exercises for Level " + fitnessLevel + ":");
                    for (int i = 0; i < exercises.Length; i++)
                    {
                        Console.WriteLine($"{i + 1}. {exercises[i]}");
                    }

                    Console.Write("Enter the number of the exercise you want to track: ");
                    if (int.TryParse(Console.ReadLine(), out int selectedExercise) && selectedExercise >= 1 && selectedExercise <= exercises.Length)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nAwesome! You've selected {exercises[selectedExercise - 1]}. Let's get started with your workout.");
                        Console.ForegroundColor=ConsoleColor.Cyan;
                        Console.Write("Enter the duration of your workout (1 to 10 minutes): ");
                        if (int.TryParse(Console.ReadLine(), out int workoutDuration) && workoutDuration >= 1 && workoutDuration <= 10)
                        {
                            Console.Write($"Enter the number of {exercises[selectedExercise - 1]} repetitions during the entire workout: ");
                            if (int.TryParse(Console.ReadLine(), out int exerciseRepetitions) && exerciseRepetitions >= 0)
                            {
                                Console.WriteLine($"\nGet ready to begin your workout in 3... 2... 1... Go!");

                                // Simulate a virtual timer countdown
                                for (int seconds = workoutDuration * 60; seconds > 0; seconds--)
                                {   Console.ForegroundColor= ConsoleColor.Red;

                                    Console.WriteLine($"[Time Remaining]: {TimeSpan.FromSeconds(seconds)}");
                                    if (Console.KeyAvailable)
                                    {
                                        // Clear the key buffer
                                        Console.ReadKey(true);
                                        break; // Exit the loop if a key is pressed
                                    }
                                    Thread.Sleep(1000); // Sleep for 1 second
                                }

                                // Check if the selected exercise is "Push-ups"
                                if (exercises[selectedExercise - 1].Equals("Push-ups", StringComparison.OrdinalIgnoreCase))
                                {
                                    TrackPushUps();
                                }
                                else
                                {
                                    // Calculate calories burned based on MET (Metabolic Equivalent of Task) values
                                    double metValue = CalculateMetValue(exercises[selectedExercise - 1]);
                                    double caloriesBurned = CalculateCaloriesBurned(exerciseRepetitions, workoutDuration, metValue, Weight);
                                    Console.ForegroundColor=(ConsoleColor.Cyan);
                                    Console.WriteLine($"\n[Workout completed]");
                                    Console.WriteLine($"Calories burned for {exerciseRepetitions} repetition {exercises[selectedExercise - 1]}: {caloriesBurned:F2} calories");
                                    Console.ResetColor();
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid number of repetitions. Please enter a non-negative integer.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid workout duration. Please enter a value between 1 and 10.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid exercise choice. Please enter a valid exercise number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid fitness level. Please enter a number between 1 and 4.");
                }

                Console.Write("\nDo you want to track another exercise? (yes/no): ");
            } while (Console.ReadLine()?.ToLower() == "yes");

            Console.WriteLine("\nThank you for using Fitfinity Exercise Tracker. Keep pushing yourself to reach your fitness goals!");
        }

        public void TrackPushUps()
        {
            Console.WriteLine("Select the intensity of push-ups:");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("1. Moderate Effort (MET: 3.8)");
            Console.WriteLine("2. Vigorous Effort (MET: 8.0)");
            Console.ForegroundColor= ConsoleColor.Cyan;
            Console.Write("Enter the number corresponding to your selected intensity: ");
            Console.ResetColor();
            if (int.TryParse(Console.ReadLine(), out int intensityChoice) && (intensityChoice == 1 || intensityChoice == 2))
            {
                double metValue = (intensityChoice == 1) ? 3.8 : 8.0;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Enter the number of push-ups you want to track: ");
                Console.ResetColor();
                if (int.TryParse(Console.ReadLine(), out int pushUpRepetitions) && pushUpRepetitions >= 0)
                {              Console.ForegroundColor=ConsoleColor.Red;
                    Console.WriteLine("\n[Workout completed]");
                    double caloriesBurned = (metValue * Weight * pushUpRepetitions) / 200;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;

                    Console.WriteLine($"Calories burned for {pushUpRepetitions} push-ups: {caloriesBurned:F2} calories");
                    Console.ResetColor( );
                }
                else
                {
                    Console.WriteLine("Invalid number of push-ups. Please enter a non-negative integer.");
                }
            }
            else
            {
                Console.WriteLine("Invalid intensity choice. Please enter 1 or 2.");
            }
        }

        public double CalculateMetValue(string exercise)
        {
            // MET values for different exercises (values are approximate)
            switch (exercise.ToLower())
            {
                case "jumping jacks":
                    return 8.0;
                case "bodyweight squats":
                    return 5.0;
                case "lunges":
                    return 5.5;
                case "plank with shoulder taps":
                    return 4.0;
                case "kettlebell swings":
                    return 12.0;
                case "jump lunges":
                    return 11.0;
                case "burpees":
                    return 14.0;
                case "mountain climbers":
                    return 13.0;
                case "box jumps":
                    return 8.0;
                default:
                    return 1.0; // Default MET value
            }
        }

         double CalculateCaloriesBurned(int repetitions, int durationMinutes, double metValue,double Weight)
        {
            // Total calories burned formula: Calories = (MET * weight in kg * 3.5) / 200 * duration in minutes
            // For simplicity, assuming a constant weight of 70 kg
            
            return (metValue * Weight * 3.5) / 200 * durationMinutes;
        }
    }

    
}
