using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testApi.Models;

namespace testApi.Controllers

{
   [EnableCors("MyImplementationPolicy")]

    [Route("api/[controller]")]
    [ApiController]

    public class CoursController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cours
        [HttpGet]
        public ActionResult<IEnumerable<Cour>> GetCours()
        {

            var courses = _context.Cours;

            if (courses.ToArray().Length == 0)
            {
                var results = new
                {
                    statusCode = 404,
                    message = "Data not found"

                };
                //return new JsonResult("not found");
                return NotFound(results);
            }
            else
            {
                //var result = new
                //{
                //    statusCode = 200,
                //    data = await courses.ToListAsync()

                //};
                //return Ok(result);
                return Ok(courses);


            }
        }

        // GET: api/Cours/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cour>> GetCour(int id)
        {
            var cour = await _context.Cours.FindAsync(id);

            if (cour == null)
            {
                return NotFound();
            }

            return cour;
        }

        // PUT: api/Cours/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCour(int id, Cour cour)
        {
            if (id != cour.Id)
            {
                return BadRequest();
            }

            _context.Entry(cour).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourExists(id))
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

        // POST: api/Cour
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cour>> PostCour(Cour cour)
        {

            _context.Cours.Add(cour);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCour", new { id = cour.Id }, cour);
        }


        // DELETE: api/Cour/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cour>> DeleteCour(int id)
        {
            var cour = await _context.Cours.FindAsync(id);
            if (cour == null)
            {
                return NotFound();
            }

            _context.Cours.Remove(cour);
            await _context.SaveChangesAsync();

            return cour;
        }

        private bool CourExists(int id)
        {
            return _context.Cours.Any(e => e.Id == id);
        }
    }
}
