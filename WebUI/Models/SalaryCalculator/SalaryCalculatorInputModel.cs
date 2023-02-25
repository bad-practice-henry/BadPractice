#region

using Application.Constants;
using ValueType = Application.Constants.ValueType;

#endregion

namespace WebUI.Models.SalaryCalculator;

public class SalaryCalculatorInputModel
{
    public decimal BaseValue { get; set; }
    public ValueType ValueType { get; set; }
    public Rate Rate { get; set; }
    public Country Country { get; set; }
}