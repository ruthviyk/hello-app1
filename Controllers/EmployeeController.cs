using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace hello_app.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        public static List<Employee> EmployeeList()
        {
            return new List<Employee>
            {
                { new Employee(){EmployeeId=1,Name="Walt"} },
                { new Employee(){EmployeeId=2,Name="Anna"} }
            };
        }

        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetEmployee")]
        public IEnumerable<Employee> Get(int employeeId)
        {
            List<Employee> employees = EmployeeList();
            var subset = from theEmployee in employees
                         where theEmployee.EmployeeId == employeeId
                         select theEmployee;
            return subset;
        }
    }
}