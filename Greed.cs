/*
 * Greed: A console game where the player rolls dice and tries to achieve as many points as possible.
 * 
 * Produced by Kareem Taleb
 * 
 * GitHub Link: 
 * 
 * IDE: Visual Studio
 * 
 * Email: kmt96@case.edu
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Greed: Input six numbers and find out the highest score you can get!
 */
namespace Greed_Game
{
    /*
     * Class Greed: The fields and methods that produce the game.
     */
    public class Greed
    {

        /*
         * Field diceRoll:
         * Job: Stores the input dice roll.
         *      Used to calculate the score for the user.
         */
        public string diceRoll;

        /*
         * Field finalScore:
         * Job: Stores the final score that is outputted to the user.
         */
        public int finalScore;

        /*
         * Field diceLength;
         * Job: A read only field used as the length of input possible.
         */
        private readonly int diceLength = 6;

        /*
         * Field eachOne:
         * Job: A read only field used as the score for each 1 in the input.
         */
        private readonly int eachOne = 100;

        /*
         * Field eachFive:
         * Job: A read only field used as the score for each 5 in the input.
         */
        private readonly int eachFive = 50;

        /*
         * Field instructions:
         * Job: Contains the editable instructions used in OutputInstructions().
         */
        private string[] instructions = new string[5];

        /* 
         * Field valueCount:
         * Job: Stores the dice index and the amount of times that index showed up in the input.
         */
        private Dictionary<char, int> valueCount = new Dictionary<char, int>();

        // Constructor that creates the game.
        public Greed()
        {
            do
            {
                InitializeGreed();
            }
            while (PlayAgain());
        }

        /*
         * Method InitializeGreed:
         * Job: Initializes the game.
         *      Calls OutputInstructions()
         *            CheckDiceRoll()
         *            InitializeDictionary()
         */
        public void InitializeGreed()
        {
            OutputInstructions();
            ScoreSheet();
            CheckDiceRoll();
            InitializeDictionary();
        }

        /*
         * Method InstructionsSet:
         * Job: Takes an index as input and returns the instruction at that from the instructions array.
         */
        private String InstructionsSet(int whichInst)
        {
            string welcome = "Welcome to Greed! A game where you roll die and try to get as many points as possible.";
            string firstInstructions = "Because we don't have a virtual die, all you have to do is input 6 numbers.";
            string secondInstructions = "Each number should be a number from 1 to 6.";
            string lastInstructions = "Based on what you input, you will receive the highest possible score you can achieve!";
            string exampleInput = "For example: 1 1 3 4 5 6";

            instructions[0] = welcome;
            instructions[1] = firstInstructions;
            instructions[2] = secondInstructions;
            instructions[3] = lastInstructions;
            instructions[4] = exampleInput;

            return instructions[whichInst];
        }

        /*
         * Method OutputInstructions:
         * Job: Takes an instructions set and writes it to the console.
         */
        private void OutputInstructions()
        {
            for (int start = 0; start < instructions.Length; start++)
            {
                Console.WriteLine(InstructionsSet(start));
            }
        }

        /*
         * Method ScoreSheet:
         * Job: Outputs the possible scores for each number.
         *      For example: Three 1's = 1000
         *      
         *      Have option to take this out of the instructions.
         */
        private void ScoreSheet()
        {
            Console.WriteLine("Would you like a cheat sheet for possible scores before you roll? (Y or N)");
            string answer = Console.ReadLine();
            if (answer.ToUpper() == "Y")
            {
                Console.WriteLine("Number                          Value");
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Each 1                          100");
                Console.WriteLine("Each 5                          50");
                Console.WriteLine("Three 1's                       1000");
                Console.WriteLine("Three 2's                       200");
                Console.WriteLine("Three 3's                       300");
                Console.WriteLine("Three 4's                       400");
                Console.WriteLine("Three 5's                       500");
                Console.WriteLine("Three 6's                       600");
                Console.WriteLine("Four of a kind                  2 * Three of a kind");
                Console.WriteLine("Five of a kind                  2 * Four of a kind");
                Console.WriteLine("-------------------------------------");
                Console.Write("Your Input: ");
            }
            else
            {
                Console.Write("Your Input: ");
            }
        }

        /*
         * Method RetrieveDiceRoll:
         * Job: Reads input from the console.
         *      Returns that same value but without spaces.
         */
        public string RetrieveDiceRoll()
        {
            string diceRollEntry;
            diceRollEntry = Console.ReadLine();
            diceRollEntry = diceRollEntry.Replace(" ", "");
            return diceRollEntry;
        }

        /*
         * Method CheckDiceRoll:
         * Job: Store the input string in the diceRoll field.
         *      If the input is longer or shorter than 6, asks the user to input another set of numbers.
         *      IF the input has values that are 0 or greater than 6, asks the user to input another set of numbers.
         *      Else, it exits the loop and diceRoll should have a proper value.
         */
        public void CheckDiceRoll()
        {
            bool properInput = false;

            diceRoll = RetrieveDiceRoll();

            while (!properInput)
            {
                if (diceRoll.Length != diceLength)
                {
                    Console.WriteLine("Please input only 6 numbers. Nothing more, nothing less.");
                    diceRoll = RetrieveDiceRoll();
                }
                else if (!CheckDiceRollValues())
                {
                    Console.WriteLine("One of your values is greater than 6 or less than 0.");
                    Console.Write("Please input 6 values that are numbers from 1 to 6: ");
                    diceRoll = RetrieveDiceRoll();
                }
                else
                {
                    properInput = true;
                }
            }
        }

