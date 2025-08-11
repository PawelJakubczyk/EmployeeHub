using Domain.Repositories;
using MediatR;
using static Domain.Entities.Employee;

namespace Application.Features.Commands.CalculateRemainingVacationDays;

public sealed class CalculateEmployeeRemainingVacationDaysCommandHandler 
    : IRequestHandler<CalculateEmployeeRemainingVacationDaysCommand, CalculateEmployeeRemainingVacationDaysResponse>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CalculateEmployeeRemainingVacationDaysCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<CalculateEmployeeRemainingVacationDaysResponse> Handle
    (
        CalculateEmployeeRemainingVacationDaysCommand command, 
        CancellationToken cancellationToken
    )
    {
        var employee = await _employeeRepository.GetEmployeeByIdWithVacationPackageAndVacationsAsync(command.EmployeeId, cancellationToken);

        employee?.EnsureExists();
        employee!.EnsureHasVacationPackage();

        var targetYear = command.Year ?? DateTime.UtcNow.Year;
        EnsureYearIsNotToEarly(targetYear);

        employee.EnsurePackageExistsInTargetYear(targetYear);

        var vacationSummary = employee.CalculateVacationSummary(targetYear);
        var remainingDays = employee!.VacationPackage!.CalculateRemainingDays(vacationSummary.TotalUsedDays);

        return new CalculateEmployeeRemainingVacationDaysResponse
        (
            employee.EmployeeId,
            employee.Name,
            targetYear,
            employee.VacationPackage.GrantedDays,
            vacationSummary.TotalUsedDays,
            remainingDays,
            vacationSummary.VacationUsages
        );
    }
}