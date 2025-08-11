using Domain.Repositories;
using MediatR;
using static Domain.Entities.Employee;

namespace Application.Features.Commands.CanSubmitVacationRequest;

public sealed class CanEmployeeSubmitVacationRequestCommandHandler 
    : IRequestHandler<CanEmployeeSubmitVacationRequestCommand, CanEmployeeSubmitVacationRequestResponse>
{
    private readonly IEmployeeRepository _employeeRepository;

    public CanEmployeeSubmitVacationRequestCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<CanEmployeeSubmitVacationRequestResponse> Handle
    (
        CanEmployeeSubmitVacationRequestCommand command, 
        CancellationToken cancellationToken
    )
    {
        EnsureRequestedDaysAreValid(command.RequestedDays);
        var targetYear = command.Year ?? DateTime.UtcNow.Year;
        EnsureYearIsCurrentOrLater(targetYear);
        EnsureYearIsNotToEarly(targetYear);

        var employee = await _employeeRepository.GetEmployeeByIdWithVacationPackageAndVacationsAsync(command.EmployeeId, cancellationToken);

        employee?.EnsureExists();
        employee!.EnsureHasVacationPackage();

        employee.EnsurePackageExistsInTargetYear(targetYear);

        var vacationSummary = employee.CalculateVacationSummary(targetYear);
        var remainingDays = employee!.VacationPackage!.CalculateRemainingDays(vacationSummary.TotalUsedDays);
        var canSubmit = remainingDays >= command.RequestedDays;

        var message = canSubmit 
            ? "A leave application may be submitted."
            : $"Insufficient vacation days. Available: {remainingDays}, required: {command.RequestedDays}.";

        return new CanEmployeeSubmitVacationRequestResponse
        (
            employee.EmployeeId,
            employee.Name,
            targetYear,
            command.RequestedDays,
            remainingDays,
            canSubmit,
            message
        );
    }
}