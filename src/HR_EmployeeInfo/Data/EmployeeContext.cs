using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HR_EmployeeInfo.Data
{
    public class EmployeeContext :DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            var employeeModel = modelbuilder.Entity<Employee>();
            employeeModel.HasKey(p => p.Employee_No);
            employeeModel.Property(p => p.Employee_Name).IsRequired();
        }


        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

    }
}
