using App;
using Application.Features.Queries.GetEmployeesWithUsedVacationDays;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace E2E.Tests.ControllerTests;
public class GetEmployeesUsedVacationDaysTests : E2ETestBase
{
    public GetEmployeesUsedVacationDaysTests(WebApplicationFactory<IApiMarker> factory)
        : base(factory) { }

    [Fact]
    public async Task GetEmployeesUsedVacationDays_ShouldReturnAllEmployeesWithUsedVacationsDays()
    {
        var response = await _client.GetAsync("/api/employees/with-used-vacation-days");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<GetEmployeesUsedVacationDaysResponse>();
        result.Should().NotBeNull();
        result!.Employees.Should().NotBeEmpty();
        result!.Employees.Should().HaveCount(28);

        result.Employees.Should().AllSatisfy(e =>
        {
            e.EmployeeName.Should().NotBeNullOrEmpty();
            e.UsedVacationDays.Should().BeGreaterThanOrEqualTo(0);
        });
    }
}
