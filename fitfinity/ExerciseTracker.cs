using System;
using System.Threading;

namespace Fitfinity
{
    internal class ExerciseTracker
    {
        public void StartExerciseTracker()
        {
            Console.WriteLine("Welcome to Fitfinity Exercise Tracker!");

            do
            {
                Console.WriteLine("\nChoose your exercise level:");
                Console.WriteLine("1. Beginner");
                Console.WriteLine("2. Intermediate");
                Console.WriteLine("3. Advanced");

                Console.Write("Enter the number corresponding to your fitness level: ");
                if (int.TryParse(Console.ReadLine(), out int fitnessLevel) && fitnessLevel >= 1 && fitnessLevel <= 3)
                {
                    Console.WriteLine($"\nGreat choice! Now, let's pick an exercise from your selected level:");

                    string[] exercises;
                    switch (fitnessLevel)
                    {
                        case 1:
                            exercises = new string[] { "Jumping Jacks", "Bodyweight Squats", "Push-ups" };
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
                        Console.WriteLine($"\nAwesome! You've selected {exercises[selectedExercise - 1]}. Let's get started with your workout.");

                        Console.Write("Enter the duration of your workout (1 to 10 minutes): ");
                        if (int.TryParse(Console.ReadLine(), out int workoutDuration) && workoutDuration >= 1 && workoutDuration <= 10)
                        {
                            Console.WriteLine($"\nGet ready to begin your workout in 3... 2... 1... Go!");

                            // Simulate a virtual timer countdown
                            for (int seconds = workoutDuration * 60; seconds > 0; seconds--)
                            {
                                Console.WriteLine($"[Time Remaining]: {TimeSpan.FromSeconds(seconds)}");
                                Thread.Sleep(1000); // Sleep for 1 second
                            }

                            Console.WriteLine("\n[Workout completed]");
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
                    Console.WriteLine("Invalid fitness level. Please enter a number between 1 and 3.");
                }

                Console.Write("\nDo you want to track another exercise? (yes/no): ");
            } while (Console.ReadLine()?.ToLower() == "yes");

            Console.WriteLine("\nThank you for using Fitfinity Exercise Tracker. Keep pushing yourself to reach your fitness goals!");
        }
    }

}