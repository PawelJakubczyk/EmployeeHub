namespace Domain.ValueObjects;

public record VacationsSummary(List<VacationUsage> VacationUsages, int TotalUsedDays);
