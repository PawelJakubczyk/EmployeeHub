using Domain.ValueObjects;

namespace Application.Features.Commands.CalculateRemainingVacationDays;

public record CalculateEmployeeRemainingVacationDaysResponse
(
    int EmployeeId,
    string EmployeeName,
    int Year,
    int GrantedDays,
    int UsedDays,
    int RemainingDays,
    List<VacationUsage> UsedVacations
);