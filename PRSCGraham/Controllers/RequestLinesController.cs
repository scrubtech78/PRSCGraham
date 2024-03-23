using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSCGraham.Models;

namespace PRSCGraham.Controllers
{
    [Route("api/LineItems")]
    [ApiController]
    public class RequestLinesController : ControllerBase
    {
        private readonly PrsDbContext _context;

        public RequestLinesController(PrsDbContext context)
        {
            _context = context;
        }

        // GET: api/RequestLines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RequestLines>>> GetRequestLines()
        {
            if (_context.RequestLines == null)
            {
                return NotFound();
            }
            return await _context.RequestLines.Include(pr => pr.Product)
                                               .Include(rl => rl.Request).ToListAsync();
        }

        // GET: api/RequestLines/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RequestLines>> GetRequestLines(int id)
        {


            var requestLines = await _context.RequestLines.Include(pr => pr.Product)
                           .Include(r => r.Request).FirstOrDefaultAsync(r => r.Id == id);

            return requestLines;
        }

        // PUT: api/RequestLines/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequestLines(int id, RequestLines requestLine)
        {
            if (id != requestLine.Id)
            {
                return BadRequest();
            }

            _context.Entry(requestLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                RecalculateTotal(requestLine.RequestId);//recaculate total
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestLinesExists(id))
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

        // POST: api/RequestLines
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RequestLines>> PostRequestLines([FromBody] RequestLines requestLine)
        {
            if (_context.RequestLines == null)
            {
                return Problem("Entity set 'PrsDbContext.RequestLines'  is null.");
            }
            _context.RequestLines.Add(requestLine);
            await _context.SaveChangesAsync();
            //recalculate total
            RecalculateTotal(requestLine.RequestId);// recalculate total

            return CreatedAtAction("GetRequestLines", new { id = requestLine.Id }, requestLine);
        }







        // DELETE: api/RequestLines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequestLines(int id)
        {
            if (_context.RequestLines == null)
            {
                return NotFound();
            }
            var requestLine = await _context.RequestLines.FindAsync(id);
            if (requestLine == null)
            {
                return NotFound();
            }

            _context.RequestLines.Remove(requestLine);
            await _context.SaveChangesAsync();
            RecalculateTotal(requestLine.RequestId );//recalculate total

            return NoContent();
        }

        private bool RequestLinesExists(int id)
        {
            return (_context.RequestLines?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpGet("/api/lines-for-pr/{RequestedId}")]
        public async Task<ActionResult<IEnumerable<RequestLines>>> GetLineItemsByRequestId(int RequestedId)

        {
            var lines = await _context.RequestLines.Include(i => i.Product).Where(r => r.RequestId == RequestedId).ToListAsync();


            return lines;
        }
        private  void  RecalculateTotal(int requestId)
        {   //calculate the sum of price*quantity for this request of requestLines
            decimal theTotal = _context.RequestLines.Include(p => p.Product)
                .Where(rl => rl.RequestId == requestId )
                .Sum(rl => rl.Product.Price * rl.Quantity);

            //find request 
            var req = _context.Requests.Where(r => r.Id == requestId).FirstOrDefault();

            // update total
            req.Total = theTotal;
            _context.SaveChanges();//save to sql
           
        }
    
    }
}
