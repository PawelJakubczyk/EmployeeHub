using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities;

public class Employee
{
    public int EmployeeId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int TeamId { get; set; }
    public int? SuperiorId { get; set; }
    public int VacationPackageId { get; set; }

    // Navigation properties
    public Employee? Superior { get; set; }
    public Team? Team { get; set; }
    public List<Employee> Subordinates { get; set; } = [];
    public VacationPackage? VacationPackage { get; set; } = null;
    public List<Vacation> Vacations { get; set; } = [];

    public Employee(int id, string name, int teamId, int? superiorId = null)
    {
        EmployeeId = id;
        Name = name;
        TeamId = teamId;
        SuperiorId = superiorId;
    }

    private Employee()
    {
        // Parameterless constructor for EF Core
    }

    public VacationsSummary CalculateVacationSummary(int targetYear)
    {
        List<VacationUsage> usedVacationDtos = [];
        int totalUsedDays = 0;

        var usedVacationsInYear = Vacations
            .Where(v => v.DateSince.Year == targetYear || v.DateUntil.Year == targetYear);

        foreach (var vacation in usedVacationsInYear)
        {
            var vacationStart = vacation.DateSince.Year < targetYear
                ? new DateTime(targetYear, 1, 1)
                : vacation.DateSince;

            var vacationEnd = vacation.DateUntil.Year > targetYear
                ? new DateTime(targetYear, 12, 31)
                : vacation.DateUntil;

            var daysUsed = (vacationEnd - vacationStart).Days + 1;
            totalUsedDays += daysUsed;

            usedVacationDtos.Add(new VacationUsage
            (
                vacation.VacationId,
                vacation.DateSince,
                vacation.DateUntil,
                daysUsed,
                vacation.IsPartVacation
            ));
        }

        return new VacationsSummary
        (
            usedVacationDtos,
            totalUsedDays
        );
    }

    public void EnsureExists()
    {
        if (this == null)
            throw new EmployeeNotFoundException("Employee not found.");
    }

    public void EnsureHasVacationPackage()
    {
        if (VacationPackage is null)
            throw new VacationPackageNotFoundException($"Employee with ID {EmployeeId} has no vacation package assigned.");
    }

    public void EnsurePackageExistsInTargetYear(int targetYear)
    {
        if (VacationPackage?.Year > targetYear)
        {
            throw new PackageNotExistsInTargetYearException
                (
                    $"Cannot check vacation days for year {targetYear}." +
                    $" Employee's vacation package starts from year {VacationPackage?.Year}."
                );
        }
    }
    
    public static void EnsureYearIsNotToEarly(int? year)
    {
        if (year.HasValue && (year < 2018))
        {
            throw new InvalidYearException($"Year must be after 2018");
        }
    }

    public static void EnsureRequestedDaysAreValid(int requestedDays)
    {
        if (requestedDays <= 0)
            throw new InvalidDomainException("Requested days must be greater than 0");
    }

    public static void EnsureYearIsCurrentOrLater(int year)
    {
        if (year < DateTime.UtcNow.Year)
            throw new InvalidDomainException("Year must be current or later");
    }
}