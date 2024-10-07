using Core.Models;
using FluentValidation;
<<<<<<<< HEAD:API/Validators/EventValidator.cs

namespace EventsTP.Validators;
========
using DataAccess.Entities;

namespace DataAccess.Validators;
>>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79:DataAccess/Validators/EventValidator.cs

public class EventValidator : AbstractValidator<EventEntity>
{
    public EventValidator()
    {
        RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 100).WithMessage("Name must be between 2 and 100 characters.");

        RuleFor(e => e.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(10, 1000).WithMessage("Description must be between 10 and 1000 characters.");

        RuleFor(e => e.TimeAndDate)
            .GreaterThan(DateTime.Now).WithMessage("EventEntity time must be in the future.");

        RuleFor(e => e.Place)
            .NotEmpty().WithMessage("Place is required.")
            .Length(2, 200).WithMessage("Place must be between 2 and 200 characters.");

        RuleFor(e => e.Category)
            .NotEmpty().WithMessage("Category is required.");
        
        RuleFor(e => e.ParticipantsMaxAmount)
            .Must(value => value >= 1).WithMessage("ParticipantsMaxAmount must be greater than or equal to 1.");

        RuleFor(e => e.Image)
            .NotEmpty().WithMessage("Image URL is required.");
    }
}