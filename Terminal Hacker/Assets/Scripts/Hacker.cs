using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    private enum Screen
    {
        MainMenu,
        Password,
        Win
    };
    private Screen currentScreen = Screen.MainMenu;

    private int level;
    private string menuHint = "You can access the main menu by typing 'menu'";

    [SerializeField]
    private string password;

    private Dictionary<int, string[]> levelPasswords = new Dictionary<int, string[]>
    {
        { 1, new string[] {"books", "aisle", "self", "shelf", "font", "borrow", "librarian"} },
        { 2, new string[] {"prisoner", "officer", "handcuffs", "holster", "uniform", "arrest"} },
        { 3, new string[] { "space", "astronaut", "station", "planetarySystem", "suit"} }
    };
    private Dictionary<string, string> levelOptions = new Dictionary<string, string>()
    {
        {"1", "Library" },
        {"2", "Police Station" },
        {"3", "NASA" },
        {"007", "Greetings, mr. Bond" }
    };
    void Start()
    {
        ShowMainMenu();
    }

    private void ShowMainMenu()
    {
        Terminal.ClearScreen();
        Terminal.WriteLine("Welcome to the machine!");
        Terminal.WriteLine("What would you like to hack?");
        foreach (var kvp in levelOptions)
        {
            Terminal.WriteLine($"Press {kvp.Key} for {kvp.Value}");
        }
        Terminal.WriteLine("Enter your selection:");
    }

    // Invoked by the Reflection within the NotifyCommandHandlers method in Terminal.cs when ENTER is pressed 
    void OnUserInput(string input)
    {
        RunMenu(input);
    }

    private void RunMenu(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
            currentScreen = Screen.MainMenu;
        }
        else if (currentScreen == Screen.MainMenu)
        {
            Terminal.WriteLine(menuHint);
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            Terminal.WriteLine(menuHint);
            RunPasswordMenu(input);
        }
    }

    private void RunPasswordMenu(string input)
    {
        bool isValidPassowrd = ValidatePassword(input);
        if (isValidPassowrd)
        {
            DisplayWinScreen();
        }
        else
        {
            Terminal.WriteLine("Invalid password");
        }
    }

    private void DisplayWinScreen()
    {
        Terminal.ClearScreen();
        StringBuilder message = new StringBuilder();

        if (level == 1)
        {
            message.AppendLine(@"Have a book...");
            message.Append(@" 
    _______
   /      /,
  /      //
 /______//
(______(/"
                               
            );
        }
        else if (level == 2)
        {
            message.AppendLine(@"You got the prison key!");
            message.Append(@" 
 __
/ o\
\_ /
 <|
 <|
 <|
 `                     
            ");
        }
        else if (level == 3)
        {
            message.Append(@"      
        _..._
      .'     '.      _
     /    .-""-\   _/ \
   .-|   /:.   |  |   |
   |  \  |:.   /.-'-./
   | .-'-;:__.'    =/
   .'=  *=|NASA _.='
  /   _.  |    ;
 ;-.-'|    \   |
/   | \    _\  _\
\__/'._;.  ==' ==\
         \    \   |
         /    /   /
         /-._/-._/
         \   `\  \
          `-._/._/");
        }

        Terminal.WriteLine(message.ToString());
    }

    private bool ValidatePassword(string input)
    {
        if (input == password)
        {
            return true;
        }
        return false;
    }

    private void RunMainMenu(string input)
    {
        if (levelOptions.ContainsKey(input))
        {
            if (input == "007")
            {
                // Greet mr. Bond
                string message = levelOptions[input];
                Terminal.WriteLine(message);
            }
            else // selected a valid level => start the game
            {
                level = int.Parse(input);
                StartLevel(level);
            }
        }
        else
        {
            string message = "Please select a valid level.";
            Terminal.WriteLine(message);
        }
    }

    private void StartLevel(int level)
    {
        currentScreen = Screen.Password;

        string[] availablePasswordsForLevel = levelPasswords[level];
        int index = Random.Range(0, availablePasswordsForLevel.Length);
        password = availablePasswordsForLevel[index];

        Terminal.ClearScreen();
        Terminal.WriteLine($"You have selected level {level} - {levelOptions[level.ToString()]}");
        Terminal.WriteLine("Please enter password:");
        Terminal.WriteLine($"Hint: {password.Anagram()}");
    }
}
