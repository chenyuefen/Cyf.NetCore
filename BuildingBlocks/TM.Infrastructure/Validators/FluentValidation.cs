using FluentValidation;

namespace TM.Infrastructure.Validators
{
    public abstract class FluentModelValidator<TModel> : AbstractValidator<TModel>
    {
    }
}
