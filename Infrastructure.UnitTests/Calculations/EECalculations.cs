#region

using Application.Constants;
using Application.SalaryCalculation;

#endregion

namespace Infrastructure.UnitTests.Calculations;

public class EECalculations : SalaryCalculationServiceTestsBase
{
    [Theory]
    [InlineData(1000, 902, 654, true)]
    [InlineData(1000, 800, 0, false)]
    public void CalculateResultFromGross_WithTrueOptionsAndEECountry_ShouldReturnCorrectResult(
        decimal inputGrossSalary,
        decimal expectedNetSalary,
        decimal expectedTaxFreeException,
        bool optionsValue
    )
    {
        // Arrange
        var baseValues = new SalaryCalculationBaseValues
        {
            Value = inputGrossSalary,
            ValueType = ValueType.Gross,
            Rate = Rate.Monthly,
            Country = Country.EE
        };

        var options = new SalaryCalculationOptions
        {
            UseTaxFree = optionsValue,
            UseUnemploymentInsuranceEmployer = optionsValue,
            UseUnemploymentInsuranceEmployee = optionsValue,
            UsePensionSecondPillar = optionsValue
        };

        // Act
        var result = SalaryCalculationService.CalculateResult(baseValues, options);

        // Assert
        Assert.Equal(inputGrossSalary, result.GrossSalary);
        Assert.Equal(expectedNetSalary, result.NetSalary);
        Assert.Equal(expectedTaxFreeException, result.TaxFreeException);
        Assert.Equal(Currency.EUR, result.Currency);
    }

    [Theory]
    [InlineData(902, 1000, 654, true)]
    [InlineData(800, 1000, 0, false)]
    public void CalculateResultFromNet_WithTrueOptionsAndEECountry_ShouldReturnCorrectResult(
        decimal inputNetSalary,
        decimal expectedGrossSalary,
        decimal expectedTaxFreeException,
        bool optionsValue)
    {
        // Arrange
        var baseValues = new SalaryCalculationBaseValues
        {
            Value = inputNetSalary,
            ValueType = ValueType.Net,
            Rate = Rate.Monthly,
            Country = Country.EE
        };

        var options = new SalaryCalculationOptions
        {
            UseTaxFree = optionsValue,
            UseUnemploymentInsuranceEmployer = optionsValue,
            UseUnemploymentInsuranceEmployee = optionsValue,
            UsePensionSecondPillar = optionsValue
        };

        // Act
        var result = SalaryCalculationService.CalculateResult(baseValues, options);

        // Assert
        Assert.Equal(expectedGrossSalary, result.GrossSalary);
        Assert.Equal(inputNetSalary, result.NetSalary);
        Assert.Equal(expectedTaxFreeException, result.TaxFreeException);
        Assert.Equal(Currency.EUR, result.Currency);
    }

    [Theory]
    [InlineData(1338, 1000, 654, 902, true)]
    [InlineData(1338, 1006.02, 0, 804.81, false)]
    public void CalculateResultFromWageFund_WithTrueOptionsAndEECountry_ShouldReturnCorrectResult(
        decimal inputEmployerExpense,
        decimal expectedGrossSalary,
        decimal expectedTaxFreeException,
        decimal expectedNetSalary,
        bool optionsValue)
    {
        // Arrange
        var baseValues = new SalaryCalculationBaseValues
        {
            Value = inputEmployerExpense,
            ValueType = ValueType.EmployerExpense,
            Rate = Rate.Monthly,
            Country = Country.EE
        };

        var options = new SalaryCalculationOptions
        {
            UseTaxFree = optionsValue,
            UseUnemploymentInsuranceEmployer = optionsValue,
            UseUnemploymentInsuranceEmployee = optionsValue,
            UsePensionSecondPillar = optionsValue
        };

        // Act
        var result = SalaryCalculationService.CalculateResult(baseValues, options);

        // Assert
        Assert.Equal(inputEmployerExpense, result.WageFund);
        Assert.Equal(expectedGrossSalary, result.GrossSalary);
        Assert.Equal(expectedNetSalary, result.NetSalary);
        Assert.Equal(expectedTaxFreeException, result.TaxFreeException);
        Assert.Equal(Currency.EUR, result.Currency);
    }
}