using FluentValidation.Results;
using System.Diagnostics;
using System.Text.Json;

namespace GuessTheNumber
{
    public class GameLoop
    {
        private int _guess;
        private List<int> totalAttempts = new List<int>();
        private double bestTime;
        private double currentAttemptTime;
        private const double UNBEATABLE_RECORD = 120;

        RandomNumberService rng = new RandomNumberService();
        Stopwatch stopwatch = new Stopwatch();

        public void Start()
        {
            double currentBestTime = 0;
            if (File.Exists("game_results.json"))
            {
                string json = File.ReadAllText("game_results.json");
                GameResult previousGameResult = JsonSerializer.Deserialize<GameResult>(json);
                currentBestTime = previousGameResult.BestTime > 0 ? previousGameResult.BestTime : UNBEATABLE_RECORD;
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
            UserInputValidator validator = new UserInputValidator();
            ValidationResult result = new ValidationResult();
            do
            {
                var userInput = new UserInput();
                userInput.InputMessage = Console.ReadLine();

                _guess = userInput.Convert(userInput.InputMessage);
                userInput.Guess = _guess;

                result = validator.Validate(userInput);

                stopwatch.Start();

                if (!result.IsValid)
                {
                    Console.WriteLine("Wrong Input");
                    continue;
                }

                if (_guess < rng.CorrectNumber)
                {
                    Console.WriteLine($"The correct number is higher... {rng.CorrectNumber}");
                    UserInput.Attempts++;
                }
                else if (_guess > rng.CorrectNumber)
                {
                    Console.WriteLine($"The correct number is lower... {rng.CorrectNumber}");
                    UserInput.Attempts++;
                }
            }
            while (_guess != rng.CorrectNumber);

            if (_guess == rng.CorrectNumber)
            {
                UserInput.Attempts++;

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
