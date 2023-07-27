namespace GuessTheNumber
{
    public class RandomNumberService
    {
        public RandomNumberService()
        {
            GetNumberAtRandom();
        }

        public int CorrectNumber { get; set; }


        private void GetNumberAtRandom()
        {
            Random rng = new Random();
            CorrectNumber = rng.Next(1, 100);
        }
    }
}
