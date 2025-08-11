using Domain.ValueObjects;

namespace Application.Features.Queries.GetEmployeesWithUsedVacationDays;

public record GetEmployeesUsedVacationDaysResponse(List<EmployeeWithUsedDays> Employees);

