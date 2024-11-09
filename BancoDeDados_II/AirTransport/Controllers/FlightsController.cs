using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirTransport;
using AirTransport.Models;

namespace AirTransport.Controllers
{
    public class FlightsController : Controller
    {
        private readonly AirTransportContext _context;

        public FlightsController(AirTransportContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
            var airTransportContext = _context.Flights.Include(f => f.IdAircraftNavigation).Include(f => f.IdDestinationAirportNavigation).Include(f => f.IdOriginAirportNavigation);
            return View(await airTransportContext.ToListAsync());
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.IdAircraftNavigation)
                .Include(f => f.IdDestinationAirportNavigation)
                .Include(f => f.IdOriginAirportNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["IdAircraft"] = new SelectList(_context.Aircraft, "Id", "Model");
            ViewData["IdDestinationAirport"] = new SelectList(_context.Airports, "Id", "Name");
            ViewData["IdOriginAirport"] = new SelectList(_context.Airports, "Id", "Name");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdAircraft,IdOriginAirport,IdDestinationAirport,ExitTime,EstimatedArrivalTime")] Flight flight)
        {
            ModelState.Remove("IdAircraftNavigation");
            ModelState.Remove("IdOriginAirportNavigation");
            ModelState.Remove("IdOriginAirportNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAircraft"] = new SelectList(_context.Aircraft, "Id", "Model", flight.IdAircraft);
            ViewData["IdDestinationAirport"] = new SelectList(_context.Airports, "Id", "Name", flight.IdDestinationAirport);
            ViewData["IdOriginAirport"] = new SelectList(_context.Airports, "Id", "Name", flight.IdOriginAirport);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["IdAircraft"] = new SelectList(_context.Aircraft, "Id", "Model", flight.IdAircraft);
            ViewData["IdDestinationAirport"] = new SelectList(_context.Airports, "Id", "Name", flight.IdDestinationAirport);
            ViewData["IdOriginAirport"] = new SelectList(_context.Airports, "Id", "Name", flight.IdOriginAirport);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdAircraft,IdOriginAirport,IdDestinationAirport,ExitTime,EstimatedArrivalTime")] Flight flight)
        {
            if (id != flight.Id)
            {
                return NotFound();
            }

            ModelState.Remove("IdAircraftNavigation");
            ModelState.Remove("IdOriginAirportNavigation");
            ModelState.Remove("IdOriginAirportNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAircraft"] = new SelectList(_context.Aircraft, "Id", "Model", flight.IdAircraft);
            ViewData["IdDestinationAirport"] = new SelectList(_context.Airports, "Id", "Name", flight.IdDestinationAirport);
            ViewData["IdOriginAirport"] = new SelectList(_context.Airports, "Id", "Name", flight.IdOriginAirport);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.IdAircraftNavigation)
                .Include(f => f.IdDestinationAirportNavigation)
                .Include(f => f.IdOriginAirportNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.Id == id);
        }
    }
}
