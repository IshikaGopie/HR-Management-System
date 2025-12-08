using HRManagementSystem.Data;
using HRManagementSystem.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Controllers;

public class JobsController : Controller
{
    private readonly HrDbContext _context;
    private readonly ILogger<JobsController> _logger;

    public JobsController(HrDbContext context, ILogger<JobsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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
        if (string.IsNullOrEmpty(job.JobId))
        {
            job.JobId = GenerateJobId();
        }
        
        _context.Jobs.Add(job);
        _context.SaveChanges();
        return Json(new[] { job }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public IActionResult Job_Update([DataSourceRequest] DataSourceRequest request, Job job)
    {
        _context.Jobs.Update(job);
        _context.SaveChanges();
        return Json(new[] { job }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public IActionResult Job_Delete([DataSourceRequest] DataSourceRequest request, Job job)
    {
        // check if the job is in use by a position
        if (_context.Positions.Any(p => p.JobId == job.JobId))
            ModelState.AddModelError("delete", "Job is in use by one or more positions and cannot be deleted.");
        else
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
    
    private string GenerateJobId()
    {
        var lastJob = _context.Jobs
            .Where(e => e.JobId.StartsWith("JOB") && e.JobId.Length == 6)
            .OrderByDescending(e => e.JobId)
            .FirstOrDefault();

        if (lastJob == null)
        {
            return "JOB001";
        }

        try
        {
            var lastNumber = int.Parse(lastJob.JobId.Substring(3));
            return $"JOB{(lastNumber + 1):D3}";
        }
        catch
        {
            return $"JOB{DateTime.Now:yyyyMMddHHmmss}";
        }
    }
}


