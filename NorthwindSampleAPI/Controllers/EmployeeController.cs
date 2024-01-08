using System.Security.Cryptography.Pkcs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using NorthwindSampleAPI.Models;

namespace NorthwindSampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public EmployeeController(NorthwindContext context)
        {
	        _context = context;
        }
        
        //GET: api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
        {
            IEnumerable<Employee> employeeEntities =  await _context.Employees.ToListAsync();
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();

            foreach (var employee in employeeEntities)
            {
                employeeDtos.Add(employee.GenerateDto());
            }
            
            return Ok(employeeDtos);
        }
        
        //GET: api/employee/1
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(short id)
        {
            Employee? employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee.GenerateDto();
        }
        
        //POST: api/employee
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostEmployee(EmployeeDto employee)
        {
            Employee newEmployee = DeconstructDto(employee);
            
            await _context.Employees.AddAsync(newEmployee);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateException)
            {
                if (EmployeeExists(employee.EmployeeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            
            return CreatedAtAction("GetEmployee", new {id = employee.EmployeeId} , employee);
        }

        // PUT: api/employee/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(short id, EmployeeDto employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            Employee target = DeconstructDto(employee);

            _context.Entry(target).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        
        // DELETE: api/employee/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(short id)
        {
            Employee? employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            
            _context.Employees.Remove(employee);
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(short id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }

        private Employee DeconstructDto(EmployeeDto employee)
        {
            Employee converted = new Employee()
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Photo = employee.Photo,
                Notes = employee.Notes,
                ReportsTo = employee.ReportsTo,
                PhotoPath = employee.PhotoPath,
                InverseReportsToNavigation = new List<Employee>(),
                Orders = new List<Order>(),
                ReportsToNavigation = null,
                Territories = new List<Territory>()
            };

            return converted;
        }
    }
}
