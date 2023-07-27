using System.Diagnostics;

namespace GuessTheNumber
{
    public class GameLoop
    {
        private int guessNumber = 0;
        RandomNumberService rng = new RandomNumberService();
        Stopwatch stopwatch = new Stopwatch();

        public void Start()
        {
            do
            {
                var userInput = new UserInput();

                stopwatch.Start();

                if (!userInput.InputConverter())
                {
                    Console.WriteLine("Wrong Input");
                    continue;
                }

                guessNumber = userInput.Guess;

                if (guessNumber < rng.CorrectNumber)
                {
                    Console.WriteLine($"The correct number is higher... {rng.CorrectNumber}");
                }
                else if (guessNumber > rng.CorrectNumber)
                {
                    Console.WriteLine($"The correct number is lower... {rng.CorrectNumber}");
                }
            }
            while (guessNumber != rng.CorrectNumber);

            if (guessNumber == rng.CorrectNumber)
            {
                stopwatch.Stop();
                Console.WriteLine($"Congratulations! In {UserInput.Attempts} attempts" +
                    $" and in {Math.Round(stopwatch.Elapsed.TotalSeconds, 2)} seconds" +
                    $" You just guessed the correct number: {rng.CorrectNumber.ToString()}");
            }
        }
    }
}
