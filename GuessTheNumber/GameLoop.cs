using System.ComponentModel.Design;

namespace GuessTheNumber
{
    public class GameLoop
    {
        private int guessNumber = 0;
        RandomNumberService rng = new RandomNumberService();

        public void Start()
        {
            do
            {
                var userInput = new UserInput();
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
                Console.WriteLine($"Congratulations! In {UserInput.Attempts} attempts" +
                    $" You just guessed the correct number: {rng.CorrectNumber.ToString()}");
            }
        }
    }
}
