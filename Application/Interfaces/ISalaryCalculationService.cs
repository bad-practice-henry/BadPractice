using Application.Constants;
using Application.DTO;
using ValueType = Application.Constants.ValueType;

namespace Application.Interfaces;

public interface ISalaryCalculationService
{
    SalaryCalculationResult CalculateResult(decimal value, ValueType valueType, Rate rate, Country country = Country.EE);
}