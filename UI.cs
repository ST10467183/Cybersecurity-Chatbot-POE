using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CybersecurityChatbot
{
    internal class UI
    {
        // Art
        public static void DisplayHeader()
        {
            // Colour
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
╔══════════════════════════════════════════════════════════════════════════════╗
║                                                                              ║
║    _____         _   _                 _        _        _                   ║
║   / ____|       | | | |               | |      | |      | |                  ║
║  | |     ___  __| | | |__   ___  _ __ | |_ __ _| |_ __ _| |_                 ║
║  | |    / _ \/ _` | | '_ \ / _ \| '_ \| __/ _` | __/ _` | __|                ║
║  | |___|  __/ (_| | | | | | (_) | | | | || (_| | || (_| | |_                 ║
║   \_____\___|\__,_| |_| |_|\___/|_| |_|\__\__,_|\__\__,_|\__|                ║
║                                                                              ║
╚══════════════════════════════════════════════════════════════════════════════╝
");
            Console.ResetColor();

            // Tag Line
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
    ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
    ░                                                                        ░
    ░              C Y B E R S E C U R I T Y   A W A R E N E S S            ░
    ░                        Your first line of defense online               ░
    ░                                                                        ░
    ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
");
            Console.ResetColor();

           
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('═', 70));
            Console.ResetColor();
        }

        // Response
        public static void DisplayResponse(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\n[🤖 BOT]: ");
            Console.ResetColor();
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('─', 70));
            Console.ResetColor();
        }

        // U message
        public static void DisplayUserMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n[👤 YOU]: ");
            Console.ResetColor();
            Console.WriteLine(message);
        }

       
        public static void TypewriterEffect(string message, int delayMilliseconds = 30)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delayMilliseconds);
            }
            Console.WriteLine();
        }


        public static void ShowLoadingAnimation(string text, int durationSeconds = 2)
        {
            Console.Write(text);
            for (int i = 0; i < durationSeconds * 4; i++)
            {
                Thread.Sleep(250);
                Console.Write(".");
            }
            Console.WriteLine();
        }
        public static void ShowSeparator()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(new string('─', 70));
            Console.ResetColor();
        }
    }
}
