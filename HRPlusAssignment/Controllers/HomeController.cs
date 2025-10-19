using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HRPlusAssignment.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace HRPlusAssignment.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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

    private static List<Employee> GetSampleEmployees()
    {
        return new List<Employee>
        {
            new Employee { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@company.com", Department = "IT", Position = "Software Developer", HireDate = new DateTime(2020, 1, 15), Salary = 75000, IsActive = true },
            new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@company.com", Department = "HR", Position = "HR Manager", HireDate = new DateTime(2019, 3, 10), Salary = 85000, IsActive = true },
            new Employee { Id = 3, FirstName = "Mike", LastName = "Johnson", Email = "mike.johnson@company.com", Department = "Finance", Position = "Financial Analyst", HireDate = new DateTime(2021, 6, 20), Salary = 70000, IsActive = true },
            new Employee { Id = 4, FirstName = "Sarah", LastName = "Williams", Email = "sarah.williams@company.com", Department = "Marketing", Position = "Marketing Specialist", HireDate = new DateTime(2022, 2, 5), Salary = 65000, IsActive = true },
            new Employee { Id = 5, FirstName = "David", LastName = "Brown", Email = "david.brown@company.com", Department = "IT", Position = "System Administrator", HireDate = new DateTime(2018, 9, 12), Salary = 80000, IsActive = true },
            new Employee { Id = 6, FirstName = "Lisa", LastName = "Davis", Email = "lisa.davis@company.com", Department = "Sales", Position = "Sales Manager", HireDate = new DateTime(2020, 11, 8), Salary = 90000, IsActive = true },
            new Employee { Id = 7, FirstName = "Tom", LastName = "Wilson", Email = "tom.wilson@company.com", Department = "Operations", Position = "Operations Coordinator", HireDate = new DateTime(2021, 4, 18), Salary = 55000, IsActive = true },
            new Employee { Id = 8, FirstName = "Emily", LastName = "Taylor", Email = "emily.taylor@company.com", Department = "HR", Position = "Recruiter", HireDate = new DateTime(2022, 7, 25), Salary = 60000, IsActive = true }
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