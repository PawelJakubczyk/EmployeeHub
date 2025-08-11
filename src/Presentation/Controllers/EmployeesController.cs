using Application.Features.Commands.CalculateRemainingVacationDays;
using Application.Features.Commands.CanSubmitVacationRequest;
using Application.Features.Queries.GetEmployeesWithUsedVacationDays;
using Application.Features.Queries.GetTeamEmployeesWithVacationsByYear;
using Application.Features.Queries.GetTeamsWithoutVacationsByYear;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly ISender _sender;

    public EmployeesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("team")]
    public async Task<ActionResult<GetEmployeesWithVacationsByYearAndTeamResponse>> GetEmployeesWithVacationsByYearAndTeam(
        [FromQuery] string teamName,
        [FromQuery] int year,
        CancellationToken cancellationToken)
    {
        var query = new GetEmployeesWithVacationsByYearAndTeamQuery(teamName, year);
        var response = await _sender.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet("with-used-vacation-days")]
    public async Task<ActionResult<GetEmployeesUsedVacationDaysResponse>> GetEmployeesUsedVacationDays(CancellationToken cancellationToken)
    {
        var query = new GetEmployeesUsedVacationDaysQuery();
        var response = await _sender.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet("teams-without-vacations")]
    public async Task<ActionResult<GetTeamsWithoutVacationsByYearResponse>> GetTeamsWithoutVacationsByYear(
        [FromQuery] int year,
        CancellationToken cancellationToken)
    {
        var query = new GetTeamsWithoutVacationsByYearQuery(year);
        var response = await _sender.Send(query, cancellationToken);
        return Ok(response);
    }

    [HttpGet("{employeeId}/remaining-vacation-days")]
    public async Task<ActionResult<CalculateEmployeeRemainingVacationDaysResponse>> GetRemainingVacationDays(
        int employeeId,
        CancellationToken cancellationToken,
        [FromQuery] int? year = null)
    {
        var command = new CalculateEmployeeRemainingVacationDaysCommand(employeeId, year);
        var response = await _sender.Send(command, cancellationToken);
        return Ok(response);
    }

    [HttpPost("{employeeId}/can-submit-vacation-request")]
    public async Task<ActionResult<CanEmployeeSubmitVacationRequestResponse>> CanSubmitVacationRequest(
        int employeeId,
        [FromBody] VacationCheckModelRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CanEmployeeSubmitVacationRequestCommand(employeeId, request.RequestedDays, request.Year);
        var response = await _sender.Send(command, cancellationToken);
        return Ok(response);
    }

    public record VacationCheckModelRequest(int Year, int RequestedDays);

}