namespace NorthwindSampleAPI.Models;

public record EmployeeDto
{
    public short EmployeeId { get; set; }
    
    public string FirstName { get; set; } = null!;
 
    public string LastName { get; set; } = null!;

    public string? Title { get; set; }

    public string? TitleOfCourtesy { get; set; }

    public DateOnly? BirthDate { get; set; }

    public DateOnly? HireDate { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? HomePhone { get; set; }

    public string? Extension { get; set; }

    public byte[]? Photo { get; set; }

    public string? Notes { get; set; }

    public short? ReportsTo { get; set; }

    public string? PhotoPath { get; set; }
}