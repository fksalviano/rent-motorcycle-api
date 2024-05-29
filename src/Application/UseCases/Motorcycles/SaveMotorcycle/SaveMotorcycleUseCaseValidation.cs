using Application.UseCases.Motorcycles.SaveMotorcycle.Abstractions;
using Application.UseCases.Motorcycles.SaveMotorcycle.Ports;
using FluentValidation;

namespace Application.UseCases.Motorcycles.SaveMotorcycle;

public class SaveMotorcycleUseCaseValidation : AbstractValidator<SaveMotorcycleInput>, ISaveMotorcycleUseCase
{
    private readonly ISaveMotorcycleUseCase _useCase;
    private ISaveMotorcycleOutputPort _outputPort = null!;

    public void SetOutputPort(ISaveMotorcycleOutputPort outputPort)
    {
        _outputPort = outputPort;
        _useCase.SetOutputPort(outputPort);
    }

    public SaveMotorcycleUseCaseValidation(ISaveMotorcycleUseCase useCase)
    {
        _useCase = useCase;

        RuleFor(input => input.Year)
            .NotEmpty()
            .WithMessage("Year is inalid");

        RuleFor(input => input.Model)
            .NotEmpty()
            .WithMessage("Model is empty");

        RuleFor(input => input.Plate)
            .NotEmpty()
            .WithMessage("Plate is empty");
    }

    public async Task ExecuteAsync(SaveMotorcycleInput input)
    {
        var validation = await ValidateAsync(input);

        if (!validation.IsValid)
        {
            var messages = string.Join(", ", validation.Errors
                .Select(error => error.ErrorMessage));

            _outputPort.Invalid(messages);
            return;
        }

        await _useCase.ExecuteAsync(input);
    }
}
