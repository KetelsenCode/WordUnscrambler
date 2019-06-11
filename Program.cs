using Scrambler.Data;
using Scrambler.Workers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Scrambler
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();
        private const string wordListFileName = "wordlist.txt";

        static void Main(string[] args)
        {
            bool continueWordUnscramble = true;
            do
            {
                Console.WriteLine("Enter option - M for manual, F for file");
                var option = Console.ReadLine() ?? string.Empty;

                switch (option.ToUpper())
                {
                    case "F":
                        Console.Write("Enter scrambled words file name: ");
                        ExecuteScrambledWordsFromFile();
                        break;
                    case "M":
                        Console.Write("Enter scrambled words manually: ");
                        ExecuteScrambledWordsManual();
                        break;
                    default:
                        Console.WriteLine("Option was not recognized");
                        break;
                }
                var continueWordUnscrambleDecision = string.Empty;
                do
                {
                    Console.WriteLine("Do you want to continue to unscramble? Y/N");
                    continueWordUnscrambleDecision = Console.ReadLine() ?? string.Empty;
                    //While it doesn't equal Y || N
                } while (!continueWordUnscrambleDecision.Equals("Y", StringComparison.OrdinalIgnoreCase) && 
                        !continueWordUnscrambleDecision.Equals("N", StringComparison.OrdinalIgnoreCase));

                //True if Y, false if N
                continueWordUnscramble = continueWordUnscrambleDecision.Equals("Y", StringComparison.OrdinalIgnoreCase);
            } while (continueWordUnscramble);
        }

        private static void ExecuteScrambledWordsManual()
        {
            var manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(",");
            DisplayUnscrambledWords(scrambledWords);
        }

        private static void ExecuteScrambledWordsFromFile()
        {
            var fileName = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = _fileReader.Read(fileName);
            DisplayUnscrambledWords(scrambledWords);
        }

        private static void DisplayUnscrambledWords(string[] scrambledWords)
        {
            string[] wordlist = _fileReader.Read(wordListFileName);

            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordlist);

            if (matchedWords.Any())
            {
                foreach (var matchedWord in matchedWords)
                {
                    Console.WriteLine("Match found for {0}: {1}",matchedWord.ScrambledWord, matchedWord.Word);
                }
            } else
            {
                Console.WriteLine("No matches found");
            }
        }
    }
}
