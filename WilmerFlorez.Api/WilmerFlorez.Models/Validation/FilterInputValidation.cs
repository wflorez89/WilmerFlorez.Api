using FluentValidation;
using WilmerFlorez.Models.Input;

namespace WilmerFlorez.Models.Validation
{
    public class FilterInputValidation : AbstractValidator<FilterInput>
    {
        public FilterInputValidation()
        {
            RuleFor(x => x.PageSize).GreaterThan(0);
            RuleFor(x => x.PageNumber).GreaterThan(0);
        }
    }
}
