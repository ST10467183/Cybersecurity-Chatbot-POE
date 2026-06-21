using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    public class DatabaseHelper
    {
        private string connectionString = "Server=localhost;Database=cyberbot_db;Uid=root;Pwd=@55@551n;";

        public DatabaseHelper()
        {
            // Replace YOUR_PASSWORD with your MySQL root password
        }

        // Add a new task
        public void AddTask(string title, string description, DateTime? reminderDate)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO tasks (title, description, reminder_date) VALUES (@title, @desc, @reminder)";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@desc", description);
                        cmd.Parameters.AddWithValue("@reminder", reminderDate);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database error - AddTask failed: " + ex.Message);
            }
        }

        // Get all tasks
        public List<TaskItem> GetTasks()
        {
            var tasks = new List<TaskItem>();
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id, title, description, reminder_date, is_completed FROM tasks";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TaskItem task = new TaskItem();
                            task.Id = reader.GetInt32("id");
                            task.Title = reader.GetString("title");
                            task.Description = reader.GetString("description");

                            // handle null 
                            if (reader.IsDBNull(reader.GetOrdinal("reminder_date")))
                            {
                                task.ReminderDate = null;
                            }
                            else
                            {
                                task.ReminderDate = reader.GetDateTime("reminder_date");
                            }

                            task.IsCompleted = reader.GetBoolean("is_completed");
                            tasks.Add(task);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database error - GetTasks failed: " + ex.Message);
            }
            return tasks;
        }

        // Mark task as complete
        public void CompleteTask(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE tasks SET is_completed = TRUE WHERE id = @id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database error - CompleteTask failed: " + ex.Message);
            }
        }

        // Delete task
        public void DeleteTask(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM tasks WHERE id = @id";
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database error - DeleteTask failed: " + ex.Message);
            }
        }

        // Check for tasks due today (for reminder notifications)
        public List<TaskItem> GetTasksDueToday()
        {
            var tasks = new List<TaskItem>();
            try
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id, title, description, reminder_date, is_completed FROM tasks WHERE DATE(reminder_date) = CURDATE() AND is_completed = FALSE";
                    using (var cmd = new MySqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TaskItem task = new TaskItem();
                            task.Id = reader.GetInt32("id");
                            task.Title = reader.GetString("title");
                            task.Description = reader.GetString("description");
                            task.ReminderDate = reader.GetDateTime("reminder_date");
                            task.IsCompleted = reader.GetBoolean("is_completed");
                            tasks.Add(task);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database error - GetTasksDueToday failed: " + ex.Message);
            }
            return tasks;
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}