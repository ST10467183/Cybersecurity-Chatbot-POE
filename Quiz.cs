using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class Quiz
    {
        private List<Question> questions;
        private int currentIndex = 0;
        private int score = 0;
        private bool quizFinished = false;

        public Quiz()
        {
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            questions = new List<Question>
            {
                new Question
                {
                    Text = "What should you do if you receive an email asking for your password?",
                    Options = new List<string> { "Reply with your password", "Delete the email", "Report it as phishing", "Ignore it" },
                    CorrectIndex = 2,
                    Explanation = "Reporting phishing emails helps prevent scams and protects others."
                },
                new Question
                {
                    Text = "What is a strong password?",
                    Options = new List<string> { "Your birthday", "A mix of letters, numbers, and symbols", "Your pet's name", "Password123" },
                    CorrectIndex = 1,
                    Explanation = "A strong password uses a mix of uppercase, lowercase, numbers, and symbols."
                },
                new Question
                {
                    Text = "What does 'https://' indicate in a URL?",
                    Options = new List<string> { "The website is slow", "The website is secure", "The website is fake", "The website is down" },
                    CorrectIndex = 1,
                    Explanation = "The 's' in 'https' stands for secure. Your data is encrypted."
                },
                new Question
                {
                    Text = "What is phishing?",
                    Options = new List<string> { "A type of fish", "A scam to steal personal information", "A security tool", "A password manager" },
                    CorrectIndex = 1,
                    Explanation = "Phishing is when scammers try to trick you into giving away personal information."
                },
                new Question
                {
                    Text = "Why should you avoid using public WiFi for banking?",
                    Options = new List<string> { "It's too slow", "Your data could be intercepted", "It costs money", "It drains your battery" },
                    CorrectIndex = 1,
                    Explanation = "Public WiFi networks are often unsecured, making it easy for hackers to steal your data."
                },
                new Question
                {
                    Text = "What is two-factor authentication (2FA)?",
                    Options = new List<string> { "A second password", "A code sent to your phone", "A fingerprint scan", "All of the above" },
                    CorrectIndex = 3,
                    Explanation = "2FA adds an extra layer of security by requiring a second form of verification."
                },
                new Question
                {
                    Text = "How often should you update your passwords?",
                    Options = new List<string> { "Never", "Every 6-12 months", "Every day", "Only when you forget them" },
                    CorrectIndex = 1,
                    Explanation = "Regular password updates help protect against compromised accounts."
                },
                new Question
                {
                    Text = "What is a scam?",
                    Options = new List<string> { "A fun game", "A fraudulent scheme to trick you", "A type of software", "A security feature" },
                    CorrectIndex = 1,
                    Explanation = "Scammers try to trick you into giving money or personal information."
                },
                new Question
                {
                    Text = "What should you do if you suspect a scam call?",
                    Options = new List<string> { "Give them your details", "Hang up and call the official number", "Keep talking to them", "Ignore it and do nothing" },
                    CorrectIndex = 1,
                    Explanation = "Always verify by calling the official number directly. Don't trust caller ID."
                },
                new Question
                {
                    Text = "Why is it important to review your privacy settings?",
                    Options = new List<string> { "To make your profile look better", "To control who can see your information", "To get more followers", "It's not important" },
                    CorrectIndex = 1,
                    Explanation = "Privacy settings protect your personal information from being misused."
                },
                new Question
                {
                    Text = "What is malware?",
                    Options = new List<string> { "A type of hardware", "Malicious software", "A password manager", "A web browser" },
                    CorrectIndex = 1,
                    Explanation = "Malware is harmful software designed to damage or access your system without permission."
                },
                new Question
                {
                    Text = "How can you spot a fake website?",
                    Options = new List<string> { "It has a padlock icon", "It has spelling mistakes", "It uses 'https'", "All of the above" },
                    CorrectIndex = 3,
                    Explanation = "Fake websites often have spelling errors, no padlock, and look unprofessional."
                }
            };
        }

        public Question GetCurrentQuestion()
        {
            if (currentIndex < questions.Count && !quizFinished)
                return questions[currentIndex];
            return null;
        }

        public bool AnswerQuestion(int selectedIndex)
        {
            if (quizFinished || currentIndex >= questions.Count)
                return false;

            var question = questions[currentIndex];
            bool isCorrect = selectedIndex == question.CorrectIndex;

            if (isCorrect)
                score++;

            currentIndex++;

            if (currentIndex >= questions.Count)
                quizFinished = true;

            return isCorrect;
        }

        public int GetScore()
        {
            return score;
        }

        public int GetTotalQuestions()
        {
            return questions.Count;
        }

        public bool IsQuizFinished()
        {
            return quizFinished;
        }

        public string GetFinalMessage()
        {
            int total = questions.Count;
            int percentage = (score * 100) / total;

            if (percentage >= 80)
                return $"Great job! You scored {score}/{total} ({percentage}%). You're a cybersecurity expert!";
            else if (percentage >= 60)
                return $"Good effort! You scored {score}/{total} ({percentage}%). Keep learning to stay safe!";
            else
                return $"You scored {score}/{total} ({percentage}%). Review the tips above and try again!";
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public List<string> Options { get; set; }
        public int CorrectIndex { get; set; }
        public string Explanation { get; set; }
    }
}