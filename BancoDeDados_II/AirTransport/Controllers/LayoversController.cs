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
    public class LayoversController : Controller
    {
        private readonly AirTransportContext _context;

        public LayoversController(AirTransportContext context)
        {
            _context = context;
        }

        // GET: Layovers
        public async Task<IActionResult> Index()
        {
            var airTransportContext = _context.Layovers.Include(l => l.IdDestinationAirportNavigation).Include(l => l.IdFlightNavigation).Include(l => l.IdOriginAirportNavigation);
            return View(await airTransportContext.ToListAsync());
        }

        // GET: Layovers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var layover = await _context.Layovers
                .Include(l => l.IdDestinationAirportNavigation)
                .Include(l => l.IdFlightNavigation)
                .Include(l => l.IdOriginAirportNavigation)
                .FirstOrDefaultAsync(m => m.IdFlight == id);
            if (layover == null)
            {
                return NotFound();
            }

            return View(layover);
        }

        // GET: Layovers/Create
        public IActionResult Create()
        {
            ViewData["IdDestinationAirport"] = new SelectList(_context.Airports, "Id", "Name");
            ViewData["IdFlight"] = new SelectList(_context.Flights, "Id", "Id");
            ViewData["IdOriginAirport"] = new SelectList(_context.Airports, "Id", "Name");
            return View();
        }

        // POST: Layovers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFlight,IdOriginAirport,IdDestinationAirport,ExitTime,EstimatedArrivalTime")] Layover layover)
        {
            ModelState.Remove("IdFlightNavigation");
            ModelState.Remove("IdOriginAirportNavigation");
            ModelState.Remove("IdDestinationAirportNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(layover);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDestinationAirport"] = new SelectList(_context.Airports, "Id", "Name", layover.IdDestinationAirport);
            ViewData["IdFlight"] = new SelectList(_context.Flights, "Id", "Id", layover.IdFlight);
            ViewData["IdOriginAirport"] = new SelectList(_context.Airports, "Id", "Name", layover.IdOriginAirport);
            return View(layover);
        }

        // GET: Layovers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var layover = await _context.Layovers.FindAsync(id);
            if (layover == null)
            {
                return NotFound();
            }
            ViewData["IdDestinationAirport"] = new SelectList(_context.Airports, "Id", "Id", layover.IdDestinationAirport);
            ViewData["IdFlight"] = new SelectList(_context.Flights, "Id", "Id", layover.IdFlight);
            ViewData["IdOriginAirport"] = new SelectList(_context.Airports, "Id", "Id", layover.IdOriginAirport);
            return View(layover);
        }

        // POST: Layovers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFlight,IdOriginAirport,IdDestinationAirport,ExitTime,EstimatedArrivalTime")] Layover layover)
        {
            if (id != layover.IdFlight)
            {
                return NotFound();
            }

            ModelState.Remove("IdFlightNavigation");
            ModelState.Remove("IdOriginAirportNavigation");
            ModelState.Remove("IdDestinationAirportNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(layover);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LayoverExists(layover.IdFlight))
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
            ViewData["IdDestinationAirport"] = new SelectList(_context.Airports, "Id", "Id", layover.IdDestinationAirport);
            ViewData["IdFlight"] = new SelectList(_context.Flights, "Id", "Id", layover.IdFlight);
            ViewData["IdOriginAirport"] = new SelectList(_context.Airports, "Id", "Id", layover.IdOriginAirport);
            return View(layover);
        }

        // GET: Layovers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var layover = await _context.Layovers
                .Include(l => l.IdDestinationAirportNavigation)
                .Include(l => l.IdFlightNavigation)
                .Include(l => l.IdOriginAirportNavigation)
                .FirstOrDefaultAsync(m => m.IdFlight == id);
            if (layover == null)
            {
                return NotFound();
            }

            return View(layover);
        }

        // POST: Layovers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var layover = await _context.Layovers.FindAsync(id);
            if (layover != null)
            {
                _context.Layovers.Remove(layover);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LayoverExists(int id)
        {
            return _context.Layovers.Any(e => e.IdFlight == id);
        }
    }
}
