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

    public static SalaryCalculationResult Calculate(SalaryCalculationBaseValues baseValues, SalaryCalculationOptions options)
    {
        switch (baseValues.ValueType)
        {
            case ValueType.EmployerExpense:
                var grossFromWageFund = CalculateGrossFromWageFund(baseValues.Value, options.UseUnemploymentInsuranceEmployer);
                return CalculateFromGross(grossFromWageFund, options);
            case ValueType.Gross:
                return CalculateFromGross(baseValues.Value, options);
            case ValueType.Net:
                var grossFromNet = CalculateGrossFromNet(baseValues.Value, options);
                return CalculateFromGross(grossFromNet, options);
            default:
                throw new ArgumentOutOfRangeException(baseValues.ValueType.ToString(), baseValues.ValueType, null);
        }
    }

    private static decimal CalculateGrossFromNet(decimal value, SalaryCalculationOptions options)
    {
        switch (value)
        {
            case <= 0:
                return 0;
            case <= MinExceptionSalaryNet:
                if (value <= TaxFreeMonthlyMax)
                    return value / (1 -
                                    (options.UseUnemploymentInsuranceEmployee ? UnemploymentInsuranceEmployeePercent : 0) -
                                    (options.UsePensionSecondPillar ? FundedPensionSecondPillarPercent : 0));

                return (value - 130.8m) / 0.7712m;
            case > MinExceptionSalaryNet and < MaxExceptionSalaryNet:
                return (value - 305.2m) / 0.625867m;
            default:
                return value / 0.7712m;
        }
    }

    private static SalaryCalculationResult CalculateFromGross(decimal value, SalaryCalculationOptions salaryCalculationOptions)
    {
        if (value <= 0) return new SalaryCalculationResult();

        var socialTax = value * SocialTaxPercent;
        var unemploymentInsuranceEmployer =
            value * (salaryCalculationOptions.UseUnemploymentInsuranceEmployer ? UnemploymentInsuranceEmployerPercent : 0);
        var pensionSecondPillar = value * (salaryCalculationOptions.UsePensionSecondPillar ? FundedPensionSecondPillarPercent : 0);
        var unemploymentInsuranceEmployee =
            value * (salaryCalculationOptions.UseUnemploymentInsuranceEmployee ? UnemploymentInsuranceEmployeePercent : 0);
        var incomeTax = CalculateIncomeTax(value, pensionSecondPillar, unemploymentInsuranceEmployee, salaryCalculationOptions,
            out var taxFree);
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

    private static decimal CalculateGrossFromWageFund(decimal value, bool useUnemploymentInsuranceEmployer)
    {
        return value / (1 + SocialTaxPercent + (useUnemploymentInsuranceEmployer ? UnemploymentInsuranceEmployerPercent : 0));
    }

    private static decimal CalculateIncomeTax(
        decimal value,
        decimal pensionSecondPillar,
        decimal unemploymentInsuranceEmployee,
        SalaryCalculationOptions salaryCalculationOptions,
        out decimal taxFree)
    {
        if (!salaryCalculationOptions.UseTaxFree)
        {
            taxFree = 0;
            return value * IncomeTaxPercent;
        }

        if (value - TaxFreeMonthlyMax - pensionSecondPillar - unemploymentInsuranceEmployee <= 0)
        {
            taxFree = TaxFreeMonthlyMax;
            if (value < TaxFreeMonthlyMax)
                taxFree = value - pensionSecondPillar - unemploymentInsuranceEmployee;
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

    public static SalaryCalculationYearlyResult CalculateYearly(SalaryCalculationBaseValues baseValues,
        SalaryCalculationOptions salaryCalculationOptions)
    {
        var monthly = Calculate(baseValues, salaryCalculationOptions);
        return new SalaryCalculationYearlyResult
        {
            AnnualWageFund = monthly.WageFund * 12,
            AnnualRevenueGross = monthly.GrossSalary * 12,
            AnnualRevenueNet = monthly.NetSalary * 12
        };
    }
}