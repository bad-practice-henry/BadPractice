using Application.Constants;
using Application.DTO;
using Application.Interfaces;
using ValueType = Application.Constants.ValueType;

namespace Application.Services;

public class SalaryCalculationService : ISalaryCalculationService
{
    public SalaryCalculationResult CalculateResult(
        decimal value,
        ValueType valueType,
        Rate rate,
        Country country = Country.EE
    )
    {
        SalaryCalculationResult result;
        switch (country)
        {
            case Country.EE:
                result = CalculateResultEE(value, valueType);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(country), country, null);
        }

        result.Currency = GetCountryCurrency(country);

        return result;
    }

    private static SalaryCalculationResult CalculateResultEE(decimal value, ValueType valueType)
    {
        return valueType switch
        {
            ValueType.EmployerExpense => new SalaryCalculationResult
            {
                WageFund = value,
                SocialTax = value * 0.2466m,
                UnemploymentInsuranceEmployer = value * 0.006m,
                GrossSalary = value * 0.7474m,
                PensionSecondPillar = value * 0.0149m,
                UnemploymentInsuranceEmployee = value * 0.012m,
                IncomeTax = value * 0.0569m,
                NetSalary = value * 0.6636m
            },
            ValueType.Gross => new SalaryCalculationResult
            {
                WageFund = value * 1.3380m,
                SocialTax = value * 0.33m,
                UnemploymentInsuranceEmployer = value * 0.008m,
                GrossSalary = value,
                PensionSecondPillar = value * 0.02m,
                UnemploymentInsuranceEmployee = value * 0.016m,
                IncomeTax = value * 0.1928m,
                NetSalary = value * 0.7712m
            },
            ValueType.Net => new SalaryCalculationResult
            {
                WageFund = value * 1.507m,
                SocialTax = value * 0.3717m,
                UnemploymentInsuranceEmployer = value * 0.009m,
                GrossSalary = value * 1.1263m,
                PensionSecondPillar = value * 0.0225m,
                UnemploymentInsuranceEmployee = value * 0.018m,
                IncomeTax = value * 0.0857m,
                NetSalary = value
            },
            _ => throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null)
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