#region

using Application.SalaryCalculation;
using ValueType = Application.Constants.ValueType;

#endregion

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
    private const decimal MinExceptionSalaryNet = 1056.24m;
    private const decimal MaxExceptionSalaryNet = 1619.52m;

    public static SalaryCalculationResult Calculate(decimal value, ValueType valueType)
    {
        switch (valueType)
        {
            case ValueType.EmployerExpense:
                var grossFromWageFund = CalculateGrossFromWageFund(value);
                return CalculateFromGross(grossFromWageFund);
            case ValueType.Gross:
                return CalculateFromGross(value);
            case ValueType.Net:
                var grossFromNet = CalculateGrossFromNet(value);
                return CalculateFromGross(grossFromNet);
            default:
                throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null);
        }
    }

    private static decimal CalculateGrossFromNet(decimal value)
    {
        switch (value)
        {
            case <= 0:
                return 0;
            case <= MinExceptionSalaryNet:
                if (value <= TaxFreeMonthlyMax)
                    return value / (1 - UnemploymentInsuranceEmployeePercent - FundedPensionSecondPillarPercent);

                return (value - 130.8m) / 0.7712m; // 0.7712x + 130.8
            case > MinExceptionSalaryNet and < MaxExceptionSalaryNet:
                return (value - 305.2m) / 0.625867m; // 0.625867x + 305.2
            default:
                return value / 0.7712m; // 0.7712x
        }
    }

    private static SalaryCalculationResult CalculateFromGross(decimal value)
    {
        if (value <= 0) return new SalaryCalculationResult();

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
    }

    private static decimal CalculateGrossFromWageFund(decimal value)
    {
        return value / (1 + SocialTaxPercent + UnemploymentInsuranceEmployerPercent);
    }

    private static decimal CalculateIncomeTax(decimal value, decimal pensionSecondPillar, decimal unemploymentInsuranceEmployee,
        out decimal taxFree)
    {
        if (value - TaxFreeMonthlyMax - pensionSecondPillar - unemploymentInsuranceEmployee <= 0)
        {
            taxFree = TaxFreeMonthlyMax;
            if (value < TaxFreeMonthlyMax)
                taxFree = value;
            return 0;
        }

        taxFree = value switch
        {
            <= MinExceptionSalary => TaxFreeMonthlyMax,
            > MinExceptionSalary and < MaxExceptionSalary =>
                TaxFreeMonthlyMax -
                TaxFreeMonthlyMax / (MaxExceptionSalary - MinExceptionSalary) *
                (value - MinExceptionSalary),
            _ => 0
        };

        var incomeTax = (value - taxFree - pensionSecondPillar - unemploymentInsuranceEmployee) * IncomeTaxPercent;

        return incomeTax;
    }

    public static SalaryCalculationYearlyResult CalculateYearly(decimal baseValuesValue, ValueType baseValuesValueType)
    {
        return new SalaryCalculationYearlyResult
        {
            TaxFreeIncome = 0,
            AnnualRevenue = baseValuesValue * 12
        };
    }
}