using Application.Constants;

namespace WebUI.Models.SalaryCalculator;

public class SalaryCalculatorResultModel
{
    public decimal WageFund { get; set; }
    public decimal SocialTax { get; set; }
    public decimal UnemploymentInsuranceEmployer { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal PensionSecondPillar { get; set; }
    public decimal UnemploymentInsuranceEmployee { get; set; }
    public decimal IncomeTax { get; set; }
    public decimal NetSalary { get; set; }
    public Currency Currency { get; set; }

    public Dictionary<string, decimal> GetAllValues()
    {
        return typeof(SalaryCalculatorResultModel).GetProperties()
            .Where(x => Type.GetTypeCode(x.PropertyType) == TypeCode.Decimal)
            .ToDictionary(x => x.Name,
                x => x.GetValue(this) is decimal ? (decimal)(x.GetValue(this) ?? 0) : 0);
    }
}