#region

using Application.Constants;

#endregion

namespace Application.SalaryCalculation;

public class SalaryCalculationYearlyResult
{
    public decimal TaxFreeIncome { get; set; }
    public decimal AnnualRevenue { get; set; }
    public Currency Currency { get; set; }
}