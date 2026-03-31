using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Threading;

namespace CybersecurityChatbot
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Title
            Console.Title = "Cybersecurity Awareness Bot";

            // Audio
            PlayVoiceGreeting();

            // Art
            UI.DisplayHeader();

            // instance
            Chatbot bot = new Chatbot();

            // Name?
            Console.Write("\n[Bot]: Hello! What's your name? ");
            string name = Console.ReadLine();

            // Be correct
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("[Bot]: I didn't catch that. What's your name? ");
                name = Console.ReadLine();
            }

            // Steal data(store name)
            bot.SetUserName(name);

            // Welcome
            UI.DisplayResponse($"Welcome, {bot.GetUserName()}! I'm your Cybersecurity Awareness Assistant. I'm here to help you stay safe online!");
            UI.DisplayResponse("You can ask me about password safety, phishing scams, or safe browsing. Type 'exit' to quit.");

            // Main Loop
            while (true)
            {
                // User input
                Console.Write("\n[You]: ");
                string userInput = Console.ReadLine();

                // Check exit
                if (userInput != null && userInput.ToLower() == "exit")
                {
                    UI.DisplayResponse($"Goodbye, {bot.GetUserName()}! Stay safe online! 🛡️");
                    break;
                }

                //get response/display
                string response = bot.GetResponse(userInput);
                UI.DisplayResponse(response);
            }

            // Pause to read
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // Pay recording
        static void PlayVoiceGreeting()
        {
            try
            {
                // sound player
                SoundPlayer player = new SoundPlayer("Audio/greeting.wav");
                player.Play(); // Plays asynchronously (doesn't block)
            }
            catch (Exception)
            {
                // problem with file
                Console.WriteLine("Note: Voice greeting not available. Continuing with text only.");
            }
        }
    }
}
