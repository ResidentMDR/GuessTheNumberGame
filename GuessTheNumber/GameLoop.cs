using FluentValidation.Results;
using System.Diagnostics;
using System.Text.Json;

namespace GuessTheNumber
{
    public class GameLoop
    {
        private int guessNumber = 0;
        private List<int> totalAttempts = new List<int>();
        private double bestTime;
        private double currentAttemptTime;

        RandomNumberService rng = new RandomNumberService();
        Stopwatch stopwatch = new Stopwatch();

        public void Start()
        {
            double currentBestTime = 0;
            if (File.Exists("game_results.json"))
            {
                string json = File.ReadAllText("game_results.json");
                GameResult previousGameResult = JsonSerializer.Deserialize<GameResult>(json);
                currentBestTime = previousGameResult.BestTime;
                totalAttempts = previousGameResult.Attempts;
            }

            GameRunning();  // The game starts and finishes there

            totalAttempts.Add(UserInput.Attempts);
            double averageOfTotalAttempts = totalAttempts.Count > 0 ? totalAttempts.Average() : 0;
            bestTime = currentAttemptTime < currentBestTime ? currentAttemptTime : currentBestTime;

            GameResult gameResult = new GameResult();
            gameResult.Attempts = totalAttempts;
            gameResult.BestTime = bestTime;

            string jsonString = JsonSerializer.Serialize(gameResult);
            File.WriteAllText("game_results.json", jsonString);

            Console.WriteLine($"Best Time: {bestTime} s");
            Console.WriteLine($"Average Attempts: {Math.Round(averageOfTotalAttempts, 2)}");
        }

        private void GameRunning()
        {
            var validator = new UserInputValidator();
            var userInput = new UserInput();
            UserInput.Attempts = 0;

            do
            {
                userInput.StringGuess = Console.ReadLine();
                ValidationResult result = validator.Validate(userInput);
                guessNumber = userInput.Guess;
                stopwatch.Start();

                if (!userInput.InputConverter() || !result.IsValid)
                {
                    Console.WriteLine("Wrong Input");
                    continue;
                }

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
                totalAttempts.Add(UserInput.Attempts);
                currentAttemptTime = Math.Round(stopwatch.Elapsed.TotalSeconds, 2);

                Console.WriteLine($"Congratulations! In {UserInput.Attempts} attempts" +
                    $" and in {currentAttemptTime} seconds" +
                    $" You just guessed the correct number: {rng.CorrectNumber.ToString()}");
            }
        }
    }
}
