using FluentValidation;

namespace OnlineCoursePlatform.Application.Features.SupportTickets.Commands.CreateSupportTicket
{
    public class CreateSupportTicketCommandValidator : AbstractValidator<CreateSupportTicketCommand>
    {
        public CreateSupportTicketCommandValidator()
        {
            RuleFor(r => r.Subject)
                .NotEmpty().WithMessage("Subject is required.");

            RuleFor(r => r.Message)
                .NotEmpty().WithMessage("Message is required.");
        }
    }
}
