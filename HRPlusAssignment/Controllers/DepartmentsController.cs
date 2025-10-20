using HRPlusAssignment.Data;
using HRPlusAssignment.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace HRPlusAssignment.Controllers;

public class DepartmentsController : Controller
{
    private readonly HrDbContext _context;
    private readonly ILogger<DepartmentsController> _logger;

    public DepartmentsController(HrDbContext context, ILogger<DepartmentsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Departments_Read([DataSourceRequest] DataSourceRequest request)
    {
        var departments = _context.Departments.ToList();
        return Json(departments.ToDataSourceResult(request));
    }

    public IActionResult Department_Read(string id)
    {
        var department = _context.Departments.Find(id);
        if (department == null) return NotFound();
        return Json(department);
    }

    [HttpPost]
    public IActionResult Department_Create([DataSourceRequest] DataSourceRequest request, Department department)
    {
        if (string.IsNullOrEmpty(department.DepartmentId))
        {
            department.DepartmentId = GenerateDepartmentId();
        }
        
        _context.Departments.Add(department);
        _context.SaveChanges();
        return Json(new[] { department }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public IActionResult Department_Update([DataSourceRequest] DataSourceRequest request, Department department)
    {
        _context.Departments.Update(department);
        _context.SaveChanges();
        return Json(new[] { department }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public IActionResult Department_Delete([DataSourceRequest] DataSourceRequest request, Department department)
    {
        _context.Departments.Remove(department);
        _context.SaveChanges();
        return Json(new[] { department }.ToDataSourceResult(request, ModelState));
    }

    // For dropdowns
    public IActionResult GetDepartments()
    {
        var departments = _context.Departments.Select(d => new { Value = d.DepartmentId, Text = d.DepartmentName }).ToList();
        return Json(departments);
    }
    
    private string GenerateDepartmentId()
    {
        var lastDepartment = _context.Departments
            .Where(e => e.DepartmentId.StartsWith("DEPT") && e.DepartmentId.Length == 7)
            .OrderByDescending(e => e.DepartmentId)
            .FirstOrDefault();

        if (lastDepartment == null)
        {
            return "DEPT001";
        }

        try
        {
            var lastNumber = int.Parse(lastDepartment.DepartmentId.Substring(4));
            return $"DEPT{(lastNumber + 1):D3}";
        }
        catch
        {
            return $"DEPT{DateTime.Now:yyyyMMddHHmmss}";
        }
    }
}



