using System;
using System.Collections.Generic;

namespace Fitfinity
{
    internal class MedicalFAQ
    {
        private Dictionary<string, (string Question, string Answer, string Advice)> faqData;

        public MedicalFAQ()
        {
            InitializeFAQ();
        }

        private void InitializeFAQ()
        {
            faqData = new Dictionary<string, (string, string, string)>
            {
                { "Q1", ("How many hours of sleep do adults need?", "Most adults need 7-9 hours of sleep per night for optimal health.", "Prioritize a consistent sleep schedule for overall well-being.") },
                { "Q2", ("What is a healthy daily water intake?", "The recommended daily water intake is around 8 cups (64 ounces) for adults. However, individual needs may vary.", "Stay hydrated by drinking water throughout the day.") },
                { "Q3", ("How often should I exercise?", "Adults should aim for at least 150 minutes of moderate-intensity aerobic activity per week, plus muscle-strengthening activities on two or more days a week.", "Find activities you enjoy to make exercise a regular part of your routine.") },
                { "Q4", ("Is it normal to feel stressed?", "Yes, stress is a normal part of life. However, chronic stress can negatively impact health.", "Practice stress management techniques such as deep breathing, meditation, or physical activity.") },
                { "Q5", ("What is a balanced diet?", "A balanced diet includes a variety of fruits, vegetables, whole grains, lean proteins, and healthy fats.", "Focus on nutrient-dense foods and maintain a well-rounded diet.") },
                { "Q6", ("How can I improve my posture?", "To improve posture, practice exercises that strengthen core muscles, and be mindful of body alignment.", "Sit and stand tall, and incorporate posture exercises into your routine.") },
                { "Q7", ("Are there benefits to taking breaks during work?", "Yes, taking breaks during work can improve focus and productivity. Short breaks and stretching can reduce mental fatigue.", "Incorporate short breaks and stretches into your work routine.") },
                { "Q8", ("What are the risks of prolonged sitting?", "Prolonged sitting is associated with health risks such as obesity and cardiovascular disease. It's important to break up long periods of sitting.", "Stand, stretch, and move regularly, especially if you have a desk job.") },
                { "Q9", ("How can I maintain a healthy weight?", "Maintaining a healthy weight involves a balance of a nutritious diet and regular physical activity.", "Focus on portion control, choose nutrient-dense foods, and stay active.") },
                { "Q10", ("Can laughter improve health?", "Yes, laughter has several health benefits, including reducing stress hormones and increasing endorphins.", "Incorporate laughter into your life through humor, socializing, or watching funny videos.") },
                { "Q11", ("How can I boost my immune system?", "A healthy lifestyle, including regular exercise, a balanced diet, adequate sleep, and stress management, can support a strong immune system.", "Prioritize overall health and wellness to support immune function.") },
                { "Q12", ("What are the benefits of regular physical activity?", "Regular physical activity has numerous benefits, including improved cardiovascular health, mental well-being, and increased strength and flexibility.", "Find activities you enjoy to make exercise a regular part of your routine.") },
                { "Q13", ("Can certain foods improve brain health?", "Foods rich in omega-3 fatty acids, antioxidants, and vitamins are beneficial for brain health. Examples include fatty fish, berries, and leafy greens.", "Incorporate brain-boosting foods into your diet for cognitive health.") },
                { "Q14", ("How can I manage stress and anxiety?", "Stress and anxiety can be managed through techniques such as deep breathing, meditation, exercise, and seeking support from friends or professionals.", "Develop coping mechanisms and a support network to manage stress and anxiety.") },
                { "Q15", ("Is it important to stay socially connected?", "Yes, social connections contribute to mental and emotional well-being. Maintaining relationships can reduce feelings of loneliness and improve overall health.", "Prioritize social interactions and stay connected with friends and family.") },
                { "Q16", ("Can lack of sleep affect mental health?", "Yes, insufficient sleep can negatively impact mental health, contributing to mood swings, irritability, and difficulty concentrating.", "Establish healthy sleep habits for better mental well-being.") },
                { "Q17", ("How can I reduce screen time?", "Reducing screen time is important for eye health and overall well-being. Set limits, take breaks, and engage in non-screen activities.", "Create a screen time routine that includes breaks and alternative activities.") },
                { "Q18", ("What is the impact of smoking on health?", "Smoking is a major risk factor for various health conditions, including lung cancer, heart disease, and respiratory issues.", "Quit smoking for improved overall health and well-being.") },
                { "Q19", ("How much sunlight do I need for vitamin D?", "Exposure to sunlight for about 10-30 minutes a few times a week is typically sufficient for vitamin D synthesis. However, individual needs vary.", "Enjoy outdoor activities to ensure adequate sunlight exposure.") },
                { "Q20", ("Can hobbies improve mental health?", "Engaging in hobbies and activities you enjoy can have positive effects on mental health, providing a sense of purpose and relaxation.", "Make time for hobbies that bring you joy and relaxation.") }
            };
        }

        public void DisplayFAQ()
        {
            Console.WriteLine("\nMedical FAQ:");

            foreach (var entry in faqData)
            {
                Console.WriteLine($"\n[{entry.Key}] {entry.Value.Question}");
                Console.WriteLine($"   {entry.Value.Answer}");
                Console.WriteLine($"   Advice: {entry.Value.Advice}");
            }
        }
    }
}