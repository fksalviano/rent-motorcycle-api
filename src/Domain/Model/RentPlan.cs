namespace Domain.Model;

public class RentPlan
{
    public int RentDays { get; set; }
    public decimal DailyValue { get; set; }
    public decimal PercentageFine { get; set; }
}
