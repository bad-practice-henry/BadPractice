#region

using Infrastructure.HttpClient;
using Infrastructure.Services;
using Moq;

#endregion

namespace Infrastructure.UnitTests;

public class SalaryCalculationServiceTestsBase
{
    protected readonly SalaryCalculationService SalaryCalculationService;

    protected SalaryCalculationServiceTestsBase()
    {
        var httpClient = new System.Net.Http.HttpClient(new Mock<HttpMessageHandler>().Object);
        var localDataHttpClient = new LocalDataHttpClient(httpClient);

        SalaryCalculationService = new SalaryCalculationService(localDataHttpClient);
    }
}