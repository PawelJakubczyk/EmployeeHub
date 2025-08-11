using Domain.Exceptions;

namespace Domain.Entities;

public class VacationPackage
{
    public int VacationPackageId { get; set; }
    public string VacationPackageName { get; set; } = string.Empty;
    public int GrantedDays { get; set; }
    public int Year { get; set; }
    
    public List<Employee> Employees { get; set; } = [];

    public VacationPackage(int vacationPackageId, string vacationPackageName, int grantedDays, int year)
    {
        VacationPackageId = vacationPackageId;
        VacationPackageName = vacationPackageName;
        GrantedDays = grantedDays;
        Year = year;
    }

    private VacationPackage()
    {
        // Parameterless constructor for EF Core
    }

    public int CalculateRemainingDays(int usedDays)
    {
        return GrantedDays - usedDays;
    }
}