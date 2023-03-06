namespace Application.SalaryCalculation;

public class SalaryCalculationOptions
{
    public bool UseTaxFree { get; set; }
    public bool UseUnemploymentInsuranceEmployer { get; set; }
    public bool UseUnemploymentInsuranceEmployee { get; set; }
    public bool UsePensionSecondPillar { get; set; }
}