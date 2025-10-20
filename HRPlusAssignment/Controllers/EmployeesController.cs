using System.Diagnostics;
using HRPlusAssignment.Data;
using HRPlusAssignment.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace HRPlusAssignment.Controllers;

public class EmployeesController : Controller
{
    private readonly ILogger<EmployeesController> _logger;
    private readonly HrDbContext _context;

    public EmployeesController(ILogger<EmployeesController> logger, HrDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    // Employees CRUD for Kendo Grid
    public IActionResult Employees_Read([DataSourceRequest] DataSourceRequest request)
    {
        var employees = _context.Employees.ToList();
        return Json(employees.ToDataSourceResult(request));
    }

    public IActionResult Employee_Read(string id)
    {
        var employee = _context.Employees.Find(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Json(employee);
    }

    [HttpPost]
    public IActionResult Employee_Create([DataSourceRequest] DataSourceRequest request, Employee employee)
    {
        if (string.IsNullOrEmpty(employee.EmployeeId))
        {
            employee.EmployeeId = GenerateEmployeeId();
        }

        _context.Employees.Add(employee);
        _context.SaveChanges();

        return Json(new[] { employee }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public IActionResult Employee_Update([DataSourceRequest] DataSourceRequest request, Employee employee)
    {
        var existingEmployee = _context.Employees.Find(employee.EmployeeId);
        if (existingEmployee == null)
        {
            ModelState.AddModelError("", "Employee not found");
        }
        else
        {
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.PositionId = employee.PositionId;
            existingEmployee.Status = employee.Status;

            _context.SaveChanges();
        }

        return Json(new[] { employee }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public IActionResult Employee_Delete([DataSourceRequest] DataSourceRequest request, Employee employee)
    {
        var existingEmployee = _context.Employees.Find(employee.EmployeeId);
        if (existingEmployee == null)
        {
            ModelState.AddModelError("", "Employee not found");
        }
        else
        {
            _context.Employees.Remove(existingEmployee);
            _context.SaveChanges();
        }

        return Json(new[] { employee }.ToDataSourceResult(request, ModelState));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private string GenerateEmployeeId()
    {
        var lastEmployee = _context.Employees
            .Where(e => e.EmployeeId.StartsWith("EMP") && e.EmployeeId.Length == 6)
            .OrderByDescending(e => e.EmployeeId)
            .FirstOrDefault();

        if (lastEmployee == null)
        {
            return "EMP001";
        }

        try
        {
            var lastNumber = int.Parse(lastEmployee.EmployeeId.Substring(3));
            return $"EMP{(lastNumber + 1):D3}";
        }
        catch
        {
            return $"EMP{DateTime.Now:yyyyMMddHHmmss}";
        }
    }
}


