namespace Application.Features.Queries.GetTeamEmployeesWithVacationsByYear;

public record GetEmployeesWithVacationsByYearAndTeamResponse
(
    string TeamName, 
    int Year, 
    List<EmployeeWithVacationsDto> Employees
);

public record EmployeeWithVacationsDto
(
    int Id,
    string Name,
    string TeamName,
    List<VacationDto> Vacations
);

public record VacationDto
(
    int VacationId,
    DateTime DateSince,
    DateTime DateUntil,
    int NumberOfHours,
    bool IsPartVacation
);