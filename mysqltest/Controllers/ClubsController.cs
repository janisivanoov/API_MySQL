using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mysqltest.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mysqltest.Models.Repository;
using mysqltest.Paging;
using AutoMapper;

namespace mysqltest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubsController : ControllerBase

    {

        private IDataRepository<Club> _dataRepository;
        private IMapper _mapper;
        
        private readonly ClubsContext _context;

        public ClubsController(IDataRepository<Club> dataRepository)
        {
          
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IActionResult GetClubs([FromQuery] QueryParameters qp)
        {
            IEnumerable<Club> clubs = _dataRepository.GetAll(qp);
            return Ok(clubs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Club>> GetClub(int id)
        {
            var club = await _context.Clubs.FindAsync(id);

            if (club == null)
            {
                return NotFound();
            }

            return club;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutClub(int id, Club club)
        {
            if (id != club.Id)
            {
                return BadRequest();
            }

            _context.Entry(club).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClubExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Club>> PostClub(Club club)
        {
            _context.Clubs.Add(club);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClub", new { id = club.Id }, club);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Club>> DeleteClub(int id)
        {
            var club = await _context.Clubs.FindAsync(id);
            if (club == null)
            {
                return NotFound();
            }

            _context.Clubs.Remove(club);
            await _context.SaveChangesAsync();

            return club;
        }

        private bool ClubExists(int id)
        {
            return _context.Clubs.Any(e => e.Id == id);
        }
    }
}