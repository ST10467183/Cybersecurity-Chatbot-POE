using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbot
{
    internal class Chatbot
    {
        private string userName;
        private string userInterest = "";
        private bool waitingForYesNo = false;
        private string pendingTopic = "";
        private Random random = new Random();

        // Flow tracking
        private string lastTopic = "";
        private int lastTipIndex = -1;

        // Sentiment
        private List<string> worriedWords = new List<string> { "worried", "scared", "nervous", "anxious", "concerned", "afraid" };
        private List<string> curiousWords = new List<string> { "curious", "interesting", "fascinating", "tell me more", "want to learn" };
        private List<string> frustratedWords = new List<string> { "frustrated", "annoying", "confusing", "difficult", "hard", "stupid" };

        // Password tips
        private List<string> passwordTips = new List<string>
        {
            "Use at least 12 characters with a mix of uppercase, lowercase, numbers, and symbols.",
            "Never reuse passwords across different accounts.",
            "Use a password manager to generate and store strong, unique passwords.",
            "Avoid using personal information like birthdays, names, or pet names.",
            "Enable two-factor authentication whenever possible.",
            "Change your passwords immediately if you suspect any account has been compromised."
        };

        // Phishing tips
        private List<string> phishingTips = new List<string>
        {
            "Never click on links in suspicious emails.",
            "Legitimate companies never ask for your password via email.",
            "Check the sender's email address carefully.",
            "Look for spelling mistakes and urgent language like 'Act now!'.",
            "Type the website URL yourself instead of clicking email links.",
            "Never download attachments from unknown senders."
        };

        // Scam tips
        private List<string> scamTips = new List<string>
        {
            "If something sounds too good to be true, it probably is.",
            "Never send money to someone you've only met online.",
            "Legitimate companies won't call you out of the blue for tech support.",
            "Be wary of calls claiming you owe money to the government.",
            "Don't click pop-up ads claiming your computer is infected.",
            "Always verify charity requests before donating."
        };

        // Privacy tips
        private List<string> privacyTips = new List<string>
        {
            "Review app permissions regularly.",
            "Use a VPN on public WiFi networks.",
            "Be careful what you share on social media.",
            "Regularly clear your browser cookies and cache.",
            "Use encrypted messaging apps for sensitive conversations.",
            "Check if your email has been breached online."
        };

        // Browsing tips
        private List<string> safeBrowsingTips = new List<string>
        {
            "Always look for 'https://' in URLs.",
            "Avoid using public WiFi for banking or shopping.",
            "Keep your browser and extensions updated.",
            "Use ad blockers to prevent malicious ads.",
            "Don't download files from untrusted websites.",
            "Enable your browser's safe browsing features."
        };

        public Chatbot()
        {
        }

        private string DetectSentiment(string input)
        {
            foreach (string word in worriedWords)
            {
                if (input.Contains(word))
                    return "worried";
            }
            foreach (string word in curiousWords)
            {
                if (input.Contains(word))
                    return "curious";
            }
            foreach (string word in frustratedWords)
            {
                if (input.Contains(word))
                    return "frustrated";
            }
            return "";
        }

        private string GetSentimentPrefix(string sentiment)
        {
            if (sentiment == "worried")
                return "It's normal to feel worried. Let me help you.\n\n";
            if (sentiment == "curious")
                return "Great that you're curious! Here's something for you.\n\n";
            if (sentiment == "frustrated")
                return "I understand the frustration. Let me simplify.\n\n";
            return "";
        }

        public string GetResponse(string userInput, string lastTopicParam)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return "I didn't quite understand that. Could you rephrase?\n\nTry asking about:\n- Passwords\n- Phishing\n- Safe browsing\n- Scams\n- Privacy\n- Two-factor authentication (2FA)";
            }

            string lowerInput = userInput.ToLower();
            string sentiment = DetectSentiment(lowerInput);
            string sentimentPrefix = GetSentimentPrefix(sentiment);

            // Handle yes/no responses
            if (waitingForYesNo)
            {
                if (lowerInput.Contains("yes") || lowerInput.Contains("yeah") || lowerInput.Contains("sure") || lowerInput.Contains("ok") || lowerInput.Contains("yep"))
                {
                    waitingForYesNo = false;
                    string tip = "";
                    switch (pendingTopic)
                    {
                        case "password":
                            tip = GetRandomPasswordTip();
                            break;
                        case "phishing":
                            tip = GetRandomPhishingTip();
                            break;
                        case "scam":
                            tip = GetRandomScamTip();
                            break;
                        case "privacy":
                            tip = GetRandomPrivacyTip();
                            break;
                        case "safe browsing":
                            tip = GetRandomSafeBrowsingTip();
                            break;
                    }
                    waitingForYesNo = true;
                    return sentimentPrefix + tip + "\n\nWould you like another tip?";
                }
                else if (lowerInput.Contains("no") || lowerInput.Contains("nope") || lowerInput.Contains("not") || lowerInput.Contains("nah"))
                {
                    waitingForYesNo = false;
                    pendingTopic = "";
                    return "No problem!\n\nYou can ask me about:\n- Password safety\n- Phishing\n- Safe browsing\n- Scam\n- Privacy\n- Two-factor authentication (2FA)";
                }
                else
                {
                    return "I didn't catch that. Would you like another tip? Please say yes or no.";
                }
            }

            // User interest
            if (lowerInput.Contains("i am interested in") || lowerInput.Contains("i like") || lowerInput.Contains("my favorite topic is"))
            {
                if (lowerInput.Contains("password"))
                {
                    userInterest = "password";
                    pendingTopic = "password";
                    waitingForYesNo = true;
                    return sentimentPrefix + $"Great! I'll remember that you're interested in password safety.\n\n{GetRandomPasswordTip()}\n\nSince you're interested in passwords, would you like another tip?";
                }
                if (lowerInput.Contains("phishing"))
                {
                    userInterest = "phishing";
                    pendingTopic = "phishing";
                    waitingForYesNo = true;
                    return sentimentPrefix + $"Great! I'll remember that you're interested in phishing awareness.\n\n{GetRandomPhishingTip()}\n\nSince you're interested in phishing, would you like another tip?";
                }
                if (lowerInput.Contains("privacy"))
                {
                    userInterest = "privacy";
                    pendingTopic = "privacy";
                    waitingForYesNo = true;
                    return sentimentPrefix + $"Great! I'll remember that you're interested in privacy.\n\n{GetRandomPrivacyTip()}\n\nSince you're interested in privacy, would you like another tip?";
                }
                if (lowerInput.Contains("scam"))
                {
                    userInterest = "scam";
                    pendingTopic = "scam";
                    waitingForYesNo = true;
                    return sentimentPrefix + $"Great! I'll remember that you're interested in scam prevention.\n\n{GetRandomScamTip()}\n\nSince you're interested in scams, would you like another tip?";
                }
                if (lowerInput.Contains("safe browsing") || lowerInput.Contains("browsing"))
                {
                    userInterest = "safe browsing";
                    pendingTopic = "safe browsing";
                    waitingForYesNo = true;
                    return sentimentPrefix + $"Great! I'll remember that you're interested in safe browsing.\n\n{GetRandomSafeBrowsingTip()}\n\nSince you're interested in safe browsing, would you like another tip?";
                }
            }

            // Recall info
            if (lowerInput.Contains("what do you know about me") || lowerInput.Contains("what do you remember"))
            {
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userInterest))
                {
                    return $"I know that your name is {userName} and you are interested in {userInterest} topics.";
                }
                else if (!string.IsNullOrEmpty(userName))
                {
                    return $"I know that your name is {userName}. You haven't told me your favorite topic yet.";
                }
                else
                {
                    return "I don't know much about you yet. Tell me your name and what you're interested in!";
                }
            }

            // tips
            if (lowerInput.Contains("tell me more") || lowerInput.Contains("another tip") || lowerInput.Contains("another one") || lowerInput.Contains("more tips"))
            {
                return sentimentPrefix + GetMoreOnLastTopic();
            }

            // Keywords
            if (lowerInput.Contains("password"))
            {
                lastTopic = "password";
                string tip = GetRandomPasswordTip();
                if (userInterest == "password")
                {
                    pendingTopic = "password";
                    waitingForYesNo = true;
                    tip = tip + "\n\nWould you like another tip?";
                }
                return sentimentPrefix + tip;
            }
            if (lowerInput.Contains("phishing"))
            {
                lastTopic = "phishing";
                string tip = GetRandomPhishingTip();
                if (userInterest == "phishing")
                {
                    pendingTopic = "phishing";
                    waitingForYesNo = true;
                    tip = tip + "\n\nWould you like another tip?";
                }
                return sentimentPrefix + tip;
            }
            if (lowerInput.Contains("scam"))
            {
                lastTopic = "scam";
                string tip = GetRandomScamTip();
                if (userInterest == "scam")
                {
                    pendingTopic = "scam";
                    waitingForYesNo = true;
                    tip = tip + "\n\nWould you like another tip?";
                }
                return sentimentPrefix + tip;
            }
            if (lowerInput.Contains("privacy"))
            {
                lastTopic = "privacy";
                string tip = GetRandomPrivacyTip();
                if (userInterest == "privacy")
                {
                    pendingTopic = "privacy";
                    waitingForYesNo = true;
                    tip = tip + "\n\nWould you like another tip?";
                }
                return sentimentPrefix + tip;
            }
            if (lowerInput.Contains("safe browsing") || lowerInput.Contains("browsing"))
            {
                lastTopic = "safe browsing";
                string tip = GetRandomSafeBrowsingTip();
                if (userInterest == "safe browsing")
                {
                    pendingTopic = "safe browsing";
                    waitingForYesNo = true;
                    tip = tip + "\n\nWould you like another tip?";
                }
                return sentimentPrefix + tip;
            }
            if (lowerInput.Contains("2fa") || lowerInput.Contains("two factor"))
            {
                lastTopic = "2fa";
                return sentimentPrefix + "TWO-FACTOR AUTHENTICATION:\n- Adds an extra layer of security\n- Requires a code from your phone\n- Works even if someone steals your password\n- Available on most major accounts\n- Always enable it when offered";
            }
            if (lowerInput.Contains("how are you"))
            {
                return sentimentPrefix + "I'm doing great! Thanks for asking! I'm ready to help you stay safe online.";
            }
            if (lowerInput.Contains("what is your purpose"))
            {
                return sentimentPrefix + "My purpose is to educate you about cybersecurity and help you protect yourself from:\n- Phishing attacks\n- Online scams\n- Weak passwords\n- Privacy breaches\n- Malware and viruses";
            }
            if (lowerInput.Contains("what can i ask") || lowerInput.Contains("what can I ask"))
            {
                return sentimentPrefix + "You can ask me about:\n- Passwords\n- Phishing\n- Safe browsing\n- Scams\n- Privacy\n- Two-factor authentication (2FA)\n\nJust type your question and I'll help you out!";
            }

            return "I didn't quite understand that. Could you rephrase?\n\nTry asking about:\n- Passwords\n- Phishing\n- Safe browsing\n- Scams\n- Privacy\n- Two-factor authentication (2FA)";
        }

        private string GetMoreOnLastTopic()
        {
            switch (lastTopic)
            {
                case "password":
                    return GetRandomPasswordTip();
                case "phishing":
                    return GetRandomPhishingTip();
                case "scam":
                    return GetRandomScamTip();
                case "privacy":
                    return GetRandomPrivacyTip();
                case "safe browsing":
                    return GetRandomSafeBrowsingTip();
                case "2fa":
                    return "TWO-FACTOR AUTHENTICATION:\n- Adds an extra layer of security\n- Requires a code from your phone\n- Works even if someone steals your password\n- Available on most major accounts\n- Always enable it when offered";
                default:
                    return "I don't have a topic to continue. Try asking me about passwords, phishing, scams, privacy, or safe browsing first!";
            }
        }

        private string GetRandomPasswordTip()
        {
            int index = random.Next(passwordTips.Count);
            return "PASSWORD TIP: " + passwordTips[index];
        }

        private string GetRandomPhishingTip()
        {
            int index = random.Next(phishingTips.Count);
            return "PHISHING TIP: " + phishingTips[index];
        }

        private string GetRandomScamTip()
        {
            int index = random.Next(scamTips.Count);
            return "SCAM TIP: " + scamTips[index];
        }

        private string GetRandomPrivacyTip()
        {
            int index = random.Next(privacyTips.Count);
            return "PRIVACY TIP: " + privacyTips[index];
        }

        private string GetRandomSafeBrowsingTip()
        {
            int index = random.Next(safeBrowsingTips.Count);
            return "SAFE BROWSING TIP: " + safeBrowsingTips[index];
        }

        public void SetUserName(string name)
        {
            userName = name;
        }

        public string GetUserName()
        {
            return userName;
        }
    }
}