using Application.SalaryCalculation;
using ValueType = Application.Constants.ValueType;

namespace Infrastructure.Services.Calculations;

public static class EECalculations
{
    private const decimal SocialTaxPercent = 0.33m;
    private const decimal IncomeTaxPercent = 0.2m;
    private const decimal UnemploymentInsuranceEmployerPercent = 0.008m;
    private const decimal FundedPensionSecondPillarPercent = 0.02m;
    private const decimal UnemploymentInsuranceEmployeePercent = 0.016m;
    private const decimal TaxFreeMonthlyMax = 654m;
    private const decimal MinExceptionSalary = 1200m;
    private const decimal MaxExceptionSalary = 2100m;

    public static SalaryCalculationResult Monthly(decimal value, ValueType valueType)
    {
        switch (valueType)
        {
            case ValueType.EmployerExpense:
                return new SalaryCalculationResult
                {
                    WageFund = value,
                    SocialTax = value * 0.2466m,
                    UnemploymentInsuranceEmployer = value * 0.006m,
                    GrossSalary = value * 0.7474m,
                    PensionSecondPillar = value * 0.0149m,
                    UnemploymentInsuranceEmployee = value * 0.012m,
                    IncomeTax = value * 0.0569m,
                    NetSalary = value * 0.6636m
                };
            case ValueType.Gross:
                var socialTax = value * SocialTaxPercent;
                var unemploymentInsuranceEmployer = value * UnemploymentInsuranceEmployerPercent;
                var pensionSecondPillar = value * FundedPensionSecondPillarPercent;
                var unemploymentInsuranceEmployee = value * UnemploymentInsuranceEmployeePercent;
                var incomeTax = CalculateIncomeTax(value, pensionSecondPillar, unemploymentInsuranceEmployee, out var taxFree);
                var netSalary = value - (pensionSecondPillar + unemploymentInsuranceEmployee + incomeTax);

                return new SalaryCalculationResult
                {
                    WageFund = value + socialTax + unemploymentInsuranceEmployer,
                    SocialTax = socialTax,
                    UnemploymentInsuranceEmployer = unemploymentInsuranceEmployer,
                    GrossSalary = value,
                    PensionSecondPillar = pensionSecondPillar,
                    UnemploymentInsuranceEmployee = unemploymentInsuranceEmployee,
                    IncomeTax = incomeTax,
                    NetSalary = netSalary,
                    TaxFreeException = taxFree
                };
            case ValueType.Net:
                return new SalaryCalculationResult
                {
                    WageFund = value * 1.507m,
                    SocialTax = value * 0.3717m,
                    UnemploymentInsuranceEmployer = value * 0.009m,
                    GrossSalary = value * 1.1263m,
                    PensionSecondPillar = value * 0.0225m,
                    UnemploymentInsuranceEmployee = value * 0.018m,
                    IncomeTax = value * 0.0857m,
                    NetSalary = value
                };
            default:
                throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null);
        }
    }

    private static decimal CalculateIncomeTax(decimal value, decimal pensionSecondPillar, decimal unemploymentInsuranceEmployee,
        out decimal taxFree)
    {
        if (value - TaxFreeMonthlyMax - pensionSecondPillar - unemploymentInsuranceEmployee <= 0)
        {
            taxFree = TaxFreeMonthlyMax;
            return 0;
        }

        taxFree = value switch
        {
            <= MinExceptionSalary => TaxFreeMonthlyMax,
            > MinExceptionSalary and < MaxExceptionSalary => TaxFreeMonthlyMax -
                                                             TaxFreeMonthlyMax / (MaxExceptionSalary - MinExceptionSalary) *
                                                             (value - MinExceptionSalary),
            _ => 0
        };

        var incomeTax = (value - taxFree - pensionSecondPillar - unemploymentInsuranceEmployee) * IncomeTaxPercent;

        return incomeTax;
    }
}