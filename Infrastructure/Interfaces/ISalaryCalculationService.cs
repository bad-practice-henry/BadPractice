#region

using Application.Constants;
using Application.SalaryCalculation;
using ValueType = Application.Constants.ValueType;

#endregion

namespace Infrastructure.Interfaces;

public interface ISalaryCalculationService
{
    SalaryCalculationResult CalculateResult(decimal value, ValueType valueType, Country country = Country.EE);
    SalaryCalculationYearlyResult CalculateYearly(decimal modelBaseValue, ValueType modelValueType, Rate modelRate);
}