using Application.UseCases.Rents.GetRents.Abstractions;
using Application.UseCases.Rents.GetRents.Extensions;
using Application.UseCases.Rents.GetRents.Ports;
using Domain.Configuration;
using Domain.Model.Extensions;
using Infra.Repositories.Abstractions;
using Microsoft.Extensions.Options;

namespace Application.UseCases.Rents.GetRents;

public class GetRentsUseCase : IGetRentsUseCase
{
    private readonly IRentRepository _repository;
    private readonly RentConfiguration _rentConfig;

    private IGetRentsOutputPort _outputPort = null!;
    
    public void SetOutputPort(IGetRentsOutputPort outputPort)  =>
        _outputPort = outputPort;        

    public GetRentsUseCase(IRentRepository repository, IOptions<RentConfiguration> rentConfig)
    {            
        _repository = repository;
        _rentConfig = rentConfig.Value;
    }

    public async Task ExecuteAsync(GetRentsInput input)
    {
        var filter = input.ToFilter();
        var rents = await _repository.GetRents(filter);

        if (rents is null)
        {
            _outputPort.Error("Error to get Rents");
            return;
        }

        if (!rents.Any())
        {
            _outputPort.NotFound();
            return;
        }

         if (input.IsGetById)
         {
            var rent = rents.First();            
            _outputPort.Ok(rent.ToOutput(input, _rentConfig));
         }
        else
            _outputPort.Ok(rents.ToOutput());
    }

}
