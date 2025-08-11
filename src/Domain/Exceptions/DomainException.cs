namespace Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message)
    {
        
    }
}

public class NotFoundDomainException : DomainException
{
    public NotFoundDomainException(string message) : base(message)
    {

    }
}

public class InvalidDomainException : DomainException
{
    public InvalidDomainException(string message) : base(message)
    {

    }
}

public class EmployeeNotFoundException : NotFoundDomainException
{
    public EmployeeNotFoundException(string message) : base(message)
    {
        
    }
}

public class VacationPackageNotFoundException : NotFoundDomainException
{
    public VacationPackageNotFoundException(string message) : base(message)
    {
        
    }
}

public class InvalidVacationPackageException : InvalidDomainException
{
    public InvalidVacationPackageException(string message) : base(message)
    {
        
    }
}

public class VacationNotFoundException : NotFoundDomainException
{
    public VacationNotFoundException(string message) : base(message)
    {
        
    }
}

public class VacationRequestNotFoundException : NotFoundDomainException
{
    public VacationRequestNotFoundException(string message) : base(message)
    {
        
    }
}

public class InvalidVacationDatesException : InvalidDomainException
{
    public InvalidVacationDatesException(string message) : base(message)
    {
        
    }
}

public class OverlappingVacationException : InvalidDomainException
{
    public OverlappingVacationException(string message) : base(message)
    {

    }
}

public class PackageNotExistsInTargetYearException : InvalidDomainException
{
    public PackageNotExistsInTargetYearException(string message) : base(message)
    {

    }
}

public class InvalidYearException : InvalidDomainException
{
    public InvalidYearException(string message) : base(message)
    {
        
    }
}