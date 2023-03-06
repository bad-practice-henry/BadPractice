#region

using Application.Constants;
using Application.SalaryCalculation;
using ValueType = Application.Constants.ValueType;

#endregion

namespace WebUI.Models.SalaryCalculator;

public class SalaryCalculatorInputModel : SalaryCalculationBaseValues
{
    public SalaryCalculatorInputModel()
    {
        Value = 0;
        ValueType = ValueType.Gross;
        Rate = Rate.Monthly;
        Hours = 0;
        Country = Country.EE;
        SalaryCalculatorOptions = new SalaryCalculatorOptionsModel
        {
            UseTaxFree = true,
            UseUnemploymentInsuranceEmployer = true,
            UseUnemploymentInsuranceEmployee = true,
            UsePensionSecondPillar = true
        };
    }

    public SalaryCalculatorOptionsModel SalaryCalculatorOptions { get; set; }
}