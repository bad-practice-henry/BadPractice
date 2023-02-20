using Application.Constants;
using Application.SalaryCalculation;
using Infrastructure.Interfaces;
using Infrastructure.Services.Calculations;
using ValueType = Application.Constants.ValueType;

namespace Infrastructure.Services;

public class SalaryCalculationService : ISalaryCalculationService
{
    public SalaryCalculationResult CalculateResult(decimal value, ValueType valueType, Country country = Country.EE)
    {
        var result = country switch
        {
            Country.EE => EECalculations.Monthly(value, valueType),
            _ => throw new ArgumentOutOfRangeException(nameof(country), country, null)
        };

        result.Currency = GetCountryCurrency(country);

        return result;
    }

    private static Currency GetCountryCurrency(Country country)
    {
        return country switch
        {
            Country.EE => Currency.EUR,
            _ => throw new ArgumentOutOfRangeException(nameof(country), country, null)
        };
    }


    public SalaryCalculationYearlyResult CalculateYearly(decimal modelBaseValue, ValueType modelValueType, Rate modelRate)
    {
        return new SalaryCalculationYearlyResult();
    }
}