using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechResourceTrackerDataHandling.Models;

namespace TechResourceTrackerDataHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CspReportsController : ControllerBase
    {
        private readonly TechResourcesContext _context;

        public CspReportsController(TechResourcesContext context)
        {
            _context = context;
        }

        //// GET: api/CspReports
        //[HttpGet]
        //public IEnumerable<CspReport> GetCspReport()
        //{
        //    return _context.CspReport;
        //}

        //// GET: api/CspReports/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetCspReport([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var cspReport = await _context.CspReport.FindAsync(id);

        //    if (cspReport == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(cspReport);
        //}

        //// PUT: api/CspReports/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCspReport([FromRoute] int id, [FromBody] CspReport cspReport)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != cspReport.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(cspReport).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CspReportExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/CspReports
        [HttpPost]
        public async Task<StatusCodeResult> PostCspReport([FromBody] CspReportWrapper cspReportWrapper)
        {
            _context.CspReport.Add(cspReportWrapper.Report);
            await _context.SaveChangesAsync();

             return StatusCode(204);
        }

        // DELETE: api/CspReports/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCspReport([FromRoute] int id)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var cspReport = await _context.CspReport.FindAsync(id);
        //    if (cspReport == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.CspReport.Remove(cspReport);
        //    await _context.SaveChangesAsync();

        //    return Ok(cspReport);
        //}

        //private bool CspReportExists(int id)
        //{
        //    return _context.CspReport.Any(e => e.Id == id);
        //}
    }
}