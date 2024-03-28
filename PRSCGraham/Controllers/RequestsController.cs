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
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly PrsDbContext _context;

        public RequestsController(PrsDbContext context)
        {
            _context = context;
        }

        // GET: api/Requests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Request>>> GetRequests()
        {

            return await _context.Requests.Include(r => r.User).ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Request>> GetRequest(int id)
        {

            var request = await _context.Requests.Include(r => r.User).FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
            {
                return NotFound();
            }

            return request;
        }

        // PUT: api/Requests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRequest(int id, Request request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RequestExists(id))
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

        // POST: api/Requests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Request>> PostRequest(Request request)
        {
            if (_context.Requests == null)
            {
                return Problem("Entity set 'PrsDbContext.Requests'  is null.");
            }
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRequest", new { id = request.UserId }, request);
        }

        // DELETE: api/Requests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRequest(int id)
        {
            if (_context.Requests == null)
            {
                return NotFound();
            }
            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }

            _context.Requests.Remove(request);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RequestExists(int id)
        {
            return (_context.Requests?.Any(e => e.UserId == id)).GetValueOrDefault();
        }


        [HttpPost("reject/{id}")]
        public async Task<ActionResult<Request>> RejectRequest(int id, [FromBody] string reasonForRejection)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);

            if (request == null)
            {
                return NotFound();
            }
            request.ReasonForRejection = reasonForRejection;
            request.Status = "REJECTED";

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return request;
        }
        [HttpGet("reviews/{userid}")]   // api/reviews/3
        public async Task<ActionResult<IEnumerable<Request>>> GetReviews(int userid)
        {//id is userid
            var reviews = await _context.Requests
                .Include(u => u.User)
                .Where(r => r.UserId != userid && r.Status == "Review").ToListAsync();
            if(reviews == null)
            {
                return NotFound();
            }
           
            
                return Ok(reviews);

             


          


            
        }

        [HttpPost("Approve/{id}")]
        public async Task<ActionResult<Request>> ApproveRequest(int id)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            request.Status = "APPROVED";
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            return request;


        }
    

            [HttpPost("ReviewRequest/{id}")]
        public async Task<ActionResult<Request>> ReviewRequest(int id)

        {
            var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == id);
            if (request == null)
            {
                return NotFound();
            }
            decimal totalRequest = request.Total;
            if(totalRequest<= 50m)
            {
                request.Status = "Approved";
            }
            else{
                request.Status="Review";
      }
            
      await _context.SaveChangesAsync();
            return request;

        }
    }
}