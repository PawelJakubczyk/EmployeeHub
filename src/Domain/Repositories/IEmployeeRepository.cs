using Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Repositories;

public interface IEmployeeRepository
{
    Task<List<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken);
    Task<List<Employee>> GetAllEmployeesWithTeamsAsync(CancellationToken cancellationToken);
    Task<List<EmployeeWithUsedDays>> GetAllEmployeesWithVacationsAsync(DateTime day, int year, CancellationToken cancellationToken);
    Task<List<Employee>> GetAllEmployeesWithTeamsAndVacationsAsync(CancellationToken cancellationToken);
    Task<Employee?> GetEmployeeByIdWithVacationPackageAndVacationsAsync(int employeeId, CancellationToken cancellationToken);
}
