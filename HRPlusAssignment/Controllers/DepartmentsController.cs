using HRPlusAssignment.Data;
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

    // For dropdowns
    public IActionResult GetDepartments()
    {
        var departments = _context.Departments.Select(d => new { Value = d.DepartmentId, Text = d.DepartmentName }).ToList();
        return Json(departments);
    }
}


