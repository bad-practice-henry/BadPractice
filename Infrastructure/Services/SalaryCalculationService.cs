#region

using System.Net.Http.Json;
using Application.Constants;
using Application.SalaryCalculation;
using Infrastructure.Interfaces;
using Infrastructure.Services.Calculations;

#endregion

namespace Infrastructure.Services;

public class SalaryCalculationService : ISalaryCalculationService
{
    private readonly HttpClient _httpClient;

    public SalaryCalculationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public SalaryCalculationResult CalculateResult(SalaryCalculationBaseValues baseValues, SalaryCalculationOptions options)
    {
        baseValues.Value = CalculateMonthlyValueBasedOnRate(baseValues);

        var result = baseValues.Country switch
        {
            Country.EE => EECalculations.Calculate(baseValues, options),
            _ => throw new ArgumentOutOfRangeException(baseValues.Country.ToString(), baseValues.Country, null)
        };

        result.Currency = GetCountryCurrency(baseValues.Country);

        return result;
    }

    public SalaryCalculationYearlyResult CalculateYearly(SalaryCalculationBaseValues baseValues, SalaryCalculationOptions options)
    {
        baseValues.Value = CalculateMonthlyValueBasedOnRate(baseValues);

        var result = baseValues.Country switch
        {
            Country.EE => EECalculations.CalculateYearly(baseValues, options),
            _ => throw new ArgumentOutOfRangeException(baseValues.Country.ToString(), baseValues.Country, null)
        };

        result.Currency = GetCountryCurrency(baseValues.Country);

        return result;
    }

    public async Task<int> GetWorkingHoursOfCurrentMonth(Country country)
    {
        var result = await _httpClient.GetFromJsonAsync<int[]>($"data/WorkingHours.{country.ToString()}.json");
        if (result is null) return 0;

        var currentMonth = DateTime.Now.Month;
        return result.Length < currentMonth ? 0 : result[currentMonth - 1];
    }

    private static decimal CalculateMonthlyValueBasedOnRate(SalaryCalculationBaseValues baseValues)
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