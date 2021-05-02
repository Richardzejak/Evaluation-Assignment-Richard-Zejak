using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KodTestQueenslabApi.Data;
using KodTestQueenslabApi.Models;

namespace KodTestQueenslabApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorsController : ControllerBase
    {
        public Department Department { get; set; }
        public List<Department> Departments { get; set; }
        private readonly Context _context;

        public VisitorsController(Context context)
        {
            _context = context;
        }


        // POST: api/Visitors
        // Posts a visitor entering a department.
        [HttpPost]
        public async Task<ActionResult<Visitor>> PostVisitor(Visitor visitor, [FromQuery] int departmentId)
        {
            visitor.Department = await _context.Departments.Where(d => d.Id == departmentId).FirstOrDefaultAsync();
            _context.Visitors.Add(visitor);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetVisitor", new { id = visitor.Id }, visitor.Department);
        }

        // GET: api/Visitors
        // Gets all the visitors and which department they have entered.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Visitor>>> GetVisitors()
        {

            return await _context.Visitors.Include(d => d.Department).ToListAsync();
        }

        // Lookup a visitor by Id, which department they are visiting and which time they entered the department
        // GET: api/Visitors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Visitor>> GetVisitor(int id)
        {
            var visitor = await _context.Visitors.Include(d => d.Department).Where(v => v.Id == id).FirstOrDefaultAsync();

            if (visitor == null)
            {
                return NotFound();
            }

            return visitor;
        }

        // Gets the number of visitors in the whole warehouse
        // GET: api/Visitorscount
        [Route("/api/Visitorscount")]
        [HttpGet]
        public async Task<ActionResult<int>> GetWarehouseCount()
        {
            var visitorCount = await _context.Visitors.ToListAsync();
            return visitorCount.Count();
        }

        // Gets the number of visitors in a requested department
        // GET: api/Visitorscount/5
        [Route("/api/department/count/{id}")]
        [HttpGet]
        public async Task<ActionResult<int>> GetVisitorsCount(int id)
        {
            var visitorCount = await _context.Visitors.Include(d => d.Department).Where(dv => dv.Department.Id==id).ToListAsync();
            return visitorCount.Count();
        }

        // Deleted a visitor leaving the department or warehouse
        // DELETE: api/Visitors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisitor(int id)
        {
            var visitor = await _context.Visitors.FindAsync(id);
            if (visitor == null)
            {
                return NotFound();
            }

            _context.Visitors.Remove(visitor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // resets the department from visitors
        // DELETE: api/department/reset/5
        [Route("/api/department/reset/{id}")]
        [HttpDelete]
        public async Task<ActionResult<string>> ResetDepartment(int id)
        {
            var departmentVisitors = await _context.Visitors.Include(d => d.Department).Where(dv => dv.Department.Id == id).ToListAsync();
            _context.Visitors.RemoveRange(departmentVisitors);
            _context.SaveChanges();
            return ("Department reseted");
        }
    }
}
