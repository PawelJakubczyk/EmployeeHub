namespace Application.Features.Queries.GetTeamsWithoutVacationsByYear;

public record GetTeamsWithoutVacationsByYearResponse(int Year, List<TeamDto> Teams);

public record TeamDto
(
    int TeamId,
    string TeamName,
    int EmployeeCount
);