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
            Country.EE => EECalculations.Calculate(baseValues),
            _ => throw new ArgumentOutOfRangeException(baseValues.Country.ToString(), baseValues.Country, null)
        };

        result.Currency = GetCountryCurrency(baseValues.Country);

        return result;
    }


    public SalaryCalculationYearlyResult CalculateYearly(SalaryCalculationBaseValues baseValues)
    {
        baseValues.Value = CalculateValueBasedOnRate(baseValues);

        var result = baseValues.Country switch
        {
            Country.EE => EECalculations.CalculateYearly(baseValues),
            _ => throw new ArgumentOutOfRangeException(baseValues.Country.ToString(), baseValues.Country, null)
        };

        result.Currency = GetCountryCurrency(baseValues.Country);

        return result;
    }

    private static decimal CalculateValueBasedOnRate(SalaryCalculationBaseValues baseValues)
    {
        return baseValues.Rate switch
        {
            Rate.Hourly => baseValues.Value * baseValues.Hours,
            Rate.Monthly => baseValues.Value,
            Rate.Yearly => baseValues.Value / 12,
            _ => throw new ArgumentOutOfRangeException(baseValues.Rate.ToString(), baseValues.Rate, null)
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