using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangmanGame {
 internal class Program {
  static void Main(string[] args) {
   string[] words = {"help", "request", "house", "tomatoes", "kittens", "reward", "curb", "progress", "deal", "comment"};

   Random rng = new Random();
   int winningStreak = 0;

   while (true) {
    string chosenWord = words[rng.Next(words.Length)];
    string blankStr = new string(chosenWord.Select(c => '_').ToArray());
    char letter;
    int numberOfTries = 5;
    bool onStreak = false;
    
    while (blankStr.Contains('_') && numberOfTries > 0) {
     Console.Clear();
     Console.WriteLine($"|{string.Join(" ", blankStr.ToCharArray())}| Tries: {numberOfTries}, Winning Streak: {winningStreak}");
     
     while (true) { 
      Console.Write("Enter a letter: ");

      try {
       letter = Convert.ToChar(Console.ReadLine().ToString().ToLower());
      
       if (!"abcdefghijklmnopqrstuvwxyz".Contains(letter)) {
        Console.WriteLine("Please enter a valid letter.");
        continue;
       }

       break;
      } catch (Exception ex) {
       Console.WriteLine("Please enter a valid letter.");
      }
     }
     
     if (chosenWord.Contains(letter)) {
      if (onStreak) numberOfTries++;
     
      blankStr = superimpose(chosenWord, blankStr, letter);
     
      onStreak = true;
     } else {
      numberOfTries--;
     
      onStreak = false;
     }
    }

    Console.Clear();
    Console.WriteLine(
     $"|{string.Join(" ", chosenWord.ToCharArray())}| " +
     "Tries: " + numberOfTries + 
     ", Winning Streak: " + (numberOfTries == 0 ? winningStreak : winningStreak + 1)
    );
    
    Console.WriteLine($"You {(numberOfTries == 0 ? "Lose! :(" : "Win!")}"); 
    
    while (true) { 
     Console.Write("Would you like to play again? (Y/N): ");
     letter = Convert.ToChar(Console.ReadLine().ToString().ToLower());

     if (letter != 'y' && letter != 'n') {
      Console.WriteLine("Please enter a valid answer.");
      continue;
     }

     break;
    }
    
    if (letter == 'n') {
     Console.WriteLine("Alright, hope you had fun!");
    
     break;
    } else {
     Console.Clear();

     winningStreak++;
    }
   }
  }

  static string superimpose(string str1, string str2, char letter) {
   char[] charArr = str2.ToCharArray();
  
   for (int i = 0; i < str1.Length; i++) {
    if (str1[i] == letter) charArr[i] = letter;
   }

   return new string(charArr);
  }
 }
}
