public class UserInput
{
    public UserInput()
    {
        StringGuess = Console.ReadLine();
        InputConverter();
    }
    public int Guess 
    { 
        get => _guess;
        set => _guess = value;
    }

    public static int Attempts { get; set; } = 0;

    private int _guess;
    public string? StringGuess { get; set; }

    public void InputConverter()
    {
        if (!Int32.TryParse(StringGuess, out _guess))
        {
            Console.WriteLine("Wrong input");
        }
        else
        {
            Attempts++;
        }
    }
}