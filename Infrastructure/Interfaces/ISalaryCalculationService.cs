#region

using Application.Constants;
using Application.SalaryCalculation;

#endregion

namespace Infrastructure.Interfaces;

public interface ISalaryCalculationService
{
    SalaryCalculationResult CalculateResult(SalaryCalculationBaseValues baseValues, SalaryCalculationOptions options);
    SalaryCalculationYearlyResult CalculateYearly(SalaryCalculationBaseValues baseValues, SalaryCalculationOptions options);
    Task<int> GetWorkingHoursOfCurrentMonth(Country country);
}