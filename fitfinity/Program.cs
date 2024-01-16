using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace fitfinity
{
    internal class Program
    {
        static void Main(string[] args)

        {

           

            UserManager userManager = new UserManager();

            string name = "███████╗██╗████████╗███████╗██╗███╗   ██╗██╗████████╗██╗   ██╗\r\n██╔════╝██║╚══██╔══╝██╔════╝██║████╗  ██║██║╚══██╔══╝╚██╗ ██╔╝\r\n█████╗  ██║   ██║   █████╗  ██║██╔██╗ ██║██║   ██║    ╚████╔╝ \r\n██╔══╝  ██║   ██║   ██╔══╝  ██║██║╚██╗██║██║   ██║     ╚██╔╝  \r\n██║     ██║   ██║   ██║     ██║██║ ╚████║██║   ██║      ██║   \r\n╚═╝     ╚═╝   ╚═╝   ╚═╝     ╚═╝╚═╝  ╚═══╝╚═╝   ╚═╝      ╚═╝   \r\n                                                              ";
           // string TITILE = "  _____ ___ _____ _____ ___ _   _ ___ _______   __\r\n|  ___|_ _|_   _|  ___|_ _| \\ | |_ _|_   _\\ \\ / /\r\n| |_   | |  | | | |_   | ||  \\| || |  | |  \\ V / \r\n|  _|  | |  | | |  _|  | || |\\  || |  | |   | |  \r\n|_|   |___| |_| |_|   |___|_| \\_|___| |_|   |_|  \r\n                                                 ";
            //string motto = "__  __                       ______ _  __                              ______                          __ \r\n\\ \\/ /____   __  __ _____   / ____/(_)/ /_ ____   ___   _____ _____   / ____/_  __ ____   ___   _____ / /_\r\n \\  // __ \\ / / / // ___/  / /_   / // __// __ \\ / _ \\ / ___// ___/  / __/  | |/_// __ \\ / _ \\ / ___// __/\r\n / // /_/ // /_/ // /     / __/  / // /_ / / / //  __/(__  )(__  )  / /___ _>  < / /_/ //  __// /   / /_  \r\n/_/ \\____/ \\__,_//_/     /_/    /_/ \\__//_/ /_/ \\___//____//____/  /_____//_/|_|/ .___/ \\___//_/    \\__/  \r\n                                                                               /_/                        ";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            AnimateText(name);

            Thread.Sleep(2000);
            string motto = "┬ ┬┌─┐┬ ┬┬─┐  ┌─┐┬┌┬┐┌┐┌┌─┐┌─┐┌─┐  ┌┬┐┬─┐┌─┐┌─┐┬┌─┌─┐┬─┐\r\n└┬┘│ ││ │├┬┘  ├┤ │ │ │││├┤ └─┐└─┐   │ ├┬┘├─┤│  ├┴┐├┤ ├┬┘\r\n ┴ └─┘└─┘┴└─  └  ┴ ┴ ┘└┘└─┘└─┘└─┘   ┴ ┴└─┴ ┴└─┘┴ ┴└─┘┴└─";
            Console.ForegroundColor = ConsoleColor.DarkRed;
          
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed ;

            Console.WriteLine(name);

            void AnimateText(string text)
            {
                int animationDelay = 50;

                for (int i = 0; i <= 15; i++) // Fade-in
                {
                    Console.Clear();
                    Console.ForegroundColor = (ConsoleColor)i;
                    Console.WriteLine(text);
                    Thread.Sleep(animationDelay);
                }

                Thread.Sleep(1000); // Pause before fade-out

                for (int i = 15; i >= 0; i--) // Fade-out
                {
                    Console.Clear();
                    Console.ForegroundColor = (ConsoleColor)i;
                    Console.WriteLine(text);
                    Thread.Sleep(animationDelay);
                }
            }
            void DisplayTitle()
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
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(motto);

            Console.WriteLine("=========================================================================================");
            Console.ResetColor();



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
                        //new add

                        if (userManager.CreateUser(newUser, newPassword))
                        {
                            Console.Clear() ;
                            DisplayTitle();
                            Console.WriteLine("Choose an option:");
                            Console.WriteLine("1. Log In");
                            Console.WriteLine("2. Exit");

                            string postSignUpChoice = Console.ReadLine();

                            switch (postSignUpChoice)
                            {
                                case "1":
                                    Console.Write("Enter your username: ");
                                    string Username = Console.ReadLine();
                                    Console.Write("Enter your password: ");
                                    string Password = Console.ReadLine();
                                    if (userManager.AuthenticateUser(Username, Password))
                                    {
                                        Console.WriteLine("Login Successful");
                                        userManager.ShowMenu();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Login Failed. Try Again.");
                                        
                                    }
                                    break;

                                case "2":
                                    Console.WriteLine("Goodbye!");
                                    return; // Exit the application

                                default:
                                    Console.WriteLine("Invalid option. Please choose a valid option.");
                                    break;
                            }
                        }
                        break;
                       case "2": Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("Enter your username: ");
                        Console.ResetColor();
                                string username = Console.ReadLine();
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.Write("Enter your password: ");
                        Console.ResetColor ();
                                string password = Console.ReadLine();
                                if (userManager.AuthenticateUser(username, password)  || userManager.AuthenticateUser(username , password))
                                { Console.Clear() ; ;
                            DisplayTitle(); Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;

                                    Console.WriteLine("Login Succesful");
                            Console.ResetColor();
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
                        }

          
        }
        
        }
    }
    

