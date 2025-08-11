using Domain.Entities;
using Domain.Services;
using FluentAssertions;

namespace Unit.Tests.Domain.Services;

public class EmployeeStructureTests
{
    private static readonly List<Employee> _employees =
    [
        new(1, "CEO", 1, null),
        new(2, "VP", 1, 1),
        new(3, "Director", 1, 2),
        new(4, "Manager", 1, 3),
        new(5, "TeamLead", 1, 4),
        new(6, "Developer", 1, 5)
    ];

    [Fact]
    public void FillEmployeesStructure_WithValidHierarchy_ShouldCreateCorrectStructure()
    {
        // Arrange and Act
        EmployeeStructure.FillEmployeesStructure(_employees);
        var result = EmployeeStructure.GetStructure();

        // Assert
        result.Should().HaveCount(6);
        result[1].Should().BeEmpty(); // CEO has no superiors
        result[2].Should().ContainKey(1).WhoseValue.Should().Be(1); // VP reports to CEO (level 1)
        result[3].Should().ContainKey(1).WhoseValue.Should().Be(2); // Director reports to VP (level 1)
        result[3].Should().ContainKey(2).WhoseValue.Should().Be(1); // Director reports to CEO (level 2)
        result[6].Should().ContainKey(1).WhoseValue.Should().Be(5); // Developer reports to TeamLead (level 1)
        result[6].Should().ContainKey(5).WhoseValue.Should().Be(1); // Developer reports to CEO (level 5)
    }

    [Fact]
    public void GetSuperiorRowOfEmployee_WithValidHierarchy_ShouldReturnCorrectLevel()
    {
        // Arrange
        EmployeeStructure.FillEmployeesStructure(_employees);

        // Act & Assert
        EmployeeStructure.GetSuperiorRowOfEmployee(6, 5).Should().Be(1); // Developer -> TeamLead (direct superior)
        EmployeeStructure.GetSuperiorRowOfEmployee(6, 4).Should().Be(2); // Developer -> Manager (2nd level)
        EmployeeStructure.GetSuperiorRowOfEmployee(6, 3).Should().Be(3); // Developer -> Director (3rd level)
        EmployeeStructure.GetSuperiorRowOfEmployee(6, 2).Should().Be(4); // Developer -> VP (4th level)
        EmployeeStructure.GetSuperiorRowOfEmployee(6, 1).Should().Be(5); // Developer -> CEO (5th level)
        EmployeeStructure.GetSuperiorRowOfEmployee(5, 4).Should().Be(1); // TeamLead -> Manager (direct superior)
        EmployeeStructure.GetSuperiorRowOfEmployee(3, 1).Should().Be(2); // Director -> CEO (2nd level)
    }

    [Fact]
    public void GetSuperiorRowOfEmployee_WithInvalidRelationship_ShouldReturnNull()
    {
        // Arrange
        EmployeeStructure.FillEmployeesStructure(_employees);

        // Act & Assert
        EmployeeStructure.GetSuperiorRowOfEmployee(1, 2).Should().BeNull(); // CEO doesn't report to VP
        EmployeeStructure.GetSuperiorRowOfEmployee(2, 3).Should().BeNull(); // VP doesn't report to Director
        EmployeeStructure.GetSuperiorRowOfEmployee(5, 6).Should().BeNull(); // TeamLead doesn't report to Developer
    }

    [Fact]
    public void GetSuperiorRowOfEmployee_WithNonExistentEmployee_ShouldThrowArgumentException()
    {
        // Arrange
        EmployeeStructure.FillEmployeesStructure(_employees);

        // Act & Assert
        var action1 = () => EmployeeStructure.GetSuperiorRowOfEmployee(999, 1);
        action1.Should().Throw<ArgumentException>()
            .WithMessage("employeeId with ID 999 does not exist in the employee structure.");

        var action2 = () => EmployeeStructure.GetSuperiorRowOfEmployee(1, 999);
        action2.Should().Throw<ArgumentException>()
            .WithMessage("superiorId with ID 999 does not exist in the employee structure.");
    }

    [Fact]
    public void Constructor_WithCircularReference_ShouldThrowException()
    {
        // Arrange
        var circularEmployees = new List<Employee>
        {
            new(1, "Employee1", 1, 2),
            new(2, "Employee2", 1, 3),
            new(3, "Employee3", 1, 1) // Creates circular reference
        };

        // Act & Assert
        var action = () => new EmployeeStructure(circularEmployees);
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Circular reference detected in employee hierarchy starting from employee ID: *");
    }

    [Fact]
    public void Constructor_WithSelfReference_ShouldThrowException()
    {
        // Arrange
        var selfReferenceEmployees = new List<Employee>
        {
            new(1, "SelfReference", 1, 1) // Employee is their own superior
        };

        // Act & Assert
        var action = () => new EmployeeStructure(selfReferenceEmployees);
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Circular reference detected in employee hierarchy starting from employee ID: 1");
    }

    [Fact]
    public void Constructor_WithComplexCircularReference_ShouldThrowException()
    {
        // Arrange
        var complexCircularEmployees = new List<Employee>
        {
            new(1, "CEO", 1, null),
            new(2, "VP", 1, 1),
            new(3, "Manager", 1, 2),
            new(4, "TeamLead", 1, 3),
            new(5, "Developer", 1, 4),
            new(6, "Junior", 1, 2), // Valid path
            new(7, "Circular1", 1, 8),
            new(8, "Circular2", 1, 9),
            new(9, "Circular3", 1, 7) // Creates circular reference
        };

        // Act & Assert
        var action = () => new EmployeeStructure(complexCircularEmployees);
        action.Should().Throw<InvalidOperationException>()
            .WithMessage("Circular reference detected in employee hierarchy starting from employee ID: *");
    }

    [Fact]
    public void Constructor_WithValidMultipleBranches_ShouldSucceed()
    {
        // Arrange
        var multiBranchEmployees = new List<Employee>
        {
            new(1, "CEO", 1, null),
            new(2, "VP1", 1, 1),
            new(3, "VP2", 1, 1),
            new(4, "Manager1", 1, 2),
            new(5, "Manager2", 1, 3),
            new(6, "Developer1", 1, 4),
            new(7, "Developer2", 1, 5)
        };

        // Act & Assert
        var action = () => new EmployeeStructure(multiBranchEmployees);
        action.Should().NotThrow();
        
        EmployeeStructure.FillEmployeesStructure(multiBranchEmployees);
        EmployeeStructure.GetSuperiorRowOfEmployee(6, 1).Should().Be(3); // Developer1 -> CEO (3rd level)
        EmployeeStructure.GetSuperiorRowOfEmployee(7, 1).Should().Be(3); // Developer2 -> CEO (3rd level)
    }

    [Fact]
    public void Constructor_WithOrphanedEmployees_ShouldHandleGracefully()
    {
        // Arrange
        var orphanedEmployees = new List<Employee>
        {
            new(1, "CEO", 1, null),
            new(2, "Manager", 1, 1),
            new(3, "Temporary Employee", 1, 999) // Superior doesn't exist
        };

        // Act & Assert
        var action = () => new EmployeeStructure(orphanedEmployees);
        action.Should().NotThrow();
        
        EmployeeStructure.FillEmployeesStructure(orphanedEmployees);
        var result = EmployeeStructure.GetStructure();
        result[3].Should().BeEmpty(); // employee has no valid superiors
    }
}