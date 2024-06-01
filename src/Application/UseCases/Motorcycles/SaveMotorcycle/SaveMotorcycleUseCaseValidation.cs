using Application.UseCases.Motorcycles.SaveMotorcycle.Abstractions;
using Application.UseCases.Motorcycles.SaveMotorcycle.Ports;
using Infra.Repositories.Abstractions;
using FluentValidation;

namespace Application.UseCases.Motorcycles.SaveMotorcycle;

public class SaveMotorcycleUseCaseValidation : AbstractValidator<SaveMotorcycleInput>, ISaveMotorcycleUseCase
{
    private readonly ISaveMotorcycleUseCase _useCase;
    private readonly IMotorcycleRepository _repository;
    private ISaveMotorcycleOutputPort _outputPort = null!;

    public void SetOutputPort(ISaveMotorcycleOutputPort outputPort)
    {
        _outputPort = outputPort;
        _useCase.SetOutputPort(outputPort);
    }

    public SaveMotorcycleUseCaseValidation(ISaveMotorcycleUseCase useCase, IMotorcycleRepository repository)
    {
        _useCase = useCase;
        _repository = repository;

        When(input => !input.IsUpdate, () =>
        {
            RuleFor(input => input.Year).NotEmpty().WithMessage("Year is inalid");
            RuleFor(input => input.Model).NotEmpty().WithMessage("Model is empty");
        });        
        RuleFor(input => input.Plate).NotEmpty().WithMessage("Plate is empty");
    }

    public async Task ExecuteAsync(SaveMotorcycleInput input)
    {
        var validation = await ValidateAsync(input);

        if (!validation.IsValid)
        {
            var messages = string.Join(", ", validation.Errors.Select(error => error.ErrorMessage));
            
            _outputPort.Invalid(messages);
            return;
        }

        var existentMotorcycles = await _repository.GetMotorcycles(new(input.Plate));

        if (existentMotorcycles is null)
        {
            _outputPort.Error("Error on get Motorcycle when checking if exists");
            return;
        }

        if (existentMotorcycles.Where(existent => existent.Id != input.Id).Any())
        {
            _outputPort.Invalid("Motorcycle already exists with the same Plate");
            return;
        }

        await _useCase.ExecuteAsync(input);
    }    
}
