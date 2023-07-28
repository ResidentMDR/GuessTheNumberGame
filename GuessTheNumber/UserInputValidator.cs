using FluentValidation;

namespace GuessTheNumber
{
    public class UserInputValidator : AbstractValidator<UserInput>
    {
        public UserInputValidator()
        {
            RuleFor(x => x.Guess).NotEmpty();
            RuleFor(x => x.Guess).LessThanOrEqualTo(100);
            RuleFor(x => x.Guess).GreaterThanOrEqualTo(1);
        }
    }
}
