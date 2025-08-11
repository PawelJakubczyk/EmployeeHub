using MediatR;

namespace Application.Features.Commands.CalculateRemainingVacationDays;

public record CalculateEmployeeRemainingVacationDaysCommand(int EmployeeId, int? Year = null) 
    : IRequest<CalculateEmployeeRemainingVacationDaysResponse>;