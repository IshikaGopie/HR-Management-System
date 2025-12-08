using HRManagementSystem.Data;
using HRManagementSystem.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementSystem.Controllers;

public class PositionsController : Controller
{
    private readonly HrDbContext _context;
    private readonly ILogger<PositionsController> _logger;

    public PositionsController(HrDbContext context, ILogger<PositionsController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Positions_Read([DataSourceRequest] DataSourceRequest request)
    {
        var positions = _context.Positions.ToList();
        return Json(positions.ToDataSourceResult(request));
    }

    public IActionResult Position_Read(string id)
    {
        var position = _context.Positions.Find(id);
        if (position == null) return NotFound();
        return Json(position);
    }

    [HttpPost]
    public IActionResult Position_Create([DataSourceRequest] DataSourceRequest request, Position position)
    {
        if (string.IsNullOrEmpty(position.PositionId))
        {
            position.PositionId = GeneratePositionId();
        }
        
        _context.Positions.Add(position);
        _context.SaveChanges();
        return Json(new[] { position }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public IActionResult Position_Update([DataSourceRequest] DataSourceRequest request, Position position)
    {
        _context.Positions.Update(position);
        _context.SaveChanges();
        return Json(new[] { position }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public IActionResult Position_Delete([DataSourceRequest] DataSourceRequest request, Position position)
    {
        if (_context.Employees.Any(e => e.PositionId == position.PositionId))
            ModelState.AddModelError("delete", "Position is in use by one or more employees and cannot be deleted.");
        else
        {
            _context.Positions.Remove(position);
            _context.SaveChanges();
        }
        return Json(new[] { position }.ToDataSourceResult(request, ModelState));
    }

    // For dropdowns
    public IActionResult GetPositions()
    {
        var positions = _context.Positions.Select(p => new { Value = p.PositionId, Text = p.PositionTitle }).ToList();
        return Json(positions);
    }
    
    private string GeneratePositionId()
    {
        var lastPositon = _context.Positions
            .Where(e => e.PositionId.StartsWith("POS") && e.PositionId.Length == 6)
            .OrderByDescending(e => e.PositionId)
            .FirstOrDefault();

        if (lastPositon == null)
        {
            return "POS001";
        }

        try
        {
            var lastNumber = int.Parse(lastPositon.PositionId.Substring(3));
            return $"POS{(lastNumber + 1):D3}";
        }
        catch
        {
            return $"POS{DateTime.Now:yyyyMMddHHmmss}";
        }
    }
}


