using Application.UseCases.Customers.SaveCustomer.Abstractions;
using Application.UseCases.Customers.SaveCustomer.Ports;
using Infra.Repositories.Abstractions;
using Infra.Repositories.Filters;
using FluentValidation;

namespace Application.UseCases.Customers.SaveCustomer;

public class SaveCustomerUseCaseValidation : AbstractValidator<SaveCustomerInput>, ISaveCustomerUseCase
{
    private readonly ISaveCustomerUseCase _useCase;
    private readonly ICustomerRepository _repository;
    private ISaveCustomerOutputPort _outputPort = null!;

    public void SetOutputPort(ISaveCustomerOutputPort outputPort)
    {
        _outputPort = outputPort;
        _useCase.SetOutputPort(outputPort);
    }

    public SaveCustomerUseCaseValidation(ISaveCustomerUseCase useCase, ICustomerRepository repository)
    {
        _useCase = useCase;
        _repository = repository;

        RuleFor(input => input.Name).NotEmpty().WithMessage("Name is empty");

        RuleFor(input => input.TaxId).NotEmpty().WithMessage("TaxId is empty");

        RuleFor(input => input.BornDate).NotEmpty().WithMessage("BornDate is invalid");

        RuleFor(input => input.DriverLicenseNumber).NotEmpty().WithMessage("DriverLicenseNumber is invalid");

        RuleFor(input => input.DriverLicenseType).NotEmpty().WithMessage("DriverLicenseType is empty");        
    }

    public async Task ExecuteAsync(SaveCustomerInput input)
    {
        var validation = await ValidateAsync(input);

        if (!validation.IsValid)
        {
            var messages = string.Join(", ", validation.Errors.Select(error => error.ErrorMessage));
            
            _outputPort.Invalid(messages);
            return;
        }

        var filter = new CustomerFilter(input.TaxId);
        var existentsByTaxId = await _repository.GetCustomers(filter);

        if (existentsByTaxId is null)
        {
            _outputPort.Error("Error on get Customer when checking if exists");
            return;
        }

        if (existentsByTaxId.Where(existent => existent.Id != input.Id).Any())
        {
            _outputPort.Invalid("Customer already exists with this TaxId");
            return;
        }

        filter = new CustomerFilter(input.DriverLicenseNumber);
        var existentsByLicense = await _repository.GetCustomers(filter);

        if (existentsByLicense!.Where(existent => existent.Id != input.Id).Any())
        {
            _outputPort.Invalid("Customer already exists with this DriverLicenseNumber");
            return;
        }

        await _useCase.ExecuteAsync(input);
    }    
}
