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
    
    // Employees
    
    // List of employees
    public IActionResult Employees_Read([DataSourceRequest] DataSourceRequest request)
    {
        var employees = _context.Employees.ToList();
        return Json(employees.ToDataSourceResult(request));
    }
    
    // Get employee by id
    public IActionResult Employee_Read(string id)
    {
        var employee = _context.Employees.Find(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Json(employee);
    }
    
    // Create employee
    public IActionResult Employee_Create([FromBody] Employee employee)
    {
        if (ModelState.IsValid)
        {
            // Generate EmployeeId if not provided
            if (string.IsNullOrEmpty(employee.EmployeeId))
            {
                employee.EmployeeId = GenerateEmployeeId();
            }
            
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Json(employee);
        }
        return BadRequest(ModelState);
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
            // Fallback if parsing fails
            return $"EMP{DateTime.Now:yyyyMMddHHmmss}";
        }
    }
    
    // Update employee
    public IActionResult Employee_Update([FromBody] Employee employee)
    {
        if (ModelState.IsValid)
        {
            var existingEmployee = _context.Employees.Find(employee.EmployeeId);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            
            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.PositionId = employee.PositionId;
            existingEmployee.Status = employee.Status;
            
            _context.SaveChanges();
            return Json(existingEmployee);
        }
        return BadRequest(ModelState);
    }
    
    // Delete employee
    public IActionResult Employee_Delete([FromBody] Employee employee)
    {
        if (ModelState.IsValid)
        {
            var existingEmployee = _context.Employees.Find(employee.EmployeeId);
            if (existingEmployee == null)
            {
                return NotFound();
            }
            
            _context.Employees.Remove(existingEmployee);
            _context.SaveChanges();
            return Json(existingEmployee);
        }
        return BadRequest(ModelState);
    }
    
    // Positions
    // List of positions
    public IActionResult Positions_Read([DataSourceRequest] DataSourceRequest request)
    {
        var positions = _context.Positions.ToList();
        return Json(positions.ToDataSourceResult(request));
    }
    
    // Get position by id
    public IActionResult Position_Read(string id)
    {
        var position = _context.Positions.Find(id);
        if (position == null)
        {
            return NotFound();
        }
        return Json(position);
    }
    
    // Create position
    public IActionResult Position_Create([FromBody] Position position)
    {
        if (ModelState.IsValid)
        {
            _context.Positions.Add(position);
            _context.SaveChanges();
            return Json(position);
        }
        return BadRequest(ModelState);
    }
    
    // Update position
    public IActionResult Position_Update([FromBody] Position position)
    {
        if (ModelState.IsValid)
        {
            _context.Positions.Update(position);
            _context.SaveChanges();
            return Json(position);
        }
        return BadRequest(ModelState);
    }
    
    // Delete position
    public IActionResult Position_Delete([FromBody] Position position)
    {
        if (ModelState.IsValid)
        {
            _context.Positions.Remove(position);
            _context.SaveChanges();
            return Json(position);
        }
        return BadRequest(ModelState);
    }
    
    
    // Jobs
    public IActionResult Jobs_Read([DataSourceRequest] DataSourceRequest request)
    {
        var jobs = _context.Jobs.ToList();
        return Json(jobs.ToDataSourceResult(request));
    }
    
    // Get job by id
    public IActionResult Job_Read(string id)
    {
        var job = _context.Jobs.Find(id);
        if (job == null)
        {
            return NotFound();
        }
        return Json(job);
    }
    
    // Create job
    public IActionResult Job_Create([FromBody] Job job)
    {
        if (ModelState.IsValid)
        {
            _context.Jobs.Add(job);
            _context.SaveChanges();
            return Json(job);
        }
        return BadRequest(ModelState);
    }
    
    // Update job
    public IActionResult Job_Update([FromBody] Job job)
    {
        if (ModelState.IsValid)
        {
            _context.Jobs.Update(job);
            _context.SaveChanges();
            return Json(job);
        }
        return BadRequest(ModelState);
    }
    
    // Delete job
    public IActionResult Job_Delete([FromBody] Job job)
    {
        if (ModelState.IsValid)
        {
            _context.Jobs.Remove(job);
            _context.SaveChanges();
            return Json(job);
        }
        return BadRequest(ModelState);
    }
    
    // Dropdown data methods
    public IActionResult GetDepartments()
    {
        var departments = _context.Departments.Select(d => new { Value = d.DepartmentId, Text = d.DepartmentName }).ToList();
        return Json(departments);
    }
    
    public IActionResult GetJobs()
    {
        var jobs = _context.Jobs.Select(j => new { Value = j.JobId, Text = j.JobTitle }).ToList();
        return Json(jobs);
    }
    
    public IActionResult GetPositions()
    {
        var positions = _context.Positions.Select(p => new { Value = p.PositionId, Text = p.PositionTitle }).ToList();
        return Json(positions);
    }
    
    public IActionResult GetJobGroups()
    {
        var jobGroups = _context.JobGroups.Select(jg => new { Value = jg.JobGroupId, Text = jg.JobGroupName }).ToList();
        return Json(jobGroups);
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