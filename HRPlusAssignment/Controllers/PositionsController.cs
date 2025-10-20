using HRPlusAssignment.Data;
using HRPlusAssignment.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;

namespace HRPlusAssignment.Controllers;

public class PositionsController : Controller
{
    private readonly HrDbContext _context;
    private readonly ILogger<PositionsController> _logger;

    public PositionsController(HrDbContext context, ILogger<PositionsController> logger)
    {
        _context = context;
        _logger = logger;
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
        if (ModelState.IsValid)
        {
            _context.Positions.Add(position);
            _context.SaveChanges();
        }
        return Json(new[] { position }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public IActionResult Position_Update([DataSourceRequest] DataSourceRequest request, Position position)
    {
        if (ModelState.IsValid)
        {
            _context.Positions.Update(position);
            _context.SaveChanges();
        }
        return Json(new[] { position }.ToDataSourceResult(request, ModelState));
    }

    [HttpPost]
    public IActionResult Position_Delete([DataSourceRequest] DataSourceRequest request, Position position)
    {
        if (ModelState.IsValid)
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
}


