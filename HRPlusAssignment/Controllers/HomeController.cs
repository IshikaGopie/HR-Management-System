using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HRPlusAssignment.Models;
using HRPlusAssignment.Data;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace HRPlusAssignment.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HrDbContext _context;

    public HomeController(ILogger<HomeController> logger, HrDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Employees_Read([DataSourceRequest] DataSourceRequest request)
    {
        var employees = GetSampleEmployees();
        return Json(employees.ToDataSourceResult(request));
    }

    private List<Employee> GetSampleEmployees()
    {
        // For now, return sample data. In a real application, you would query the database
        return new List<Employee>
        {
            new Employee { EmployeeId = "EMP001", PositionId = "POS001", FirstName = "John", LastName = "Doe", Email = "john.doe@company.com", Phone = "555-0101", Status = EmployeeStatus.Active },
            new Employee { EmployeeId = "EMP002", PositionId = "POS002", FirstName = "Jane", LastName = "Smith", Email = "jane.smith@company.com", Phone = "555-0102", Status = EmployeeStatus.Active },
            new Employee { EmployeeId = "EMP003", PositionId = "POS003", FirstName = "Mike", LastName = "Johnson", Email = "mike.johnson@company.com", Phone = "555-0103", Status = EmployeeStatus.Active },
            new Employee { EmployeeId = "EMP004", PositionId = "POS004", FirstName = "Sarah", LastName = "Williams", Email = "sarah.williams@company.com", Phone = "555-0104", Status = EmployeeStatus.Active },
            new Employee { EmployeeId = "EMP005", PositionId = "POS005", FirstName = "David", LastName = "Brown", Email = "david.brown@company.com", Phone = "555-0105", Status = EmployeeStatus.Active },
            new Employee { EmployeeId = "EMP006", PositionId = "POS006", FirstName = "Lisa", LastName = "Davis", Email = "lisa.davis@company.com", Phone = "555-0106", Status = EmployeeStatus.Active },
            new Employee { EmployeeId = "EMP007", PositionId = "POS007", FirstName = "Tom", LastName = "Wilson", Email = "tom.wilson@company.com", Phone = "555-0107", Status = EmployeeStatus.Active },
            new Employee { EmployeeId = "EMP008", PositionId = "POS008", FirstName = "Emily", LastName = "Taylor", Email = "emily.taylor@company.com", Phone = "555-0108", Status = EmployeeStatus.Active }
        };
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}