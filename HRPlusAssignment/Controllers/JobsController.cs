using HRPlusAssignment.Data;
using HRPlusAssignment.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace HRPlusAssignment.Controllers;

public class JobsController : Controller
{
    private readonly HrDbContext _context;
    private readonly ILogger<JobsController> _logger;

    public JobsController(HrDbContext context, ILogger<JobsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Jobs_Read([DataSourceRequest] DataSourceRequest request)
    {
        var jobs = _context.Jobs.ToList();
        return Json(jobs.ToDataSourceResult(request));
    }

    public IActionResult Job_Read(string id)
    {
        var job = _context.Jobs.Find(id);
        if (job == null) return NotFound();
        return Json(job);
    }

    [HttpPost]
    public IActionResult Job_Create([DataSourceRequest] DataSourceRequest request, Job job)
    {
        if (ModelState.IsValid)
        {
            _context.Jobs.Add(job);
            _context.SaveChanges();
        }
        return Json(new[] { job }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public IActionResult Job_Update([DataSourceRequest] DataSourceRequest request, Job job)
    {
        if (ModelState.IsValid)
        {
            _context.Jobs.Update(job);
            _context.SaveChanges();
        }
        return Json(new[] { job }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public IActionResult Job_Delete([DataSourceRequest] DataSourceRequest request, Job job)
    {
        if (ModelState.IsValid)
        {
            _context.Jobs.Remove(job);
            _context.SaveChanges();
        }
        return Json(new[] { job }.ToDataSourceResult(request, ModelState));
    }

    // For dropdowns
    public IActionResult GetJobs()
    {
        var jobs = _context.Jobs.Select(j => new { Value = j.JobId, Text = j.JobTitle }).ToList();
        return Json(jobs);
    }
}


