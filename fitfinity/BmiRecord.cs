using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fitfinity
{
    internal class BmiRecord
    {
        public string UserName { get; set; }
        public double Bmi { get; set; }
        public DateTime RecordDate { get; set; }
        public BmiRecord(string userName, DateTime recordDate, double bmi)
        {
            UserName = userName;
            RecordDate = recordDate;
            Bmi = bmi;
        }

        public BmiRecord(string userName,double bmi)
        {
            UserName = userName;
            Bmi = bmi;
            RecordDate = DateTime.Now; // Automatically set the current date and time
        }

        public override string ToString()
        {
            return $"User Name: {UserName}, BMI: {Bmi}, Date: {RecordDate.ToString("yyyy-MM-dd HH:mm:ss")}";
        }
    }
}
