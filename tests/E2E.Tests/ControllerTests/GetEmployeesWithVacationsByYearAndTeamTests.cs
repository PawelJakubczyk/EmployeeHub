using App;
using Application.Features.Queries.GetTeamEmployeesWithVacationsByYear;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace E2E.Tests.ControllersTests;

public class EmployeesControllerTests : E2ETestBase
{
    public EmployeesControllerTests(WebApplicationFactory<IApiMarker> factory)
        : base(factory) { }

    [Fact]
    public async Task GetEmployeesWithVacationsByYearAndTeam_WithValidTeamAndYear_ShouldReturnEmployeesWithVacations()
    {
        var response = await _client.GetAsync("/api/employees/team?teamName=dotnet&year=2024");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<GetEmployeesWithVacationsByYearAndTeamResponse>();
        result.Should().NotBeNull();
        result!.Employees.Should().NotBeEmpty();
        result.Employees.Should().HaveCount(6);

        result.Year.Should().Be(2024);
    }

    [Fact]
    public async Task GetEmployeesWithVacationsByYearAndTeam_WithTeamWithoutVacations_ShouldReturnEmptyList()
    {
        var response = await _client.GetAsync("/api/employees/team?teamName=QA&year=2024");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<GetEmployeesWithVacationsByYearAndTeamResponse>();
        result.Should().NotBeNull();
        result!.Employees.Should().BeEmpty();
    }

    [Fact]
    public async Task GetEmployeesWithVacationsByYearAndTeam_WithInvalidTeam_ShouldReturnNotFound()
    {
        var response = await _client.GetAsync("/api/employees/team?teamName=nonexistent&year=2024");
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var error = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        error.Should().NotBeNull();
        error!.Detail.Should().Be("Team 'nonexistent' not found.");
    }

    [Fact]
    public async Task GetEmployeesWithVacationsByYearAndTeam_WithValidTeamAndYear_ShouldReturnCorrectVacationDetails()
    {
        var response = await _client.GetAsync("/api/employees/team?teamName=dotnet&year=2024");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<GetEmployeesWithVacationsByYearAndTeamResponse>();
        result.Should().NotBeNull();

        foreach (var employee in result!.Employees)
        {
            employee.Vacations.Should().AllSatisfy(v =>
            {
                v.DateSince.Year.Should().Be(2024);
                v.DateUntil.Year.Should().Be(2024);
            });
        }
    }
}
