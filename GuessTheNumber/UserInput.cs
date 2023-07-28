using GuessTheNumber.Interfaces;

namespace GuessTheNumber
{
    public class UserInput : IUserInput
    {
        public UserInput()
        {
            //InputMessage = Console.ReadLine();
        }
        
        public int Guess
        {
            get => _guess;
            set => _guess = value;
        }

        public static int Attempts { get; set; } = 0;

        private int _guess;
        public string? InputMessage { get; set; }

        public int Convert(string input)
        {
            if(Int32.TryParse(input, out _guess))
            {
                return _guess;
            }
            else
            {
                return -1;
            }
        }
    }
}