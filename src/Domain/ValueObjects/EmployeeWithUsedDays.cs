namespace Domain.ValueObjects;

public record EmployeeWithUsedDays
(
    int EmployeeId,
    string EmployeeName,
    string TeamName,
    int UsedVacationDays
);