using Domain.Repositories;
using MediatR;

namespace Application.Features.Queries.GetEmployeesWithUsedVacationDays;

public sealed class GetEmployeesUsedVacationDaysQueryHandler 
    : IRequestHandler<GetEmployeesUsedVacationDaysQuery, GetEmployeesUsedVacationDaysResponse>
{
    private readonly IEmployeeRepository _emplyeeRepository;

    public GetEmployeesUsedVacationDaysQueryHandler(IEmployeeRepository employeeRepository)
    {
        _emplyeeRepository = employeeRepository;
    }

    public async Task<GetEmployeesUsedVacationDaysResponse> Handle
    (
        GetEmployeesUsedVacationDaysQuery query, 
        CancellationToken cancellationToken
    )
    {
        var currentYear = DateTime.Now.Year;
        var today = DateTime.UtcNow.Date;

        var employees = await _emplyeeRepository.GetAllEmployeesWithVacationsAsync(today, currentYear, cancellationToken);
        return new GetEmployeesUsedVacationDaysResponse(employees);
    }
}