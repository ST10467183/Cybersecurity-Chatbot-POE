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

        // ===== TASK ASSISTANT METHODS =====

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