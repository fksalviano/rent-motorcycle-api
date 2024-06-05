using Application.UseCases.Rents.SaveRent.Abstractions;
using Application.UseCases.Rents.SaveRent.Extensions;
using Application.UseCases.Rents.SaveRent.Ports;
using Domain.Configuration;
using Domain.Model.Extensions;
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
        if (input.IsUpdate)
        {
            await UpdateRentAsync(input);
        }
        else
        {
            await SaveRentAsync(input);
        }
    }

    private async Task SaveRentAsync(SaveRentInput input)
    {
        var rentPlan = _rentConfig.Plans.GetPlan(input.RentDays);
        if (rentPlan is null)
        {
            _outputPort.Invalid($"Thre is no Plan fo this RentDays, valid RentDays: {_rentConfig.Plans.ToStrings()}");
            return;
        }

        var rent = input.ToRent(rentPlan);
        var savedRent = await _repository.CreateRent(rent);

        if (savedRent is null)
        {
            _outputPort.Error("Error to save Rent");
            return;
        }

        var output = rent.ToOutput();
        _outputPort.Created(output);
    }

    private async Task UpdateRentAsync(SaveRentInput input)
    {

        var rentToUpdate = await _repository.GetRent(input.Id!.Value);
        if (rentToUpdate is null)
        {
            _outputPort.NotFound();
            return;
        }
        
        var rentPlan = _rentConfig.Plans.GetPlan(rentToUpdate.RentDays);
        var rent = input.ToRent(rentPlan, rentToUpdate);

        var updatedRents = _repository.UpdateRent(rent);

        if (updatedRents is null)
        {
            _outputPort.Error("Error to update Rent");
            return;
        }

        var output = rent.ToUpdateOutput();
        _outputPort.Updated(output);
    }
}
