using System;

class SubtractionGame
{
    static void Main()
    {
        Random random = new Random();
        bool playAgain = true;

        while (playAgain)
        {
            int num1 = random.Next(0, 21);
            int num2 = random.Next(0, num1);

            Console.WriteLine("What is the result of subtracting " + num2 + " from " + num1 + "?");
            Console.Write("Your answer: ");
            int userAnswer = Convert.ToInt32(Console.ReadLine());

            int correctAnswer = num1 - num2;

            if (userAnswer == correctAnswer)
            {
                Console.WriteLine("Correct! Well done.");
            }
            else
            {
                Console.WriteLine("Incorrect. The correct answer is: " + correctAnswer);
            }

            Console.WriteLine("Do you want to play again? (yes/no)");
            string playAgainInput = Console.ReadLine().ToLower();

            playAgain = playAgainInput == "yes" || playAgainInput == "y";
        }

        Console.WriteLine("Thanks for playing!");
    }
}