namespace Application.Features.Commands.CanSubmitVacationRequest;

public record CanEmployeeSubmitVacationRequestResponse
(
    int EmployeeId,
    string EmployeeName,
    int Year,
    int RequestedDays,
    int RemainingDays,
    bool CanSubmit,
    string Message
);