#region

using Application.Constants;
using ValueType = Application.Constants.ValueType;

#endregion

namespace Application.SalaryCalculation;

public class SalaryCalculationBaseValues
{
    public decimal Value { get; set; }
    public ValueType ValueType { get; set; }
    public Rate Rate { get; set; }
    public Country Country { get; init; }
    public decimal Hours { get; set; }
    public decimal TaxFreeAmount { get; set; }
}