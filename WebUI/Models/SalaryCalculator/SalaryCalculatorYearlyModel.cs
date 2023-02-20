using Application.SalaryCalculation;

namespace WebUI.Models.SalaryCalculator;

public class SalaryCalculatorYearlyModel : SalaryCalculationYearlyResult
{
    public Dictionary<string, decimal> GetAllValues()
    {
        return typeof(SalaryCalculatorResultModel).GetProperties()
            .Where(x => Type.GetTypeCode(x.PropertyType) == TypeCode.Decimal)
            .ToDictionary(x => x.Name,
                x => x.GetValue(this) is decimal ? (decimal)(x.GetValue(this) ?? 0) : 0);
    }
}