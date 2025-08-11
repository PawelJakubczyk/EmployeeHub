using Domain.Exceptions;

namespace Domain.Entities;

public class Vacation
{
    public int VacationId { get; set; }
    public DateTime DateSince { get; set; }
    public DateTime DateUntil { get; set; }
    public int NumberOfHours { get; set; }
    public bool IsPartVacation { get; set; }

    // Navigation properties
    public int EmployeeId { get; set; }
    public Employee? Employee { get; set; }

    public Vacation(int vacationId, DateTime dateSince, DateTime dateUntil, int numberOfHours, bool isPartVacation)
    {
        VacationId = vacationId;
        DateSince = dateSince;
        DateUntil = dateUntil;
        NumberOfHours = numberOfHours;
        IsPartVacation = isPartVacation;
    }

    private Vacation()
    {
        // Parameterless constructor for EF Core
    }

    public void ValidateVacationPeriod()
    {
        if (DateSince > DateUntil)
            throw new InvalidVacationDatesException("Start date must be before end date");
    }

    public void EnsureNoOverlappingVacations(List<Vacation> vacations)
    {
        if (vacations == null || vacations.Count == 0)
            return;

        if (vacations.Any(v => v.VacationId != this.VacationId &&
                               DateSince <= v.DateUntil &&
                               DateUntil >= v.DateSince))
        {
            throw new OverlappingVacationException("Vacation dates overlap with existing vacation");
        }
    }
}