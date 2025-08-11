using Domain.Repositories;
using MediatR;
using static Domain.Entities.Employee;

namespace Application.Features.Queries.GetTeamsWithoutVacationsByYear;

public sealed class GetTeamsWithoutVacationsByYearQueryHandler 
    : IRequestHandler<GetTeamsWithoutVacationsByYearQuery, GetTeamsWithoutVacationsByYearResponse>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetTeamsWithoutVacationsByYearQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<GetTeamsWithoutVacationsByYearResponse> Handle
    (
        GetTeamsWithoutVacationsByYearQuery query, 
        CancellationToken cancellationToken
    )
    {
        EnsureYearIsNotToEarly(query.Year);

        var allEmployees = await _employeeRepository.GetAllEmployeesWithTeamsAndVacationsAsync(cancellationToken);

        var teamsGrouped = allEmployees
            .Where(e => e.Team != null)
            .GroupBy(e => e.Team!)
            .ToList();

        var teamsWithoutVacationsInYear = teamsGrouped
            .Where(teamGroup => !teamGroup.Any(employee => 
                employee.Vacations.Any(v => v.DateSince.Year == query.Year || v.DateUntil.Year == query.Year)))
            .Select(teamGroup => new TeamDto(
                teamGroup.Key.TeamId,
                teamGroup.Key.Name,
                teamGroup.Count()))
            .ToList();

        return new GetTeamsWithoutVacationsByYearResponse(query.Year, teamsWithoutVacationsInYear);
    }
}