using System;
using System.Collections.Generic;

namespace Fitfinity
{
    internal class MedicalFAQ
    {
        private Dictionary<string, string> faq;

        public MedicalFAQ()
        {
            InitializeFAQ();
        }

        private void InitializeFAQ()
        {
            faq = new Dictionary<string, string>
            {
                {"1", "What is the recommended daily water intake? Answer: The recommended daily water intake varies, but a common guideline is to drink at least eight 8-ounce glasses, which is about 2 liters, or half a gallon."},
                {"2", "How many hours of sleep should I get each night? Answer: Adults generally need 7-9 hours of sleep per night for optimal health and well-being."},
                {"3", "What is a healthy resting heart rate? Answer: A healthy resting heart rate for adults is typically between 60-100 beats per minute, but lower rates are generally associated with better cardiovascular fitness."},
                {"4", "How can I improve my posture? Answer: To improve posture, focus on exercises that strengthen core muscles, practice mindful sitting and standing, and consider ergonomic adjustments to your workspace."},
                {"5", "What are some ways to reduce stress? Answer: Stress reduction techniques include deep breathing exercises, meditation, regular exercise, and engaging in activities you enjoy."},
                {"6", "What is a balanced diet? Answer: A balanced diet includes a variety of fruits, vegetables, whole grains, lean proteins, and healthy fats."},
                {"7", "How often should I exercise? Answer: Aim for at least 150 minutes of moderate-intensity aerobic exercise or 75 minutes of vigorous-intensity exercise per week, along with muscle-strengthening activities on 2 or more days per week."},
                {"8", "What are the benefits of regular physical activity? Answer: Regular physical activity has numerous benefits, including improved cardiovascular health, weight management, better mood, and reduced risk of chronic diseases."},
                {"9", "How can I manage my weight effectively? Answer: Weight management involves a combination of a healthy diet, regular physical activity, and lifestyle modifications. Consult with a healthcare professional for personalized advice."},
                {"10", "What is the importance of stretching before exercise? Answer: Stretching before exercise helps improve flexibility, enhances joint range of motion, and may reduce the risk of injury. It is best done after a warm-up."},
                {"11", "How can I stay motivated to exercise regularly? Answer: Set realistic goals, find activities you enjoy, vary your routine, and consider exercising with a friend or joining a group class to stay motivated."},
                {"12", "What are the signs of dehydration? Answer: Signs of dehydration include dark yellow urine, dry mouth, dizziness, and fatigue. It's essential to drink an adequate amount of water throughout the day."},
                {"13", "Is it safe to exercise during pregnancy? Answer: In many cases, exercise during pregnancy is safe and beneficial. However, it's crucial to consult with a healthcare provider to determine the most suitable activities."},
                {"14", "How can I prevent common injuries during exercise? Answer: Warm up before exercise, use proper form, wear appropriate gear, gradually increase intensity, and listen to your body to prevent injuries."},
                {"15", "What are the benefits of strength training? Answer: Strength training helps build and maintain muscle mass, increases metabolism, improves bone density, and enhances overall functional fitness."},
                {"16", "What are the recommended cholesterol levels for heart health? Answer: A healthy total cholesterol level is generally below 200 mg/dL, with LDL (bad) cholesterol below 100 mg/dL and HDL (good) cholesterol above 40 mg/dL for men and 50 mg/dL for women."},
                {"17", "How can I improve my cardiovascular health? Answer: Improving cardiovascular health involves aerobic exercise, a heart-healthy diet, maintaining a healthy weight, not smoking, and managing stress."},
                {"18", "What is the role of antioxidants in the diet? Answer: Antioxidants help protect cells from damage caused by free radicals. They are found in various fruits, vegetables, and other plant-based foods."},
                {"19", "What are the benefits of good sleep hygiene? Answer: Good sleep hygiene practices include maintaining a consistent sleep schedule, creating a comfortable sleep environment, and avoiding stimulants before bedtime. These practices promote better sleep quality."},
                {"20", "How can I reduce my risk of chronic diseases? Answer: Adopting a healthy lifestyle, including regular exercise, a balanced diet, not smoking, limiting alcohol intake, and managing stress, can significantly reduce the risk of chronic diseases."}
            };
        }

        public void DisplayFAQ()
        {
            Console.WriteLine("Medical FAQ:");

            foreach (var entry in faq)
            {
                Console.WriteLine($"{entry.Key}. {entry.Value.Split('?')[0]}?");
            }
        }

        public void AnswerQuestion(string questionNumber)
        {
            if (faq.TryGetValue(questionNumber, out string answer))
            {
                string[] questionParts = faq[questionNumber].Split('?');
                Console.WriteLine($"\nQuestion {questionNumber}: {questionParts[0]}?");
                Console.WriteLine($"{questionParts[1]}");
            }
            else
            {
                Console.WriteLine("\nInvalid question number. Please enter a valid question number.");
            }
        }

    }
}
