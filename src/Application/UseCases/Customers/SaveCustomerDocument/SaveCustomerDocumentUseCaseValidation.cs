using Application.UseCases.Customers.SaveCustomerDocument.Abstractions;
using Application.UseCases.Customers.SaveCustomerDocument.Ports;
using Infra.Repositories.Abstractions;
using FluentValidation;

namespace Application.UseCases.Customers.SaveCustomerDocument;

public class SaveCustomerDocumentUseCaseValidation : AbstractValidator<SaveCustomerDocumentInput>, ISaveCustomerDocumentUseCase
{
    private readonly ISaveCustomerDocumentUseCase _useCase;
    private readonly ICustomerRepository _repository;
    private ISaveCustomerDocumentOutputPort _outputPort = null!;

    public void SetOutputPort(ISaveCustomerDocumentOutputPort outputPort)
    {
        _outputPort = outputPort;
        _useCase.SetOutputPort(outputPort);
    }

    public SaveCustomerDocumentUseCaseValidation(ISaveCustomerDocumentUseCase useCase, ICustomerRepository repository)
    {
        _useCase = useCase;
        _repository = repository;

        RuleFor(input => input.CustomerId).NotEmpty().WithMessage("Customer Id is empty");
        RuleFor(input => input.FileStream).NotEmpty().WithMessage("File is empty");

        When(input => input.FileStream is not null, () =>
        {
            RuleFor(input => input.FileType)
                .Must(type => ValidFileTypes.Contains(type!.ToLower()) )
                .WithMessage($"File Type is invalid, allowed types: {string.Join(", ", ValidFileTypes)}");
        });
    }

    private string[] ValidFileTypes = {"image/png", "image/bmp"};

    public async Task ExecuteAsync(SaveCustomerDocumentInput input)
    {
        var validation = await ValidateAsync(input);
        if (!validation.IsValid)
        {
            var messages = string.Join(", ", validation.Errors.Select(error => error.ErrorMessage));

            _outputPort.Invalid(messages);
            return;
        }

        var customerExixtent = await _repository.GetCustomers(new(input.CustomerId));

        if (customerExixtent is null)
        {
            _outputPort.Error("Error on get Customer when checking if exists");
            return;
        }

        if (!customerExixtent.Any())
        {
            _outputPort.NotFound();
            return;
        }

        await _useCase.ExecuteAsync(input);
    }
}
