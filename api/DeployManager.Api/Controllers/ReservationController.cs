using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DeployManager.Api.Models;

namespace DeployManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly DeployManagerContext _context;

        public ReservationController(DeployManagerContext context)
        {
            _context = context;
        }

        // GET: api/Reservation
        [HttpGet]
        public IEnumerable<Reservation> GetReservation()
        {
            return _context.Reservation
                .FromSql($"EXECUTE [dbo].[GetReservations]")
                .ToList();
        }

        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public IActionResult GetReservation([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservations = _context.Reservation
                .FromSql($"EXECUTE [dbo].[GetReservations] {id}")
                .ToList();

            if (reservations == null)
            {
                return NotFound();
            }

            return Ok(reservations.Single());
        }

        // PUT: api/Reservation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation([FromRoute] string id, [FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservation
        [HttpPost]
        public async Task<IActionResult> PostReservation([FromBody] Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Reservation.FromSql($@"
                EXECUTE [dbo].[InsertReservation] 
                    {reservation.DeployType},
                    {reservation.ServerType},
                    {reservation.BranchName},
                    {reservation.Author},
                    {reservation.Start},
                    {reservation.End},
                    {reservation.Previous}
                ").ToList();
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReservationExists(reservation.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();

            return Ok(reservation);
        }

        private bool ReservationExists(string id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}