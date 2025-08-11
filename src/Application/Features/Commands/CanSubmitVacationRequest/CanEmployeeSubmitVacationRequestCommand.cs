using MediatR;

namespace Application.Features.Commands.CanSubmitVacationRequest;

public record CanEmployeeSubmitVacationRequestCommand
(
    int EmployeeId, 
    int RequestedDays, 
    int? Year = null
) : IRequest<CanEmployeeSubmitVacationRequestResponse>;