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
        var employees = _context.Employees.ToList();
        return employees;
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