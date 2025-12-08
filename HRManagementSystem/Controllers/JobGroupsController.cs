using HRManagementSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Controllers;

public class JobGroupsController : Controller
{
    private readonly HrDbContext _context;
    private readonly ILogger<JobGroupsController> _logger;

    public JobGroupsController(HrDbContext context, ILogger<JobGroupsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // For dropdowns
    public IActionResult GetJobGroups()
    {
        var jobGroups = _context.JobGroups.Select(jg => new { Value = jg.JobGroupId, Text = jg.JobGroupName }).ToList();
        return Json(jobGroups);
    }
}


