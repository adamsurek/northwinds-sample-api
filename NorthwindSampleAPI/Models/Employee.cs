using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NorthwindSampleAPI.Controllers;

namespace NorthwindSampleAPI.Models;

public partial class Employee
{
    public short EmployeeId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

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

    [JsonIgnore]
    public virtual ICollection<Employee> InverseReportsToNavigation { get; set; } = new List<Employee>();

    [JsonIgnore]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [JsonIgnore]
    public virtual Employee? ReportsToNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<Territory> Territories { get; set; } = new List<Territory>();

    public EmployeeDto GenerateDto()
    {
        return new EmployeeDto()
        {
            EmployeeId = this.EmployeeId,
            FirstName = this.FirstName,
            LastName = this.LastName,
            Title = this.Title,
            TitleOfCourtesy = this.TitleOfCourtesy,
            BirthDate = this.BirthDate,
            HireDate = this.HireDate,
            Address = this.Address,
            City = this.City,
            Region = this.Region,
            PostalCode = this.PostalCode,
            Country = this.Country,
            HomePhone = this.HomePhone,
            Extension = this.Extension,
            Photo = this.Photo,
            Notes = this.Notes,
            ReportsTo = this.ReportsTo,
            PhotoPath = this.PhotoPath
        };
    }
}
