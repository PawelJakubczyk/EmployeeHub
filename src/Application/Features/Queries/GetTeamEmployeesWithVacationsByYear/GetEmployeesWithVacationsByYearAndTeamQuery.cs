using MediatR;

namespace Application.Features.Queries.GetTeamEmployeesWithVacationsByYear;

public record GetEmployeesWithVacationsByYearAndTeamQuery(string TeamName, int Year) 
    : IRequest<GetEmployeesWithVacationsByYearAndTeamResponse>;