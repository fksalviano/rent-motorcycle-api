using Application.UseCases.Rents.SaveRent.Abstractions;
using Application.UseCases.Rents.SaveRent.Ports;
using Infra.Repositories.Abstractions;
using FluentValidation;
using Domain.Model.Enums;

namespace Application.UseCases.Rents.SaveRent;

public class SaveRentUseCaseValidation : AbstractValidator<SaveRentInput>, ISaveRentUseCase
{
    private readonly ISaveRentUseCase _useCase;    
    private readonly IRentRepository _rentRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMotorcycleRepository _motorcycleRepository;

    private ISaveRentOutputPort _outputPort = null!;

    public void SetOutputPort(ISaveRentOutputPort outputPort)
    {
        _outputPort = outputPort;
        _useCase.SetOutputPort(outputPort);
    }

    public SaveRentUseCaseValidation(ISaveRentUseCase useCase, IRentRepository rentRepository,
        ICustomerRepository customerRepository, IMotorcycleRepository motorcycleRepository)
    {            
        _useCase = useCase;

        _rentRepository = rentRepository;
        _customerRepository = customerRepository;
        _motorcycleRepository = motorcycleRepository;

        When(input => !input.IsUpdate, () =>
        {
            RuleFor(input => input.CustomerId).NotEmpty().WithMessage("CustomerId is invalid");
            RuleFor(input => input.MotorcycleId).NotEmpty().WithMessage("MotorcycleId is invalid");
            RuleFor(input => input.RentDays).NotEmpty().WithMessage("RentDays is invalid");            
            RuleFor(input => input.StartDate).NotEmpty().WithMessage("StartDate is invalid");            
        });

        When(input => input.IsUpdate, () =>
        {
            RuleFor(input => input.EndDate).NotEmpty().WithMessage("EndDate is invalid");            
        });
    }

    private string[] ValidLicensesToRent = { LicenseType.A.ToString(), LicenseType.AB.ToString() };

    public async Task ExecuteAsync(SaveRentInput input)
    {
        var validation = await ValidateAsync(input);
        if (!validation.IsValid)
        {
            var messages = string.Join(", ", validation.Errors.Select(error => error.ErrorMessage));
            _outputPort.Invalid(messages);
            return;
        }

        if (!input.IsUpdate)
        {
            var motorcycle = await _motorcycleRepository.GetMotorcycle(input.MotorcycleId);
            if (motorcycle is null)
            {
                _outputPort.Invalid("MotorcycleId not found");
                return;
            }

            var customer = await _customerRepository.GetCustomer(input.CustomerId);
            if (customer is null)
            {
                _outputPort.Invalid("CustomerId not found");
                return;
            }

            if (!ValidLicensesToRent.Contains(customer.DriverLicenseType))
            {
                _outputPort.Invalid($"Customer DriverLicense type should be {string.Join(", ", ValidLicensesToRent)}");
                return;
            }            

            var customerRents = await _rentRepository.GetRents(new(customerId: customer.Id));
            if (customerRents!.Any(rent => rent.EndDate is null))
            {
                _outputPort.Invalid("Customer already has an active Rent");
                return;
            }
        }

        await _useCase.ExecuteAsync(input);
    }
}
