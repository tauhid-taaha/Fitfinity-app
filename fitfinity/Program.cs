using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitfinity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserManager userManager = new UserManager();
            Console.WriteLine("Welcome to Calorie Intake Calculator!");
            Console.WriteLine("=====================================");

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Sign Up");
                Console.WriteLine("2. Log In");
                Console.WriteLine("3. Exit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter a username: ");
                        string newUser = Console.ReadLine();
                        Console.Write("Enter a password: ");
                        string newPassword = Console.ReadLine();
                        

                        userManager.CreateUser(newUser, newPassword);
                        break;
                    case "2":
                        Console.Write("Enter your username: ");
                        string username = Console.ReadLine();
                        Console.Write("Enter your password: ");
                        string password = Console.ReadLine();
                          if (userManager.AuthenticateUser(username, password))
                    {
                            Console.WriteLine("Login Succesful");
                        userManager.ShowMenu(); // Show menu options after successful login
                    }
                        break;
                    case "3":
                        Console.WriteLine("Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please choose a valid option.");
                        break;
                }

                Console.WriteLine();
            }
        }
    }
    }

