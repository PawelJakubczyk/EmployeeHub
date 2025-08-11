using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmployeeHubDbContext _context;

    public EmployeeRepository(EmployeeHubDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync(CancellationToken cancellationToken)
    {
        return await _context.Employees
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Employee>> GetAllEmployeesWithTeamsAsync(CancellationToken cancellationToken)
    {
        return await _context.Employees
            .Include(e => e.Team)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<EmployeeWithUsedDays>> GetAllEmployeesWithVacationsAsync
    (
        DateTime day,
        int year,
        CancellationToken cancellationToken
    )
    {
        var utcDay = day.Kind == DateTimeKind.Utc ? day : day.ToUniversalTime();
        
        return await _context.Employees
            .Include(e => e.Vacations)
            .Select(e => new EmployeeWithUsedDays
            (
                e.EmployeeId,
                e.Name,
                e.Team!.Name,
                e.Vacations
                    .Where(v => (v.DateSince.Year == year || v.DateUntil.Year == year) && v.DateUntil < utcDay)
                    .Sum(v => (v.DateUntil - v.DateSince).Days + 1))
            )
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Employee>> GetAllEmployeesWithTeamsAndVacationsAsync(CancellationToken cancellationToken)
    {
        return await _context.Employees
            .Include(e => e.Team)
            .Include(e => e.Vacations)
            .ToListAsync(cancellationToken);
    }

    public async Task<Employee?> GetEmployeeByIdWithVacationPackageAndVacationsAsync(int employeeId, CancellationToken cancellationToken)
    {
        return await _context.Employees
            .Include(e => e.VacationPackage)
            .Include(e => e.Vacations)
            .FirstOrDefaultAsync(e => e.EmployeeId == employeeId, cancellationToken);
    }
}