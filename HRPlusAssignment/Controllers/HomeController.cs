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
    public IActionResult Employee_Read(int id)
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
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Json(employee);
        }
        return BadRequest(ModelState);
    }
    
    // Update employee
    public IActionResult Employee_Update([FromBody] Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();
            return Json(employee);
        }
        return BadRequest(ModelState);
    }
    
    // Delete employee
    public IActionResult Employee_Delete([FromBody] Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return Json(employee);
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
    public IActionResult Position_Read(int id)
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
    public IActionResult Job_Read(int id)
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