using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;
using System.Text.RegularExpressions;

namespace Chatbot
{
    class Program
    {
        static string userName = "";
        static string userInterest = "";

        static void Main(string[] args)
        {
            // Display the ASCII art logo
            DisplayAsciiArt();

            // Play the voice greeting
            PlayGreeting();

            // Display the Welcome Message
            DisplayWelcomeMessage();

            // Start the interaction loop
            StartInteractionLoop();
        }

        static void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Yellow; // Set ASCII art color to YELLOW
            string asciiArt = @" 
▐▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▀▌
▐                                                                                                                                                                             ▌
▐     _______      ____     __  _______       .-''-.  .-------.                     .-'''-.     .-''-.      _______     ___    _ .-------.   .-./`) ,---------.  ____     __  ▌
▐    /   __  \     \   \   /  /\  ____  \   .'_ _   \ |  _ _   \                   / _     \  .'_ _   \    /   __  \  .'   |  | ||  _ _   \  \ .-.')\          \ \   \   /  / ▌
▐   | ,_/  \__)     \  _. /  ' | |    \ |  / ( ` )   '| ( ' )  |                  (`' )/`--' / ( ` )   '  | ,_/  \__) |   .'  | || ( ' )  |  / `-' \ `--.  ,---'  \  _. /  '  ▌
▐ ,-./  )            _( )_ .'  | |____/ / . (_ o _)  ||(_ o _) /     _ _    _ _  (_ o _).   . (_ o _)  |,-./  )       .'  '_  | ||(_ o _) /   `-'`""""`    |   \     _( )_ .'   ▌
▐ \  '_ '`)      ___(_ o _)'   |   _ _ '. |  (_,_)___|| (_,_).' __  ( ' )--( ' )  (_,_). '. |  (_,_)___|\  '_ '`)     '   ( \.-.|| (_,_).' __ .---.     :_ _:  ___(_ o _)'    ▌
▐  > (_)  )  __ |   |(_,_)'    |  ( ' )  \'  \   .---.|  |\ \  |  |(_{;}_)(_{;}_).---.  \  :'  \   .---. > (_)  )  __ ' (`. _` /||  |\ \  |  ||   |     (_I_) |   |(_,_)'     ▌
▐ (  .  .-'_/  )|   `-'  /     | (_{;}_) | \  `-'    /|  | \ `'   / (_,_)--(_,_) \    `-'  | \  `-'    /(  .  .-'_/  )| (_ (_) _)|  | \ `'   /|   |    (_(=)_)|   `-'  /      ▌
▐  `-'`-'     /  \      /      |  (_,_)  /  \       / |  |  \    /                \       /   \       /  `-'`-'     /  \ /  . \ /|  |  \    / |   |     (_I_)  \      /       ▌
▐    `._____.'    `-..-'       /_______.'    `'-..-'  ''-'   `'-'                  `-...-'     `'-..-'     `._____.'    ``-'`-'' ''-'   `'-'  '---'     '---'   `-..-'        ▌
▐    ____    .--.      .--.   ____    .-------.        .-''-.  ,---.   .--.    .-''-.     .-'''-.    .-'''-.         _______       ,-----.  ,---------.                       ▌
▐  .'  __ `. |  |_     |  | .'  __ `. |  _ _   \     .'_ _   \ |    \  |  |  .'_ _   \   / _     \  / _     \       \  ____  \   .'  .-,  '.\          \                      ▌
▐ /   '  \  \| _( )_   |  |/   '  \  \| ( ' )  |    / ( ` )   '|  ,  \ |  | / ( ` )   ' (`' )/`--' (`' )/`--'       | |    \ |  / ,-.|  \ _ \`--.  ,---'                      ▌
▐ |___|  /  ||(_ o _)  |  ||___|  /  ||(_ o _) /   . (_ o _)  ||  |\_ \|  |. (_ o _)  |(_ o _).   (_ o _).          | |____/ / ;  \  '_ /  | :  |   \                         ▌
▐    _.-`   || (_,_) \ |  |   _.-`   || (_,_).' __ |  (_,_)___||  _( )_\  ||  (_,_)___| (_,_). '.  (_,_). '.        |   _ _ '. |  _`,/ \ _/  |  :_ _:                         ▌
▐ .'   _    ||  |/    \|  |.'   _    ||  |\ \  |  |'  \   .---.| (_ o _)  |'  \   .---..---.  \  :.---.  \  :       |  ( ' )  \: (  '\_/ \   ;  (_I_)                         ▌
▐ |  _( )_  ||  '  /\  `  ||  _( )_  ||  | \ `'   / \  `-'    /|  (_,_)\  | \  `-'    /\    `-'  |\    `-'  |       | (_{;}_) | \ `""""/  \ )   /(_(=)_)                        ▌
▐ \ (_ o _) /|    /  \    |\ (_ o _) /|  |  \    /   \       / |  |    |  |  \       /  \       /  \       /        |  (_,_)  /  '. \_/``"""".'   (_I_)                         ▌
▐  '.(_,_).' `---'    `---` '.(_,_).' ''-'   `'-'     `'-..-'  '--'    '--'   `'-..-'    `-...-'    `-...-'         /_______.'     '-----'      '---'                         ▌
▐                                                                                                                                                                             ▌
▐▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▄▌
             ";
            Console.WriteLine(asciiArt);
        }

        static void PlayGreeting()
        {
            try
            {
                string audioFilePath = @"C:\\Users\\RC_Student_lab\\source\\repos\\Chatbot\\audio\\Welcoming.wav";
                using (SoundPlayer player = new SoundPlayer(audioFilePath))
                {
                    player.PlaySync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while trying to play the greeting: " + ex.Message);
            }
        }

        static void DisplayWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("*  Welcome to the Cybersecurity Awareness Bot :) *");
            Console.WriteLine("------------------------------------------------");
            Console.ResetColor();
            GreetUser();
        }

        static void GreetUser()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("You: Hello, hi there! What's your name? ");
            userName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Bot: Welcome, {userName}! I'm here to help you stay safe online.");
            Console.ResetColor();
        }

        static void StartInteractionLoop()
        {
            Console.WriteLine("\n" + new string('-', 50));
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Bot: Feel free to ask me anything about cybersecurity!");
            Console.WriteLine(new string('-', 50));

            var responses = new Dictionary<string, List<string>>
            {
                { "password", new List<string> { "Always use strong passwords and never reuse them.", "Avoid using personal info like birthdays in passwords.", "Use a password manager to store your passwords securely." } },
                { "phishing", new List<string> { "Phishing emails often create urgency—don't rush.", "Don't click links in suspicious emails.", "Verify the sender's address before responding." } },
                { "privacy", new List<string> { "Adjust privacy settings on social media.", "Avoid oversharing personal information online.", "Use encrypted messaging apps for sensitive communication." } },
                { "scam", new List<string> { "Watch for unsolicited requests for money or info.", "If something seems too good to be true, it probably is.", "Scammers often impersonate known brands—stay alert." } }
            };

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("You: ");
                string userInput = Console.ReadLine().ToLower();

                // Validate input for numbers and gibberish
                if (Regex.IsMatch(userInput, @"^\d+$") || !Regex.IsMatch(userInput, @"[a-zA-Z]"))
                {
                    TypeWriter("Bot: That doesn't seem like a valid question. Please try again with real words.");
                    continue;
                }

                string sentiment = DetectSentiment(userInput);

                bool found = false;
                foreach (var key in responses.Keys)
                {
                    if (userInput.Contains(key))
                    {
                        found = true;
                        userInterest = key;
                        Random rand = new Random();
                        string reply = responses[key][rand.Next(responses[key].Count)];
                        TypeWriter($"Bot: {ApplySentiment(reply, sentiment)}");
                        break;
                    }
                }

                if (!found)
                {
                    if (userInput.Contains("exit"))
                    {
                        TypeWriter("Bot: Thank you for chatting! Stay safe online.");
                        return;
                    }
                    else if (userInput.Contains("more") && !string.IsNullOrEmpty(userInterest))
                    {
                        Random rand = new Random();
                        string reply = responses[userInterest][rand.Next(responses[userInterest].Count)];
                        TypeWriter($"Bot: Here's more about {userInterest}: {reply}");
                    }
                    else
                    {
                        TypeWriter("Bot: I'm not sure I understand. Can you try rephrasing?");
                    }
                }
                else
                {
                    TypeWriter("Bot: Was that helpful? (yes/no)");
                    Console.ForegroundColor = ConsoleColor.White;
                    string feedback = Console.ReadLine().ToLower();
                    if (feedback == "no")
                    {
                        TypeWriter("Bot: I'm sorry! Could you ask in a different way?");
                    }
                }
            }
        }

        static void TypeWriter(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(25);
            }
            Console.WriteLine();
        }

        static string DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared")) return "worried";
            if (input.Contains("frustrated") || input.Contains("angry")) return "frustrated";
            if (input.Contains("curious") || input.Contains("interested")) return "curious";
            return "neutral";
        }

        static string ApplySentiment(string response, string sentiment)
        {
            switch (sentiment)
            {
                case "worried": return "It's okay to feel that way. " + response;
                case "frustrated": return "I understand it's frustrating. " + response;
                case "curious": return "Great question! " + response;
                default: return response;
            }
        }
    }
}
