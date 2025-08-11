using Domain.Exceptions;
using Domain.Repositories;
using MediatR;
using static Domain.Entities.Employee;

namespace Application.Features.Queries.GetTeamEmployeesWithVacationsByYear;

public sealed class GetEmployeesWithVacationsByYearAndTeamQueryHandler 
    : IRequestHandler<GetEmployeesWithVacationsByYearAndTeamQuery, GetEmployeesWithVacationsByYearAndTeamResponse>
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeesWithVacationsByYearAndTeamQueryHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<GetEmployeesWithVacationsByYearAndTeamResponse> Handle
    (
        GetEmployeesWithVacationsByYearAndTeamQuery query, 
        CancellationToken cancellationToken
    )
    {
        EnsureYearIsNotToEarly(query.Year);

        var allEmployees = await _employeeRepository.GetAllEmployeesWithTeamsAndVacationsAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(query.TeamName))
        {
            throw new InvalidDomainException($"Selected team can not be null or empty.");
        }

        if (!allEmployees.Any(e => e.Team?.Name == query.TeamName))
        {
            throw new NotFoundDomainException($"Team '{query.TeamName}' not found.");
        }

        var filteredEmployees = allEmployees
            .Where(e => e.Team != null && 
                        e.Team.Name.Equals(query.TeamName, StringComparison.OrdinalIgnoreCase) && 
                        e.Vacations.Any(v => v.DateSince.Year == query.Year || v.DateUntil.Year == query.Year))
            .Select(e => new EmployeeWithVacationsDto(
                e.EmployeeId,
                e.Name,
                e.Team!.Name,
                e.Vacations
                    .Where(v => v.DateSince.Year == query.Year || v.DateUntil.Year == query.Year)
                    .Select(v => new VacationDto(
                        v.VacationId,
                        v.DateSince,
                        v.DateUntil,
                        v.NumberOfHours,
                        v.IsPartVacation))
                    .ToList()))
            .ToList();

        return new GetEmployeesWithVacationsByYearAndTeamResponse(query.TeamName, query.Year, filteredEmployees);
    }
}