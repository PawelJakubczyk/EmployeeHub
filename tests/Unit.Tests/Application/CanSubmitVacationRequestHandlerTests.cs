using Application.Features.Commands.CanSubmitVacationRequest;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Unit.Tests.Application;

// Task 5. Write two simple unit tests that verify the operation of the method implemented in task number 4.

// [Test]
// public void employee_can_request_vacation()
// {
// }

// [Test]
// public void employee_cant_request_vacation()
// {
// }

public class CanSubmitVacationRequestHandlerTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly CanEmployeeSubmitVacationRequestCommandHandler _handler;

    public CanSubmitVacationRequestHandlerTests()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _handler = new CanEmployeeSubmitVacationRequestCommandHandler(_employeeRepositoryMock.Object);
    }

    [Fact]
    public async Task CanSubmitVacationRequest_WhenEmployeeHasEnoughDays_ShouldReturnSuccessResponse()
    {
        // Arrange
        var employeeId = 1;
        var year = 2025;
        var requestedDays = 5;

        var employee = new Employee(employeeId, "John Doe", 1)
        {
            VacationPackage = new VacationPackage(1, "Standard 2025", 20, 2025)
        };

        _employeeRepositoryMock
            .Setup(x => x.GetEmployeeByIdWithVacationPackageAndVacationsAsync(employeeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(employee);

        var command = new CanEmployeeSubmitVacationRequestCommand(employeeId, requestedDays, year);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.CanSubmit.Should().BeTrue();
        result.EmployeeId.Should().Be(employeeId);
        result.Year.Should().Be(year);
        result.RequestedDays.Should().Be(requestedDays);
        result.Message.Should().Be("A leave application may be submitted.");
    }

    [Fact]
    public async Task CanSubmitVacationRequest_WhenEmployeeHasInsufficientDays_ShouldReturnFailureResponse()
    {
        // Arrange
        var employeeId = 1;
        var year = 2025;
        var requestedDays = 25; // More than available

        var employee = new Employee(employeeId, "John Doe", 1)
        {
            VacationPackage = new VacationPackage(1, "Standard 2025", 20, 2025)
        };

        _employeeRepositoryMock
            .Setup(x => x.GetEmployeeByIdWithVacationPackageAndVacationsAsync(employeeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(employee);

        var command = new CanEmployeeSubmitVacationRequestCommand(employeeId, requestedDays, year);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.CanSubmit.Should().BeFalse();
        result.EmployeeId.Should().Be(employeeId);
        result.Year.Should().Be(year);
        result.RequestedDays.Should().Be(requestedDays);
        result.Message.Should().Be("Insufficient vacation days. Available: 20, required: 25.");
    }
}