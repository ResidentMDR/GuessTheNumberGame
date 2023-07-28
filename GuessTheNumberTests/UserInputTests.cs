using GuessTheNumber;

namespace GuessTheNumberTests
{
    [TestFixture]
    public class UserInputTests
    {
        [Test]
        [TestCase("1", 1)]
        [TestCase("100", 100)]
        [TestCase("invalid input", -1)]
        public void Convert_TwoNumbers_ParsesStringToInt(string actual, int expected)
        {
            var userInput = new UserInput();

            var result = userInput.Convert(actual);

            Assert.That(expected, Is.EqualTo(result));
        }
    }
}
