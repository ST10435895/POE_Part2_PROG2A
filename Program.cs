using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    // Store user preferences like name and topic of interest
    static Dictionary<string, string> memory = new Dictionary<string, string>();

    // Store predefined responses for keywords
    static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
    {
        { "password", new List<string> {
            "Use a mix of uppercase, lowercase, numbers, and special characters in your password.",
            "Avoid using personal information like birthdates in your passwords.",
            "Use a unique password for every account to reduce risk."
        }},
        { "scam", new List<string> {
            "If something seems too good to be true online, it probably is.",
            "Never share your OTP or banking info with anyone via email or SMS.",
            "Always verify the identity of the sender before clicking any suspicious links."
        }},
        { "privacy", new List<string> {
            "Regularly review app permissions on your devices.",
            "Avoid oversharing personal details on social media.",
            "Use a VPN when accessing public Wi-Fi to maintain privacy."
        }},
        { "phishing", new List<string> {
            "Don't click links in unsolicited emails.",
            "Check the sender's address for signs of impersonation.",
            "Report suspicious emails to your IT or email provider."
        }}
    };

    // Detect sentiment and provide empathetic response
    static string DetectSentiment(string input)
    {
        if (input.Contains("worried")) return "It's okay to feel worried. Cyber threats can be scary, but I'm here to help.";
        if (input.Contains("curious")) return "Curiosity is great! Learning about cybersecurity helps protect you online.";
        if (input.Contains("frustrated")) return "I understand your frustration. Let’s work through it together.";
        return null;
    }

    // Typewriter effect for bot messages
    static void TypeWriter(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(15); // Simulate typing
        }
        Console.WriteLine();
    }

    // Validate user input for non-empty, non-numeric, meaningful content
    static bool IsValidInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return false;
        if (input.All(char.IsDigit)) return false;
        if (!input.Any(char.IsLetter)) return false;
        return true;
    }

    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Cybersecurity Awareness Chatbot (Type 'exit' to quit)");
        Console.ResetColor();

        Console.Write("What's your name? ");
        string userName = Console.ReadLine();
        memory["name"] = userName;

        Console.WriteLine($"Nice to meet you, {userName}!");

        Console.Write("What's your favourite cybersecurity topic (e.g., privacy, password, scam)? ");
        string topic = Console.ReadLine()?.ToLower().Trim() ?? "";
        if (!string.IsNullOrWhiteSpace(topic))
        {
            memory["topic"] = topic;
            Console.WriteLine($"Great! I'll remember that you're interested in {topic}.");
        }

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nYou: ");
            Console.ResetColor();
            string userInput = Console.ReadLine()?.ToLower().Trim() ?? "";

            // Validate input before processing
            if (!IsValidInput(userInput))
            {
                TypeWriter("Bot: I'm not sure I understand. Please try rephrasing your question.");
                continue;
            }

            // Exit condition
            if (userInput == "exit")
            {
                TypeWriter($"Bot: Goodbye, {userName}! Stay cyber safe!");
                break;
            }

            // Detect sentiment
            string sentimentResponse = DetectSentiment(userInput);
            if (sentimentResponse != null)
            {
                TypeWriter($"Bot: {sentimentResponse}");
            }

            // Personalised follow-up using memory
            if (memory.ContainsKey("topic") && userInput.Contains("more") && userInput.Contains("info"))
            {
                string favTopic = memory["topic"];
                if (keywordResponses.ContainsKey(favTopic))
                {
                    var tip = keywordResponses[favTopic][new Random().Next(keywordResponses[favTopic].Count)];
                    TypeWriter($"Bot: Since you're interested in {favTopic}, here's a tip: {tip}");
                    continue;
                }
            }

            // Keyword detection and response
            bool keywordFound = false;
            foreach (var keyword in keywordResponses.Keys)
            {
                if (userInput.Contains(keyword))
                {
                    var responses = keywordResponses[keyword];
                    var randomResponse = responses[new Random().Next(responses.Count)];
                    TypeWriter($"Bot: {randomResponse}");
                    keywordFound = true;
                    break;
                }
            }

            // Fallback if no keyword matched
            if (!keywordFound)
            {
                TypeWriter("Bot: That’s an interesting question. I’m still learning! Try asking about passwords, scams, privacy, or phishing.");
            }
        }
    }
}

