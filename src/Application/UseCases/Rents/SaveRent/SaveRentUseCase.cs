using Application.UseCases.Rents.SaveRent.Abstractions;
using Application.UseCases.Rents.SaveRent.Extensions;
using Application.UseCases.Rents.SaveRent.Ports;
using Domain.Configuration;
using Infra.Repositories.Abstractions;
using Microsoft.Extensions.Options;

namespace Application.UseCases.Rents.SaveRent;

public class SaveRentUseCase : ISaveRentUseCase
{
    private readonly IRentRepository _repository;    
    private readonly RentConfiguration _rentConfig;

    private ISaveRentOutputPort _outputPort = null!;

    public void SetOutputPort(ISaveRentOutputPort outputPort)  =>
        _outputPort = outputPort;

    public SaveRentUseCase(IRentRepository repository, IOptions<RentConfiguration> rentConfig)
    {    
        _repository = repository;
        _rentConfig = rentConfig.Value;
    }

    public async Task ExecuteAsync(SaveRentInput input)
    {
        var rentPlan = _rentConfig.Plans.GetPlan(input.RentDays);
        if (rentPlan is null)
        {
            _outputPort.Invalid($"No plain exist with this RentDays, valid RentDays values: {_rentConfig.Plans!.ToStrings()}");
            return;
        }

        var rent = input.ToRent(rentPlan.Value);

        var savedRents = input.IsUpdate switch
        {
            true =>  await _repository.UpdateRent(rent),
            false => await _repository.CreateRent(rent)
        };

        if (savedRents is null)
        {
            _outputPort.Error("Error to save Rent");
            return;
        }

        if (savedRents == 0 && input.IsUpdate)
        {
            _outputPort.NotFound();
            return;
        }

        if (input.IsUpdate)
            _outputPort.Updated(rent.ToUpdateOutput());
        else
            _outputPort.Created(rent.ToOutput());
    }
}
