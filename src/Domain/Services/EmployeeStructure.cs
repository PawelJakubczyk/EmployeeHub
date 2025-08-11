using Domain.Entities;

namespace Domain.Services;

// Task 1: In the implemented system, we store information about the structure of employees.  
// Each of them can have an assigned supervisor. The following class illustrates the current state.  
// For optimization purposes, you have been assigned the task of creating a structure  
// that stores information about all levels of supervision.  

// The proposed solution should allow determining whether a given employee  
// is a supervisor of any rank for another employee, as well as storing the number of that rank.  
// Add the appropriate classes and a method to populate them based on the list of all employees:

public class EmployeeStructure
{
    private static Dictionary<int, Dictionary<int, int>> _employees = [];

    public EmployeeStructure(List<Employee> employees)
    {
        ValidateHierarchyForLoops(employees);
        
        var employeeDict = employees.ToDictionary(e => e.EmployeeId);
        var result = new Dictionary<int, Dictionary<int, int>>();

        foreach (var employee in employees)
        {
            var path = new Dictionary<int, int>();
            var current = employee;
            int level = 1;

            while (current.SuperiorId.HasValue && employeeDict.TryGetValue(current.SuperiorId.Value, out var superior))
            {
                path[level] = superior.EmployeeId;
                current = superior;
                level++;
            }

            result[employee.EmployeeId] = path;
        }

        _employees = result;
    }

    public static void FillEmployeesStructure(List<Employee> employees)
    {
        _ = new EmployeeStructure(employees);
    }

    public static Dictionary<int, Dictionary<int, int>> GetStructure() => _employees;

    private static void ValidateHierarchyForLoops(List<Employee> employees)
    {
        var employeeDict = employees.ToDictionary(e => e.EmployeeId);
        var visited = new HashSet<int>();
        var currentPath = new HashSet<int>();

        foreach (var employee in employees)
        {
            if (!visited.Contains(employee.EmployeeId))
            {
                if (HasCircularReference(employee.EmployeeId, employeeDict, visited, currentPath))
                {
                    throw new InvalidOperationException($"Circular reference detected in employee hierarchy starting from employee ID: {employee.EmployeeId}");
                }
            }
        }
    }
    
    public static int? GetSuperiorRowOfEmployee(int employeeId, int superiorId)
    {
        if (!_employees.ContainsKey(employeeId))
        {
            throw new ArgumentException($"{nameof(employeeId)} with ID {employeeId} does not exist in the employee structure.");
        }

        if (!_employees.ContainsKey(superiorId))
        {
            throw new ArgumentException($"{nameof(superiorId)} with ID {superiorId} does not exist in the employee structure.");
        }

        if (_employees.TryGetValue(employeeId, out var superiorsWithLevels))
        {
            foreach (var (level, superiorIdInHierarchy) in superiorsWithLevels)
            {
                if (superiorIdInHierarchy == superiorId)
                    return level;
            }
        }
        return null;
    }

    private static bool HasCircularReference(int employeeId, Dictionary<int, Employee> employeeDict, HashSet<int> visited, HashSet<int> currentPath)
    {
        if (currentPath.Contains(employeeId))
        {
            return true; // Circular reference found
        }

        if (visited.Contains(employeeId))
        {
            return false; // Already processed this path
        }

        visited.Add(employeeId);
        currentPath.Add(employeeId);

        if (employeeDict.TryGetValue(employeeId, out var employee) && employee.SuperiorId.HasValue)
        {
            if (HasCircularReference(employee.SuperiorId.Value, employeeDict, visited, currentPath))
            {
                return true;
            }
        }

        currentPath.Remove(employeeId);
        return false;
    }
}