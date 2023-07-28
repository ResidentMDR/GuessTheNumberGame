namespace GuessTheNumber.Interfaces
{
    public interface IUserInput
    {
        int Guess { get; set; }
        string? InputMessage { get; }
        int Convert(string input);
    }
}
