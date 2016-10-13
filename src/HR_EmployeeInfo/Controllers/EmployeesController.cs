using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HR_EmployeeInfo.Data;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace HR_EmployeeInfo.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly EmployeeContext _personContext;

        public EmployeesController(EmployeeContext personContext)
        {
            _personContext = personContext;
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Employee>), (int)HttpStatusCode.OK)]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}
        public IActionResult Get()
        {
            //return Ok(await _carContext.Cars.OrderBy(p => p.Brand).ToListAsync());
            string sql = "select distinct rtrim(dbo.vEmployee.Employee_No) Employee_No, rtrim(dbo.vEmployee.Employee_Name) Employee_Name, rtrim(dbo.vEmployee.Employee_Photo) Employee_Photo"
                      + ", rtrim(dbo.vService.Employee_Position) Employee_Position, rtrim(dbo.vService.Employee_Category) Employee_Category, rtrim(dbo.vService.Employee_Division) Employee_Division"
                      + ", rtrim(dbo.vService.Employee_Department) Employee_Department, rtrim(dbo.vEmployee.Employee_Email) Employee_Email, rtrim(dbo.vAddress.Employee_Mobile) as Employee_Mobile"
                      + ", rtrim(dbo.vService.Employee_Branch) Employee_Branch "
                    + "from dbo.vEmployee "
                     + " left join dbo.vService on dbo.vEmployee.Employee_No = dbo.vService.Employee_No"
                     + " left join dbo.vAddress on dbo.vEmployee.Employee_No = dbo.vAddress.Employee_No"
                    + " where dbo.vService.Employee_Branch = 'HQ    '";
            var results = _personContext.Employees.FromSql(sql);
            if (results == null)
            {
                return NotFound();
            }
            return Ok(results);
        }

        [HttpGet("id")]
        public string Get(int id)
        {
            return "value";
        }
    }
}
