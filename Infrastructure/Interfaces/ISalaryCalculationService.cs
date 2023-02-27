#region

using Application.SalaryCalculation;

#endregion

namespace Infrastructure.Interfaces;

public interface ISalaryCalculationService
{
    SalaryCalculationResult CalculateResult(SalaryCalculationBaseValues baseValues);
    SalaryCalculationYearlyResult CalculateYearly(SalaryCalculationBaseValues baseValues);
}