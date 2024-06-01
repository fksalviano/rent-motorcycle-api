using Domain.Model;

namespace Domain.Configuration;

public class RentConfiguration
{
    public IEnumerable<RentPlan>? Plans { get; set; }    
}
