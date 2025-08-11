using MediatR;

namespace Application.Features.Queries.GetTeamsWithoutVacationsByYear;

public record GetTeamsWithoutVacationsByYearQuery(int Year) : IRequest<GetTeamsWithoutVacationsByYearResponse>;