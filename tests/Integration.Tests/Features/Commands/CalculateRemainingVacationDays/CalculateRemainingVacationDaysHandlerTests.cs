using Application.Features.Commands.CalculateRemainingVacationDays;
using Domain.Entities;
using Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Integration.Tests.Features.Commands.CalculateRemainingVacationDays;

public class CalculateRemainingVacationDaysHandlerTests
{
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly CalculateEmployeeRemainingVacationDaysCommandHandler _handler;

    private readonly Employee employee1 = new(1, "John Doe", 1)
    {
        VacationPackage = new VacationPackage(1, "SummerItaly", 14, 2024),
        Vacations =
        [
            new Vacation
            (
                vacationId: 1,
                dateSince: new DateTime(2024, 1, 1),
                dateUntil: new DateTime(2024, 1, 5),
                numberOfHours: 40,
                isPartVacation: false
            ),
            new Vacation
            (
                vacationId: 2,
                dateSince: new DateTime(2024, 2, 1),
                dateUntil: new DateTime(2024, 2, 3),
                numberOfHours: 0,
                isPartVacation: false
            )
        ]
    };

    public CalculateRemainingVacationDaysHandlerTests()
    {
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _handler = new CalculateEmployeeRemainingVacationDaysCommandHandler(_employeeRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidEmployee_ShouldCalculateRemainingDaysCorrectly()
    {
        // Arrange
        var command = new CalculateEmployeeRemainingVacationDaysCommand(1, 2024);
        
        _employeeRepositoryMock
            .Setup(repository => repository.GetEmployeeByIdWithVacationPackageAndVacationsAsync(1, CancellationToken.None))
            .ReturnsAsync(employee1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.EmployeeId.Should().Be(1);
        result.EmployeeName.Should().Be("John Doe");
        result.Year.Should().Be(2024);
        result.GrantedDays.Should().Be(20);
        result.UsedDays.Should().Be(8); // 5 days + 3 days
        result.RemainingDays.Should().Be(12); // 20 - 8
        result.UsedVacations.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_WithNonExistentEmployee_ShouldThrowException()
    {
        // Arrange
        var command = new CalculateEmployeeRemainingVacationDaysCommand(999, 2024);
        _employeeRepositoryMock
            .Setup(x => x.GetEmployeeByIdWithVacationPackageAndVacationsAsync(999, CancellationToken.None))
            .ReturnsAsync((Employee?)null);

        // Act & Assert
        var act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Employee with ID 999 not found.");
    }

    [Fact]
    public async Task Handle_WithNoVacationPackage_ShouldThrowException()
    {
        // Arrange
        var employee = new Employee(1, "John Doe", 1) { VacationPackage = null };
        var command = new CalculateEmployeeRemainingVacationDaysCommand(1, 2024);
        _employeeRepositoryMock
            .Setup(x => x.GetEmployeeByIdWithVacationPackageAndVacationsAsync(1, CancellationToken.None))
            .ReturnsAsync(employee);

        // Act & Assert
        var act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Employee with ID 1 has no vacation package assigned.");
    }

    [Fact]
    public async Task Handle_WithWrongYearPackage_ShouldThrowException()
    {
        // Arrange
        var command = new CalculateEmployeeRemainingVacationDaysCommand(1, 2022);
        _employeeRepositoryMock
            .Setup(x => x.GetEmployeeByIdWithVacationPackageAndVacationsAsync(1, CancellationToken.None))
            .ReturnsAsync(employee1);

        // Act & Assert
        var act = async () => await _handler.Handle(command, CancellationToken.None);
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Employee's vacation package is for year 2023, not 2024.");
    }

    [Fact]
    public async Task Handle_WithCrossYearVacation_ShouldCalculateCorrectly()
    {
        // Arrange
        var command = new CalculateEmployeeRemainingVacationDaysCommand(1, 2024);
        _employeeRepositoryMock
            .Setup(x => x.GetEmployeeByIdWithVacationPackageAndVacationsAsync(1, CancellationToken.None))
            .ReturnsAsync(employee1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.UsedDays.Should().Be(8);
        result.RemainingDays.Should().Be(3);
    }
}