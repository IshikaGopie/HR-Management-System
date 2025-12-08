// File: Data/DatabaseSeeder.cs
using HRManagementSystem.Models;

namespace HRManagementSystem.Data
{
    public static class DatabaseSeeder
    {
        public static async Task SeedAsync(HrDbContext context)
        {
            // check if database is created
            await context.Database.EnsureCreatedAsync();

            // check if data already exists
            if (context.Departments.Any())
            {
                return; 
            }

            // seed JobGroups
            var jobGroups = new List<JobGroup>
            {
                new JobGroup { JobGroupId = "JG001", JobGroupName = "Executive" },
                new JobGroup { JobGroupId = "JG002", JobGroupName = "Management" },
                new JobGroup { JobGroupId = "JG003", JobGroupName = "Professional" },
                new JobGroup { JobGroupId = "JG004", JobGroupName = "Administrative" },
                new JobGroup { JobGroupId = "JG005", JobGroupName = "Technical" }
            };
            context.JobGroups.AddRange(jobGroups);
            await context.SaveChangesAsync();

          
            // Seed Departments
            var departments = new List<Department>
            {
                new Department { DepartmentId = "DEPT001", DepartmentCode = "HR", DepartmentName = "Human Resources" },
                new Department { DepartmentId = "DEPT002", DepartmentCode = "IT", DepartmentName = "Information Technology" },
                new Department { DepartmentId = "DEPT003", DepartmentCode = "FIN", DepartmentName = "Finance" },
                new Department { DepartmentId = "DEPT004", DepartmentCode = "MKT", DepartmentName = "Marketing" },
                new Department { DepartmentId = "DEPT005", DepartmentCode = "OPS", DepartmentName = "Operations" }
            };
            context.Departments.AddRange(departments);
            await context.SaveChangesAsync();
            
            // Seed Jobs
            var jobs = new List<Job>
            {
                new Job { JobId = "JOB001", JobCode = "CEO", JobTitle = "Chief Executive Officer", JobGroupId = "JG001" },
                new Job { JobId = "JOB002", JobCode = "CTO", JobTitle = "Chief Technology Officer", JobGroupId = "JG001" },
                new Job { JobId = "JOB003", JobCode = "HRM", JobTitle = "Human Resources Manager", JobGroupId = "JG002" },
                new Job { JobId = "JOB004", JobCode = "ITM", JobTitle = "IT Manager", JobGroupId = "JG002" },
                new Job { JobId = "JOB005", JobCode = "DEV", JobTitle = "Software Developer", JobGroupId = "JG003" },
                new Job { JobId = "JOB006", JobCode = "ANALYST", JobTitle = "Business Analyst", JobGroupId = "JG003" },
                new Job { JobId = "JOB007", JobCode = "ADMIN", JobTitle = "Administrative Assistant", JobGroupId = "JG004" },
                new Job { JobId = "JOB008", JobCode = "TECH", JobTitle = "Technical Support", JobGroupId = "JG005" }
            };
            context.Jobs.AddRange(jobs);
            await context.SaveChangesAsync();

            
            // Seed Positions
            var positions = new List<Position>
            {
                new Position { PositionId = "POS001", PositionCode = "CEO001", PositionTitle = "Chief Executive Officer", DepartmentId = "DEPT001", JobId = "JOB001", JobLevel = "C-Level", Status = PositionStatus.Active },
                new Position { PositionId = "POS002", PositionCode = "CTO001", PositionTitle = "Chief Technology Officer", DepartmentId = "DEPT002", JobId = "JOB002", JobLevel = "C-Level", ReportsToPositionId = "POS001", Status = PositionStatus.Active },
                new Position { PositionId = "POS003", PositionCode = "HRM001", PositionTitle = "Human Resources Manager", DepartmentId = "DEPT001", JobId = "JOB003", JobLevel = "Manager", ReportsToPositionId = "POS001", Status = PositionStatus.Active },
                new Position { PositionId = "POS004", PositionCode = "ITM001", PositionTitle = "IT Manager", DepartmentId = "DEPT002", JobId = "JOB004", JobLevel = "Manager", ReportsToPositionId = "POS002", Status = PositionStatus.Active },
                new Position { PositionId = "POS005", PositionCode = "DEV001", PositionTitle = "Senior Software Developer", DepartmentId = "DEPT002", JobId = "JOB005", JobLevel = "Senior", ReportsToPositionId = "POS004", Status = PositionStatus.Active },
                new Position { PositionId = "POS006", PositionCode = "DEV002", PositionTitle = "Software Developer", DepartmentId = "DEPT002", JobId = "JOB005", JobLevel = "Mid", ReportsToPositionId = "POS004", Status = PositionStatus.Active },
                new Position { PositionId = "POS007", PositionCode = "ANALYST001", PositionTitle = "Business Analyst", DepartmentId = "DEPT003", JobId = "JOB006", JobLevel = "Mid", ReportsToPositionId = "POS001", Status = PositionStatus.Active },
                new Position { PositionId = "POS008", PositionCode = "ADMIN001", PositionTitle = "Administrative Assistant", DepartmentId = "DEPT001", JobId = "JOB007", JobLevel = "Entry", ReportsToPositionId = "POS003", Status = PositionStatus.Active },
                new Position { PositionId = "POS009", PositionCode = "DEV003", PositionTitle = "Junior Software Developer", DepartmentId = "DEPT002", JobId = "JOB005", JobLevel = "Entry", ReportsToPositionId = "POS004", Status = PositionStatus.Active },
                new Position { PositionId = "POS010", PositionCode = "ANALYST002", PositionTitle = "Senior Business Analyst", DepartmentId = "DEPT003", JobId = "JOB006", JobLevel = "Senior", ReportsToPositionId = "POS001", Status = PositionStatus.Active },
                new Position { PositionId = "POS011", PositionCode = "TECH001", PositionTitle = "Technical Support Specialist", DepartmentId = "DEPT002", JobId = "JOB008", JobLevel = "Mid", ReportsToPositionId = "POS004", Status = PositionStatus.Active },
                new Position { PositionId = "POS012", PositionCode = "ADMIN002", PositionTitle = "Office Administrator", DepartmentId = "DEPT001", JobId = "JOB007", JobLevel = "Mid", ReportsToPositionId = "POS003", Status = PositionStatus.Active },
                new Position { PositionId = "POS013", PositionCode = "FIN001", PositionTitle = "Financial Analyst", DepartmentId = "DEPT003", JobId = "JOB006", JobLevel = "Mid", ReportsToPositionId = "POS001", Status = PositionStatus.Active },
                new Position { PositionId = "POS014", PositionCode = "MKT001", PositionTitle = "Marketing Coordinator", DepartmentId = "DEPT004", JobId = "JOB006", JobLevel = "Mid", ReportsToPositionId = "POS001", Status = PositionStatus.Active },
                new Position { PositionId = "POS015", PositionCode = "OPS001", PositionTitle = "Operations Coordinator", DepartmentId = "DEPT005", JobId = "JOB006", JobLevel = "Mid", ReportsToPositionId = "POS001", Status = PositionStatus.Active }
            };
            context.Positions.AddRange(positions);
            await context.SaveChangesAsync();

            
            // Seed Employees
            var employees = new List<Employee>
            {
                new Employee { EmployeeId = "EMP001", PositionId = "POS001", FirstName = "John", LastName = "Smith", Email = "john.smith@company.com", Phone = "555-0101", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP002", PositionId = "POS002", FirstName = "Sarah", LastName = "Johnson", Email = "sarah.johnson@company.com", Phone = "555-0102", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP003", PositionId = "POS003", FirstName = "Mike", LastName = "Davis", Email = "mike.davis@company.com", Phone = "555-0103", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP004", PositionId = "POS004", FirstName = "Lisa", LastName = "Wilson", Email = "lisa.wilson@company.com", Phone = "555-0104", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP005", PositionId = "POS005", FirstName = "David", LastName = "Brown", Email = "david.brown@company.com", Phone = "555-0105", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP006", PositionId = "POS006", FirstName = "Emily", LastName = "Taylor", Email = "emily.taylor@company.com", Phone = "555-0106", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP007", PositionId = "POS007", FirstName = "Robert", LastName = "Anderson", Email = "robert.anderson@company.com", Phone = "555-0107", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP008", PositionId = "POS008", FirstName = "Jennifer", LastName = "Martinez", Email = "jennifer.martinez@company.com", Phone = "555-0108", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP009", PositionId = "POS009", FirstName = "Alex", LastName = "Garcia", Email = "alex.garcia@company.com", Phone = "555-0109", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP010", PositionId = "POS010", FirstName = "Maria", LastName = "Rodriguez", Email = "maria.rodriguez@company.com", Phone = "555-0110", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP011", PositionId = "POS011", FirstName = "James", LastName = "Lee", Email = "james.lee@company.com", Phone = "555-0111", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP012", PositionId = "POS012", FirstName = "Amanda", LastName = "White", Email = "amanda.white@company.com", Phone = "555-0112", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP013", PositionId = "POS013", FirstName = "Christopher", LastName = "Harris", Email = "christopher.harris@company.com", Phone = "555-0113", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP014", PositionId = "POS014", FirstName = "Jessica", LastName = "Clark", Email = "jessica.clark@company.com", Phone = "555-0114", Status = EmployeeStatus.Active },
                new Employee { EmployeeId = "EMP015", PositionId = "POS015", FirstName = "Michael", LastName = "Lewis", Email = "michael.lewis@company.com", Phone = "555-0115", Status = EmployeeStatus.Active }
            };
            context.Employees.AddRange(employees);
            await context.SaveChangesAsync();
        }
    }
}
