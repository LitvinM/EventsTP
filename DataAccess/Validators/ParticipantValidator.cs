<<<<<<<< HEAD:API/Validators/ParticipantValidator.cs
using Core.Models;
using FluentValidation;

namespace EventsTP.Validators;
========
using DataAccess.Entities;
using FluentValidation;

namespace DataAccess.Validators;
>>>>>>>> ba1505c709d05d12d481ed83d53eb8355fe75b79:DataAccess/Validators/ParticipantValidator.cs

public class ParticipantValidator : AbstractValidator<ParticipantEntity>
{
    public ParticipantValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");

        RuleFor(p => p.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .Length(2, 50).WithMessage("Surname must be between 2 and 50 characters.");

        RuleFor(p => p.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Date of Birth must be in the past.");

        RuleFor(p => p.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
    }
}
