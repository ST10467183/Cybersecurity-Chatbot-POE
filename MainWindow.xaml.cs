using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CybersecurityChatbot
{
    public partial class MainWindow : Window
    {
        private Chatbot bot;
        private DatabaseHelper dbHelper = new DatabaseHelper();
        private Quiz quiz;

        public MainWindow()
        {
            InitializeComponent();
            PlayVoiceGreeting();
            bot = new Chatbot();
            AddToChat("BOT: Welcome to the Cybersecurity Awareness Bot!", "#00ff9d");
            AddToChat("BOT: What's your name?", "#00ff9d");
            CheckReminders();
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("Audio/greeting.wav");
                player.Play();
            }
            catch (Exception)
            {
                AddToChat("BOT: Voice greeting file not found. Continuing with text only.", "#ff6b6b");
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendUserMessage();
        }

        private void UserInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendUserMessage();
            }
        }

        private void SendUserMessage()
        {
            string userMessage = UserInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(userMessage))
                return;

            AddToChat($"YOU: {userMessage}", "#ffffff");
            UserInput.Text = "";

            string botResponse = ProcessUserInput(userMessage);
            AddToChat($"BOT: {botResponse}", "#00ff9d");
        }

        private string ProcessUserInput(string userInput)
        {
            if (string.IsNullOrEmpty(bot.GetUserName()))
            {
                bot.SetUserName(userInput);
                return $"Nice to meet you, {bot.GetUserName()}! I'm here to help you stay safe online.\n\nYou can ask me about:\n- Passwords\n- Phishing\n- Safe browsing\n- Scams\n- Privacy\n- Two-factor authentication (2FA)";
            }

            return bot.GetResponse(userInput, "");
        }

        private void AddToChat(string message, string colorHex)
        {
            ChatDisplay.Items.Add(message);
            ChatDisplay.ScrollIntoView(ChatDisplay.Items[ChatDisplay.Items.Count - 1]);
        }

        private void CheckReminders()
        {
            var dueTasks = dbHelper.GetTasksDueToday();
            foreach (var task in dueTasks)
            {
                AddToChat($"REMINDER: Task '{task.Title}' is due today! - {task.Description}", "#ff6b6b");
            }
        }

        // ===== QUIZ =====

        private void StartQuizButton_Click(object sender, RoutedEventArgs e)
        {
            quiz = new Quiz();
            ShowQuestion();
            StartQuizButton.Content = "Restart Quiz";
        }

        private void ShowQuestion()
        {
            var question = quiz.GetCurrentQuestion();
            if (question == null)
            {
                ShowQuizResult();
                return;
            }

            QuizQuestionDisplay.Text = question.Text;
            QuizProgressDisplay.Text = $"{quiz.GetScore() + 1} / {quiz.GetTotalQuestions()}";

            QuizOption1.Content = $"A) {question.Options[0]}";
            QuizOption2.Content = $"B) {question.Options[1]}";
            QuizOption3.Content = $"C) {question.Options[2]}";
            QuizOption4.Content = $"D) {question.Options[3]}";

            QuizFeedbackDisplay.Text = "Select an answer to see feedback";
            QuizFeedbackDisplay.Foreground = System.Windows.Media.Brushes.LightGray;

            QuizOption1.IsEnabled = true;
            QuizOption2.IsEnabled = true;
            QuizOption3.IsEnabled = true;
            QuizOption4.IsEnabled = true;
        }

        private void QuizOption_Click(object sender, RoutedEventArgs e)
        {
            if (quiz == null)
            {
                QuizFeedbackDisplay.Text = "Please start the quiz first!";
                return;
            }

            var currentQuestion = quiz.GetCurrentQuestion();
            if (currentQuestion == null)
            {
                ShowQuizResult();
                return;
            }

            var button = sender as Button;
            int selectedIndex = int.Parse(button.Tag.ToString());

            // Store data BEFORE answering
            string questionText = currentQuestion.Text;
            string correctAnswer = currentQuestion.Options[currentQuestion.CorrectIndex];
            string explanation = currentQuestion.Explanation;

            bool isCorrect = quiz.AnswerQuestion(selectedIndex);

            if (isCorrect)
            {
                QuizFeedbackDisplay.Text = $"Correct! {explanation}";
                QuizFeedbackDisplay.Foreground = System.Windows.Media.Brushes.LightGreen;
            }
            else
            {
                QuizFeedbackDisplay.Text = $"Incorrect. The correct answer was: {correctAnswer}\n{explanation}";
                QuizFeedbackDisplay.Foreground = System.Windows.Media.Brushes.LightCoral;
            }

            QuizOption1.IsEnabled = false;
            QuizOption2.IsEnabled = false;
            QuizOption3.IsEnabled = false;
            QuizOption4.IsEnabled = false;

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                ShowQuestion();
            };
            timer.Start();
        }

        private void ShowQuizResult()
        {
            string result = quiz.GetFinalMessage();
            QuizQuestionDisplay.Text = "Quiz Complete!";
            QuizProgressDisplay.Text = $"Score: {quiz.GetScore()} / {quiz.GetTotalQuestions()}";
            QuizFeedbackDisplay.Text = result;
            QuizFeedbackDisplay.Foreground = System.Windows.Media.Brushes.Gold;

            QuizOption1.Content = "";
            QuizOption2.Content = "";
            QuizOption3.Content = "";
            QuizOption4.Content = "";
            QuizOption1.IsEnabled = false;
            QuizOption2.IsEnabled = false;
            QuizOption3.IsEnabled = false;
            QuizOption4.IsEnabled = false;
        }

        // ===== TASKS =====

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TaskTitle.Text.Trim();
            string description = TaskDescription.Text.Trim();
            DateTime? reminderDate = TaskReminderDate.SelectedDate;

            if (string.IsNullOrEmpty(title))
            {
                AddToChat("Please enter a task title.", "#ff6b6b");
                return;
            }

            dbHelper.AddTask(title, description, reminderDate);
            AddToChat($"Task added: {title}", "#00ff9d");
            RefreshTaskList();
            TaskTitle.Text = "";
            TaskDescription.Text = "";
            TaskReminderDate.SelectedDate = null;
        }

        private void RefreshTaskList()
        {
            var tasks = dbHelper.GetTasks();
            TaskListBox.ItemsSource = tasks;
        }

        private void RefreshTasksButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshTaskList();
        }

        private void CompleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int id = (int)button.Tag;
            dbHelper.CompleteTask(id);
            AddToChat($"Task marked as complete.", "#00ff9d");
            RefreshTaskList();
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int id = (int)button.Tag;
            dbHelper.DeleteTask(id);
            AddToChat($"Task deleted.", "#ff6b6b");
            RefreshTaskList();
        }
    }
}