using System;
using System.Diagnostics;

namespace Promise.MathGame
{
    public class MathGame
    {
        private static readonly Random _random = new Random();
        private static readonly Stopwatch _stopwatch = new Stopwatch();
        private static readonly List<string> GameHistory = new();
        private static readonly Dictionary<int?, char> Operator = new()
        {
            {1, '+'},
            {2, '-' },
            {3, '*' },
            {4, '/' },

        };
        public static (int op1, int op2, int result) GenerateQuestion(int operation, int level)
        {
            int op1 = 0, op2 = 0, result = 0;
            switch (level)
            {
                case 1:
                    op1 = _random.Next(1, 30);
                    op2 = _random.Next(1, 30);
                    break;
                case 2:
                    op1 = _random.Next(30, 60);
                    op2 = _random.Next(30, 60);
                    break;
                case 3:
                    op1 = _random.Next(60, 101);
                    op2 = _random.Next(60, 101);
                    break;
            }
            switch (operation)
            {
                case 1:
                    result = op1 + op2;
                    break;
                case 2:
                    result = op1 - op2;
                    break;
                case 3:
                    result = op1 * op2;
                    break;
                case 4:
                    while (op1 % op2 != 0)
                    {
                        switch (level)
                        {
                            case 1:
                                op1 = _random.Next(1, 30);
                                op2 = _random.Next(1, 30);
                                break;
                            case 2:
                                op1 = _random.Next(30, 60);
                                op2 = _random.Next(30, 60);
                                break;
                            case 3:
                                op1 = _random.Next(60, 101);
                                op2 = _random.Next(60, 101);
                                break;
                        }
                    }
                    result = op1 / op2;
                    break;
            }
            return (op1, op2, result);

        }
       public static (int value1, int value2, int result, int op) RandomGame(int level)
        {
            
            int randomChoice = _random.Next(1, 5);
            var op = randomChoice;
            var question = GenerateQuestion(op, level);
            return (question.op1, question.op2, question.result, op);
        }
        public static string GetHistory()
        {
            Console.WriteLine();
            return string.Join("\n", GameHistory);
        }
        public static void StartGame()
        {
            int rounds;
            int gameLevel;
            // Display list of operations available
            Console.WriteLine();
            Console.WriteLine($"""
                Select the operation you wish to perform:
                (1) Addition
                (2) Subtraction
                (3) Multiplication
                (4) Division
                (5) Random Game
                """
            );
            int operation;
            int score = 0;
            int answer;

            // Operation
            while (true)
            {
                Console.WriteLine();
                Console.Write("\nEnter the operation: ");
                string op = Console.ReadLine();
                if (int.TryParse(op, out operation))
                {
                    if (operation < 1 || operation > 5)
                    {
                        Console.WriteLine("Operation must be an option between 1 - 5 ");
                        continue;
                    }
                    break;
                }
                else
                {
                    Console.WriteLine("Enter a valid integer option for the operation to perform");
                }
            }
            // Level
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(
                            $"""
                            Select a difficulty level:
                            (1) Easy
                            (2) Medium
                            (3) Hard
                            """
                );
                Console.Write("select a level: ");
                string level = Console.ReadLine();
                if (int.TryParse(level, out gameLevel))
                    break;
                else {
                    Console.WriteLine("Enter a valid game level.");
                    continue;
                }
;            
            }
            // Number of questions
            while (true)
            {
                Console.WriteLine();
                Console.Write("\nHow many questions do you want to answer: ");
                string questions = Console.ReadLine();
                if (int.TryParse(questions, out rounds))
                {
                    if (rounds <= 0)
                    {
                        Console.WriteLine("Number of questions must be a positive integer.");
                        continue;
                    }
                    break;
                }
                Console.WriteLine("Invalid response");
            }
            (int op1, int op2, int result) output = (0, 0, 0);
            (int op1, int op2, int result, int op) randomGameOutput = (0, 0, 0, 0);
            // Loop
            _stopwatch.Start();
            for (int i = 1; i < rounds + 1; i++)
            {
                if (operation == 5)
                {
                    randomGameOutput = RandomGame(gameLevel);
                    operation = randomGameOutput.op;
                    output = (randomGameOutput.op1, randomGameOutput.op2, randomGameOutput.result);
                }
                else
                {
                    output = GenerateQuestion(operation, gameLevel);
                }
                Console.WriteLine($"({i}) {output.op1} {Operator[operation]} {output.op2}");
              
                while (true)
                {
                    Console.Write("Answer: ");
                    var response = Console.ReadLine();
                    if (!int.TryParse(response, out answer))
                    {
                        Console.WriteLine("Answer must be an integer value.");
                        continue;
                    }
                    break;
                }
                if (answer == output.result)
                {
                    score++;
                    Console.WriteLine($"Congratulations, the answer is {output.result} and you chose {answer}");
                    GameHistory.Add($"{output.op1} {Operator[operation]} {output.op2} = {output.result}");
                }
                else {
                    Console.WriteLine($"Oops, wrong answer! The answer is {output.result} and you chose {answer}");
                    GameHistory.Add($"{output.op1} {Operator[operation]} {output.op2} = {output.result}");
                }
                Console.WriteLine();
                
                if (operation == randomGameOutput.op)
                {
                    operation = 5;
                }
            }
            
            _stopwatch.Stop();
            Console.WriteLine($"Final Score: {score}/{rounds}.");
            
            // Display the elapsed time
            Console.WriteLine($"\nTime taken to play the game: {_stopwatch.Elapsed.TotalSeconds} seconds");
        }
    }
}
