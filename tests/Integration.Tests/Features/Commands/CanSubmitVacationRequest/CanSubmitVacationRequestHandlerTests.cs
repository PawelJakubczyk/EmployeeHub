using Application.Features.Commands.CanSubmitVacationRequest;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Integration.Tests.Features.Commands.CanSubmitVacationRequest;

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
    public async Task Handle_WithSufficientDays_ShouldReturnCanSubmitTrue()
    {
        // Arrange
        var employeeId = 1;
        var year = 2025;
        var requestedDays = 10;
        
        var employee = new Employee(employeeId, "John Doe", 1)
        {
            VacationPackage = new VacationPackage(1, "Standard 2025", 20, year)
        };

        _employeeRepositoryMock
            .Setup(x => x.GetEmployeeByIdWithVacationPackageAndVacationsAsync(employeeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(employee);

        var command = new CanEmployeeSubmitVacationRequestCommand(employeeId, requestedDays, year);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.EmployeeId.Should().Be(employeeId);
        result.EmployeeName.Should().Be("John Doe");
        result.Year.Should().Be(year);
        result.RequestedDays.Should().Be(requestedDays);
        result.RemainingDays.Should().Be(20); // No vacations used yet
        result.CanSubmit.Should().BeTrue();
        result.Message.Should().Be("A leave application may be submitted.");
    }

    [Fact]
    public async Task Handle_WithInsufficientDays_ShouldReturnCanSubmitFalse()
    {
        // Arrange
        var employeeId = 1;
        var year = 2024;
        var requestedDays = 5;

        var employee = new Employee(employeeId, "John Doe", 1)
        {
            VacationPackage = new VacationPackage(1, "Standard 2024", 20, year),
            Vacations = new List<Vacation>
            {
                new(1, new DateTime(2024, 1, 1), new DateTime(2024, 1, 18), 144, false) // 18 days used
            }
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
        result.RemainingDays.Should().Be(2); // 20 - 18 days used
        result.Message.Should().Be("Insufficient vacation days. Available: 2, required: 5.");
    }

    [Fact]
    public async Task Handle_WithExactlyEnoughDays_ShouldReturnCanSubmitTrue()
    {
        // Arrange
        var employeeId = 1;
        var year = 2024;
        var requestedDays = 5;

        var employee = new Employee(employeeId, "John Doe", 1)
        {
            VacationPackage = new VacationPackage(1, "Standard 2024", 20, year),
            Vacations = new List<Vacation>
            {
                new(1, new DateTime(2024, 1, 1), new DateTime(2024, 1, 15), 120, false) // 15 days used
            }
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
        result.RemainingDays.Should().Be(5); // 20 - 15 days used
        result.Message.Should().Be("A leave application may be submitted.");
    }
}