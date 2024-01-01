using System.Security.Cryptography.Pkcs;
using Microsoft.AspNetCore.Mvc;
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
    }
}
