#region

using Application.Constants;

#endregion

namespace Application.SalaryCalculation;

public class SalaryCalculationResult
{
    public decimal WageFund { get; set; }
    public decimal SocialTax { get; set; }
    public decimal UnemploymentInsuranceEmployer { get; set; }
    public decimal GrossSalary { get; set; }
    public decimal PensionSecondPillar { get; set; }
    public decimal UnemploymentInsuranceEmployee { get; set; }
    public decimal IncomeTax { get; set; }
    public decimal TaxFreeException { get; set; }
    public decimal NetSalary { get; set; }
    public Currency Currency { get; set; }
}