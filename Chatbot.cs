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
        private Random random = new Random();

        // Conversation flow tracking
        private string lastTopic = "";
        private int lastTipIndex = -1;

        // Password Tips
        private List<string> passwordTips = new List<string>
        {
            "Use at least 12 characters with a mix of uppercase, lowercase, numbers, and symbols.",
            "Never reuse passwords across different accounts.",
            "Use a password manager to generate and store strong, unique passwords.",
            "Avoid using personal information like birthdays, names, or pet names.",
            "Enable two-factor authentication whenever possible.",
            "Change your passwords immediately if you suspect any account has been compromised."
        };

        // Phishing Tips
        private List<string> phishingTips = new List<string>
        {
            "Never click on links in suspicious emails.",
            "Legitimate companies never ask for your password via email.",
            "Check the sender's email address carefully.",
            "Look for spelling mistakes and urgent language like 'Act now!'.",
            "Type the website URL yourself instead of clicking email links.",
            "Never download attachments from unknown senders."
        };

        // Scam Tips
        private List<string> scamTips = new List<string>
        {
            "If something sounds too good to be true, it probably is.",
            "Never send money to someone you've only met online.",
            "Legitimate companies won't call you out of the blue for tech support.",
            "Be wary of calls claiming you owe money to the government.",
            "Don't click pop-up ads claiming your computer is infected.",
            "Always verify charity requests before donating."
        };

        // Privacy Tips
        private List<string> privacyTips = new List<string>
        {
            "Review app permissions regularly.",
            "Use a VPN on public WiFi networks.",
            "Be careful what you share on social media.",
            "Regularly clear your browser cookies and cache.",
            "Use encrypted messaging apps for sensitive conversations.",
            "Check if your email has been breached online."
        };

        // Browsing Tips
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

        public string GetResponse(string userInput, string lastTopicParam)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return "I didn't catch that. Could you please say something?\n\nTry asking about:\n- Password safety\n- Phishing awareness\n- Safe browsing\n- Scam prevention\n- Privacy protection";
            }

            string lowerInput = userInput.ToLower();

            // Conversation flow - tell me more
            if (lowerInput.Contains("tell me more") || lowerInput.Contains("another tip") || lowerInput.Contains("another one") || lowerInput.Contains("more tips"))
            {
                return GetMoreOnLastTopic();
            }

            // Random tip 
            if (lowerInput.Contains("password"))
            {
                lastTopic = "password";
                return GetRandomPasswordTip();
            }
            if (lowerInput.Contains("phishing"))
            {
                lastTopic = "phishing";
                return GetRandomPhishingTip();
            }
            if (lowerInput.Contains("scam"))
            {
                lastTopic = "scam";
                return GetRandomScamTip();
            }
            if (lowerInput.Contains("privacy"))
            {
                lastTopic = "privacy";
                return GetRandomPrivacyTip();
            }
            if (lowerInput.Contains("safe browsing") || lowerInput.Contains("browsing"))
            {
                lastTopic = "safe browsing";
                return GetRandomSafeBrowsingTip();
            }
            if (lowerInput.Contains("2fa") || lowerInput.Contains("two factor"))
            {
                lastTopic = "2fa";
                return "TWO-FACTOR AUTHENTICATION:\n- Adds an extra layer of security\n- Requires a code from your phone\n- Works even if someone steals your password\n- Available on most major accounts\n- Always enable it when offered";
            }
            if (lowerInput.Contains("how are you"))
            {
                return "I'm doing great! Thanks for asking! I'm ready to help you stay safe online.";
            }
            if (lowerInput.Contains("what is your purpose"))
            {
                return "My purpose is to educate you about cybersecurity and help you protect yourself from:\n- Phishing attacks\n- Online scams\n- Weak passwords\n- Privacy breaches\n- Malware and viruses";
            }
            if (lowerInput.Contains("what can i ask") || lowerInput.Contains("what can I ask"))
            {
                return "You can ask me about:\n- Password safety\n- Phishing awareness\n- Safe browsing habits\n- Scam prevention\n- Privacy protection\n- Two-factor authentication (2FA)\n\nJust type your question and I'll help you out!";
            }

            return "I didn't quite understand that. Could you rephrase?\n\nTry asking about:\n- Password safety\n- Phishing scams\n- Safe browsing\n- Privacy protection\n- Two-factor authentication";
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