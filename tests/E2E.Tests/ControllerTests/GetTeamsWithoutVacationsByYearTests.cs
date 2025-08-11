using App;
using Application.Features.Queries.GetTeamsWithoutVacationsByYear;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace E2E.Tests.ControllerTests;

public class GetTeamsWithoutVacationsByYearTests : E2ETestBase
{
    public GetTeamsWithoutVacationsByYearTests(WebApplicationFactory<IApiMarker> factory)
        : base(factory) { }


    [Fact]
    public async Task GetTeamsWithoutVacationsByYear_WithValidYear_ShouldReturnTeamsWithoutVacations()
    {
        var response = await _client.GetAsync("/api/employees/teams-without-vacations?year=2024");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var result = await response.Content.ReadFromJsonAsync<GetTeamsWithoutVacationsByYearResponse>();
        result.Should().NotBeNull();
        result!.Year.Should().Be(2024);
        result.Teams.Should().NotBeEmpty();

        result.Teams.Should().Contain(t => t.TeamName == "QA");
    }

    [Fact]
    public async Task GetTeamsWithoutVacationsByYear_WithTooEarlyYear_ShouldReturnBadRequest()
    {
        var response = await _client.GetAsync("/api/employees/teams-without-vacations?year=1999");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var error = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        error.Should().NotBeNull();
        error!.Detail.Should().Contain("Year must be after 2018");
    }

    [Fact]
    public async Task GetTeamsWithoutVacationsByYear_ResponseStructure_ShouldBeValid()
    {
        var response = await _client.GetAsync("/api/employees/teams-without-vacations?year=2024");
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("year");
        content.Should().Contain("teams");
        content.Should().Contain("teamId");
        content.Should().Contain("teamName");
        content.Should().Contain("employeeCount");
    }

}
