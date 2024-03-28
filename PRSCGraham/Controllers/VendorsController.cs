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
    public class VendorsController : ControllerBase
    {
        private readonly PrsDbContext _context;

        public VendorsController(PrsDbContext context)
        {
            _context = context;
        }
        [HttpPost("byCityState")]
        public ActionResult GetVendorByCityState([FromBody] CityState location)
        {
            var vendors = _context.Vendors.Where(v => v.City == location.City &&
                                                       v.State == location.State);
            return Ok(vendors);
        }

        //api/vendors/code/{code}

        // [HttpGet("code/{vendorcode}")]
        // public ActionResult GetVendorByCode(string vendorcode) 

        [HttpPost("code")]
        public ActionResult GetVendorByCode([FromBody]string vendorcode)
        {
            var vendor = _context.Vendors.Where(v => v.Code == vendorcode).FirstOrDefault();
            if (vendor == null)
            {
                return NotFound();
            }
            return Ok (vendor);
        }

        // GET: api/Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
          if (_context.Vendors == null)
          {
              return NotFound();
          }
            return await _context.Vendors.ToListAsync();
        }

        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
          if (_context.Vendors == null)
          {
              return NotFound();
          }
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }
       // [HttpGet("code")]        //vendor summary attempt
       // public Task<ActionResult<Vendor>> GetVendorSummary(Vendor vendorcode)
       ////method to count products 
           // var vendor = _context.Vendors.Include(p => p.Product)
               // .Where(v => v.Code == vendorcode.Code && v.Name == vendorcode.Name)
              //  .Sum(p => p.Product.Name.Count());
            
            //returning the new request
           // var vendorSum = _context.Vendors.Where(v => v.Code == vendorcode.Code && v.Name == vendorcode.Name);
           // return vendorSum;
            
            

        
        // PUT: api/Vendors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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

        // POST: api/Vendors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor vendor)
        {
          if (_context.Vendors == null)
          {
              return Problem("Entity set 'PrsDbContext.Vendors'  is null.");
          }
            _context.Vendors.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = vendor.Id }, vendor);
        }

        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            if (_context.Vendors == null)
            {
                return NotFound();
            }
            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        
        private bool VendorExists(int id)
        {
            return (_context.Vendors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
