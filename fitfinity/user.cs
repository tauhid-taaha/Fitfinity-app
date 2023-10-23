using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitfinity
{
     class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public double Height { get; set; }

        public double Weight { get; set; }
        public int age { get; set; }
        // ... Other user properties like name, age, weight, height, etc.

        public User(string username, string password , string gender, double height, double weight ,int age/*, other properties */)
        {
            Username = username;
            Password = password;
            Gender = gender;
            Height = height;
            Weight = weight;
            this.age = age;
            // Initialize other properties
        }
    }
}
