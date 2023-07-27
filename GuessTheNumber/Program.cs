using GuessTheNumber;

Console.WriteLine("Welcome to the Guess The Number Game, input your number from range 1-100");

var rng = new RandomNumberService();
int guess = 0;

while (guess != rng.CorrectNumber)
{
    var stringGuess = Console.ReadLine();
    guess = Convert.ToInt32(stringGuess);

    if (guess < rng.CorrectNumber)
    {
        Console.WriteLine("The correct number is higher...");
    }
    else if (guess > rng.CorrectNumber)
    {
        Console.WriteLine("The correct number is lower...");
    }
}

if (guess == rng.CorrectNumber)
{
    Console.WriteLine($"Congratulations! You just guessed the correct number: {rng.CorrectNumber.ToString()}");
}


