# Cybersecurity Awareness Chatbot

A cybersecurity chatbot for PROG6221 POE - Part 1 (Console), Part 2 (GUI), and Part 3 (Full POE).

## Parts Completed
- ✅ Part 1: Console Application
- ✅ Part 2: WPF GUI Application
- ✅ Part 3: Full POE with Database, Quiz, NLP, and Activity Log

## Part 1 Features (Console)
- Voice greeting on startup
- ASCII art logo with colored console UI
- Personalized responses using user's name
- Cybersecurity tips for:
  - Password safety
  - Phishing awareness
  - Safe browsing
  - Scam prevention
  - Privacy protection
  - Two-factor authentication (2FA)

## Part 2 Features (GUI)
- WPF graphical interface with dark purple theme
- ASCII art header spelling "CYBERBOT"
- Keyword recognition for 6 cybersecurity topics
- Random responses for passwords, phishing, scams, privacy, and safe browsing
- Conversation flow with "tell me more" and "another tip" commands
- Memory recall - remembers user name and interests
- Sentiment detection for worried, curious, and frustrated users
- "Did you mean" suggestions for partial matches
- Voice greeting on startup

## Part 3 Features (Full POE)
- Task Assistant with MySQL database integration
  - Add, view, complete, and delete tasks
  - Reminder dates and notifications
- Cybersecurity Mini-Game (Quiz) with 12 questions
  - Multiple-choice format
  - Immediate feedback with explanations
  - Score tracking and final result
- NLP Simulation
  - Recognises different phrasing for tasks and quiz
  - Limited "I don't understand" responses
- Activity Log
  - Logs all actions (tasks, quiz, etc.)
  - Show last 10 actions with timestamps
  - "Show more" option for full history

## How to Run

### Part 1 (Console)
1. Open `CybersecurityChatbot.slnx` in Visual Studio 2026
2. In Solution Explorer, right-click the `CybersecurityChatbot` project and select **Set as Startup Project**
3. Press `F5` to run

### Part 2 (GUI)
1. Open `CybersecurityChatbot.slnx` in Visual Studio 2026
2. Build the project (Ctrl + Shift + B)
3. Press F5 to run
4. The GUI window will open automatically
5. Type your name to start the conversation

### Part 3 (Full POE)
1. Ensure MySQL is running on your machine
2. Open `CybersecurityChatbot.slnx` in Visual Studio 2026
3. Build the project (Ctrl + Shift + B)
4. Press F5 to run
5. Use the Chat, Tasks, and Quiz tabs

## Example Commands
| You Type | Bot Response |
|----------|--------------|
| `Tell me about passwords` | Random password safety tip |
| `tell me more` | Another tip on the same topic |
| `What is phishing?` | Random phishing awareness tip |
| `another tip` | Different tip on current topic |
| `add task` | NLP response directing to Tasks tab |
| `start quiz` | NLP response directing to Quiz tab |
| `show log` | Displays recent activity log |
| `show more` | Displays full activity history |

## Technologies Used
- C# .NET Framework 4.7.2
- WPF (Windows Presentation Foundation)
- MySQL Database
- Visual Studio 2026
- GitHub Actions CI/CD

## Author
ST10467183

## Course
PROG6221 - Programming 2A

## References

Pieterse, H. 2021. The Cyber Threat Landscape in South Africa: A 10-Year Review. The African Journal of Information and Communication, 28(28). doi: https://doi.org/10.23962/10539/32213 (Accessed 12 May 2026).

Microsoft. 2026. WPF Documentation. Available at: https://docs.microsoft.com/en-us/dotnet/desktop/wpf/ (Accessed 12 May 2026).

GitHub. 2026. GitHub Actions Documentation. Available at: https://docs.github.com/en/actions (Accessed 12 May 2026).

MySQL. 2026. MySQL Documentation. Available at: https://dev.mysql.com/doc/ (Accessed 12 May 2026).