using MediatR;

namespace Application.Features.Queries.GetEmployeesWithUsedVacationDays;

public record GetEmployeesUsedVacationDaysQuery() : IRequest<GetEmployeesUsedVacationDaysResponse>;