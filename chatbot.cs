using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CybersecurityChatbot
{
    public class Chatbot
    {
        private string userName;
        private Dictionary<string, string> responses;

        public Chatbot()
        {
            responses = new Dictionary<string, string>();
            InitializeResponses();
        }

        private void InitializeResponses()
        {
            responses.Add("how are you", "I'm doing great! Thanks for asking! I'm fully charged and ready to help you stay safe online!");
            responses.Add("what is your purpose", "I'm your Cybersecurity Awareness Assistant! My purpose is to educate you about online safety and help you protect yourself from cyber threats like phishing, scams, and weak passwords.");
            responses.Add("what can i ask", "You can ask me about:\n- Password safety\n- Phishing scams\n- Safe browsing habits\n- General cybersecurity tips\n\nJust type your question and I'll help you out!");
            responses.Add("password", "🔐 PASSWORD SAFETY TIP: Use strong, unique passwords for each account! A strong password should be at least 12 characters and mix uppercase, lowercase, numbers, and symbols. Never reuse passwords across different sites!");
            responses.Add("phishing", "🎣 PHISHING AWARENESS: Phishing is when scammers try to trick you into giving personal information. Never click suspicious links, don't share passwords via email, and always verify the sender's email address!");
            responses.Add("safe browsing", "🌐 SAFE BROWSING TIPS:\n1. Look for 'https://' in URLs\n2. Avoid public WiFi for banking\n3. Keep your browser updated\n4. Don't download files from untrusted sources\n5. Use ad blockers to avoid malicious ads");
            responses.Add("scam", "⚠️ SCAM ALERT: Scammers often create urgency to trick you. Remember:\n- Legitimate companies never ask for passwords\n- Too-good-to-be-true offers are usually scams\n- Verify calls by calling back official numbers\n- Never send money to strangers online");
            responses.Add("privacy", "🛡️ PRIVACY PROTECTION:\n- Review app permissions regularly\n- Use two-factor authentication when available\n- Be careful what you share on social media\n- Use a VPN on public networks\n- Regularly clear browser cookies");
        }

        public void SetUserName(string name)
        {
            userName = name;
        }

        public string GetUserName()
        {
            return userName;
        }

        public string GetResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return "I didn't catch that. Could you please say something? Try asking about passwords, phishing, or safe browsing!";
            }

            string lowerInput = userInput.ToLower();

            foreach (KeyValuePair<string, string> response in responses)
            {
                if (lowerInput.Contains(response.Key))
                {
                    return response.Value;
                }
            }

            return "I didn't quite understand that. Could you rephrase? Try asking about:\n- Password safety\n- Phishing scams\n- Safe browsing\n- Privacy protection\n\nOr type 'exit' to leave.";
        }

    }
}
