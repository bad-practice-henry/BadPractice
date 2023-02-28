#region

using Application.Constants;

#endregion

namespace Application.SalaryCalculation;

public class SalaryCalculationYearlyResult
{
    public decimal AnnualWageFund { get; set; }
    public decimal AnnualRevenueGross { get; set; }
    public decimal AnnualRevenueNet { get; set; }
    public Currency Currency { get; set; }
}