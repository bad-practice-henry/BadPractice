#region

using Application.Constants;
using Application.SalaryCalculation;
using Infrastructure.Interfaces;
using Infrastructure.Services.Calculations;

#endregion

namespace Infrastructure.Services;

public class SalaryCalculationService : ISalaryCalculationService
{
    public SalaryCalculationResult CalculateResult(SalaryCalculationBaseValues baseValues)
    {
        baseValues.Value = CalculateValueBasedOnRate(baseValues);

        var result = baseValues.Country switch
        {
            Country.EE => EECalculations.Calculate(baseValues.Value, baseValues.ValueType),
            _ => throw new ArgumentOutOfRangeException(nameof(baseValues.Country), baseValues.Country, null)
        };

        result.Currency = GetCountryCurrency(baseValues.Country);

        return result;
    }


    public SalaryCalculationYearlyResult CalculateYearly(SalaryCalculationBaseValues baseValues)
    {
        var result = baseValues.Country switch
        {
            Country.EE => EECalculations.CalculateYearly(baseValues.Value, baseValues.ValueType),
            _ => throw new ArgumentOutOfRangeException(nameof(baseValues.Country), baseValues.Country, null)
        };

        result.Currency = GetCountryCurrency(baseValues.Country);

        return result;
    }

    private decimal CalculateValueBasedOnRate(SalaryCalculationBaseValues baseValues)
    {
        return baseValues.Rate switch
        {
            Rate.Hourly => baseValues.Value * baseValues.Hours,
            Rate.Monthly => baseValues.Value,
            Rate.Yearly => baseValues.Value / 12,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private static Currency GetCountryCurrency(Country country)
    {
        return country switch
        {
            Country.EE => Currency.EUR,
            _ => throw new ArgumentOutOfRangeException(nameof(country), country, null)
        };
    }
}