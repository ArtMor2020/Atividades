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
    public class ListPassengersFlightsController : Controller
    {
        private readonly AirTransportContext _context;

        public ListPassengersFlightsController(AirTransportContext context)
        {
            _context = context;
        }

        // GET: ListPassengersFlights
        public async Task<IActionResult> Index()
        {
            var airTransportContext = _context.ListPassengersFlights.Include(l => l.IdFlightNavigation).Include(l => l.IdPassengerNavigation);
            return View(await airTransportContext.ToListAsync());
        }

        // GET: ListPassengersFlights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listPassengersFlight = await _context.ListPassengersFlights
                .Include(l => l.IdFlightNavigation)
                .Include(l => l.IdPassengerNavigation)
                .FirstOrDefaultAsync(m => m.IdFlight == id);
            if (listPassengersFlight == null)
            {
                return NotFound();
            }

            return View(listPassengersFlight);
        }

        // GET: ListPassengersFlights/Create
        public IActionResult Create()
        {
            ViewData["IdFlight"] = new SelectList(_context.Flights, "Id", "Id");
            ViewData["IdPassenger"] = new SelectList(_context.Passengers, "Id", "Id");
            return View();
        }

        // POST: ListPassengersFlights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFlight,IdPassenger,IsWindowSeat,IsRight,SeatNumber")] ListPassengersFlight listPassengersFlight)
        {
            ModelState.Remove("IdFlightNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(listPassengersFlight);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFlight"] = new SelectList(_context.Flights, "Id", "Id", listPassengersFlight.IdFlight);
            ViewData["IdPassenger"] = new SelectList(_context.Passengers, "Id", "Id", listPassengersFlight.IdPassenger);
            return View(listPassengersFlight);
        }

        // GET: ListPassengersFlights/Edit/5
        public async Task<IActionResult> Edit(int? idFlight, int? idPassenger)
        {
            if (idFlight == null || idPassenger == null)
            {
                return NotFound();
            }

            var listPassengersFlight = await _context.ListPassengersFlights.FindAsync(idFlight, idPassenger);
            if (listPassengersFlight == null)
            {
                return NotFound();
            }
            ViewData["IdFlight"] = new SelectList(_context.Flights, "Id", "Id", listPassengersFlight.IdFlight);
            ViewData["IdPassenger"] = new SelectList(_context.Passengers, "Id", "Id", listPassengersFlight.IdPassenger);
            return View(listPassengersFlight);
        }

        // POST: ListPassengersFlights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int idFlight, int idPassenger, [Bind("IdFlight,IdPassenger,IsWindowSeat,IsRight,SeatNumber")] ListPassengersFlight listPassengersFlight)
        {
            if (idFlight != listPassengersFlight.IdFlight || idPassenger != listPassengersFlight.IdPassenger)
            {
                return NotFound();
            }

            ModelState.Remove("IdFlightNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listPassengersFlight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListPassengersFlightExists(listPassengersFlight.IdFlight))
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
            ViewData["IdFlight"] = new SelectList(_context.Flights, "Id", "Id", listPassengersFlight.IdFlight);
            ViewData["IdPassenger"] = new SelectList(_context.Passengers, "Id", "Id", listPassengersFlight.IdPassenger);
            return View(listPassengersFlight);
        }

        // GET: ListPassengersFlights/Delete/5
        [HttpGet]
        public async Task<IActionResult> Delete(int? idFlight, int? idPassenger)
        {
            if (idFlight == null || idPassenger == null)
            {
                return NotFound();
            }

            var listPassengersFlight = await _context.ListPassengersFlights
                .Include(l => l.IdFlightNavigation)
                .Include(l => l.IdPassengerNavigation)
                .FirstOrDefaultAsync(m => m.IdFlight == idFlight);
            if (listPassengersFlight == null)
            {
                return NotFound();
            }

            return View(listPassengersFlight);
        }

        // POST: ListPassengersFlights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int idFlight, int idPassenger)
        {
            var listPassengersFlight = await _context.ListPassengersFlights.FindAsync(idFlight, idPassenger);
            if (listPassengersFlight != null)
            {
                _context.ListPassengersFlights.Remove(listPassengersFlight);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListPassengersFlightExists(int id)
        {
            return _context.ListPassengersFlights.Any(e => e.IdFlight == id);
        }
    }
}
