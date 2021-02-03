using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeadowLogsController : ControllerBase
    {
        private readonly MeadowLogContext _context;

        public MeadowLogsController(MeadowLogContext context)
        {
            _context = context;
        }

        // GET: api/MeadowLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeadowLog>>> GetMeadowLog()
        {
            return await _context.MeadowLog.ToListAsync();
        }

        // GET: api/MeadowLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MeadowLog>> GetMeadowLog(int id)
        {
            var meadowLog = await _context.MeadowLog.FindAsync(id);

            if (meadowLog == null)
            {
                return NotFound();
            }

            return meadowLog;
        }

        // PUT: api/MeadowLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMeadowLog(int id, MeadowLog meadowLog)
        {
            if (id != meadowLog.MeadowLogID)
            {
                return BadRequest();
            }

            _context.Entry(meadowLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeadowLogExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MeadowLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MeadowLog>> PostMeadowLog(MeadowLog meadowLog)
        {
            _context.MeadowLog.Add(meadowLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMeadowLog", new { id = meadowLog.MeadowLogID }, meadowLog);
        }

        // DELETE: api/MeadowLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMeadowLog(int id)
        {
            var meadowLog = await _context.MeadowLog.FindAsync(id);
            if (meadowLog == null)
            {
                return NotFound();
            }

            _context.MeadowLog.Remove(meadowLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MeadowLogExists(int id)
        {
            return _context.MeadowLog.Any(e => e.MeadowLogID == id);
        }
    }
}
