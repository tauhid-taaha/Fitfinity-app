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
        // ... Other user properties like name, age, weight, height, etc.

        public User(string username, string password , string gender, double height, double weight/*, other properties */)
        {
            Username = username;
            Password = password;
            Gender = gender;
            Height = height;
            Weight = weight;
            // Initialize other properties
        }
    }
}
