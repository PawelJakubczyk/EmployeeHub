namespace Domain.ValueObjects;

public record VacationUsage
(
    int VacationId,
    DateTime DateSince,
    DateTime DateUntil,
    int UsedDays,
    bool IsPartVacation
);