        /*
         * Method CheckDiceRollValues:
         * Job: Checks each value of the input string.
         *      If the input has a number that is 0 or 6, this method returns false.
         *      Used in the CheckDiceRoll method.
         */
        public bool CheckDiceRollValues()
        {
            bool properValues = true; ;

            for (int index = 0; index < diceRoll.Length; index++)
            {
                if (diceRoll[index] > '6' || diceRoll[index] == '0')
                {
                    properValues = false;
                }
            }

            return properValues;
        }

        /*
         * Method InitializeDictionary:
         * Job: Initializes valueCount by:
         *      1. Looping through the diceRoll.
         *      2. Checking if the dictionary already contains that key, adds it if the key does not exist.
         *      3. Increments the index of the key if it already exists.
         */
        public void InitializeDictionary()
        {
            valueCount.Clear();
            int counter = 1;

            for (int index = 0; index < diceRoll.Length; index++)
            {
                if (!valueCount.ContainsKey(diceRoll[index]))
                {
                    valueCount.Add(diceRoll[index], counter);
                }
                else
                {
                    valueCount[diceRoll[index]]++;
                }
            }
            CalculateScore();
        }

        /*
         * Method CalculateScore:
         * Job: Using the values in the dictionary, it calculates the final score for the user.
         */
        public void CalculateScore()
        {
            finalScore = 0;

            if (valueCount.ContainsKey('1') && valueCount['1'] < 3)
            {
                finalScore = finalScore + (eachOne * valueCount['1']);
            }
            else if (valueCount.ContainsKey('1') && valueCount['1'] > 5)
            {
                finalScore = finalScore + eachOne;
            }

            if (valueCount.ContainsKey('5') && valueCount['5'] < 3)
            {
                finalScore = finalScore + (eachFive * valueCount['5']);
            }
            else if (valueCount.ContainsKey('5') && valueCount['5'] > 5)
            {
                finalScore = finalScore + eachFive;
            }

            foreach (var diceIndex in valueCount)
            {
                switch (diceIndex.Value)
                {
                    case 3:
                        finalScore = finalScore + ThreeOfAKind(diceIndex.Key);
                        break;

                    case 4:
                        finalScore = finalScore + FourOfAKind(diceIndex.Key);
                        break;

                    case 5:
                        finalScore = finalScore + FiveOfAKind(diceIndex.Key);
                        break;

                    case 6:
                        finalScore = finalScore + FiveOfAKind(diceIndex.Key);
                        break;
                }
            }
            Console.WriteLine("The best possible score for this dice roll is: " + finalScore);
        }

        /*
         * Method ThreeOfAKind:
         * Job: Takes an index as input and returns the score
         *      when a three of a kind occurs for that index.
         */
        public int ThreeOfAKind(char index)
        {
            int output = 0;

            switch (index)
            {
                case '1':
                    output = 1000;
                    break;

                case '2':
                    output = 200;
                    break;

                case '3':
                    output = 300;
                    break;

                case '4':
                    output = 400;
                    break;

                case '5':
                    output = 500;
                    break;

                case '6':
                    output = 600;
                    break;

                default:
                    output = 0;
                    break;
            }
            return output;
        }

        /*
         * Method FourOfAKind:
         * Job: Takes an index as input and returns the score
         *      when a four of a kind occurs for that index.
         */
        public int FourOfAKind(char index)
        {
            int output = 0;

            switch (index)
            {
                case '1':
                    output = 2000;
                    break;

                case '2':
                    output = 400;
                    break;

                case '3':
                    output = 600;
                    break;

                case '4':
                    output = 800;
                    break;

                case '5':
                    output = 1000;
                    break;

                case '6':
                    output = 1200;
                    break;
            }
            return output;
        }

        /*
         * Method FiveOfAKind:
         * Job: Takes an index as input and returns the score
         *      when a five of a kind occurs for that index.
         */
        public int FiveOfAKind(char index)
        {
            int output = 0;

            switch (index)
            {
                case '1':
                    output = 4000;
                    break;

                case '2':
                    output = 800;
                    break;

                case '3':
                    output = 1200;
                    break;

                case '4':
                    output = 1600;
                    break;

                case '5':
                    output = 2000;
                    break;

                case '6':
                    output = 2400;
                    break;
            }
            return output;
        }

        /*
         * Method PlayAgain:
         * Job: Returns true if the player wants to play again
         *      and false if the player does not want to play again.
         */
        public bool PlayAgain()
        {
            while (true)
            {
                string playAgain = "Would you like to roll again?";
                string answerType = "Please answer with Y for yes or any other letter for no.";
                Console.WriteLine(playAgain);
                Console.WriteLine(answerType);
                string response = Console.ReadLine().ToUpper();

                if (response == "Y")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /*
         * Main Method
         * Job: Creates an instance of the Greed game and begins the game.
         */
        static void Main(string[] args)
        {

            Greed game1 = new Greed();

        }
    }
}
