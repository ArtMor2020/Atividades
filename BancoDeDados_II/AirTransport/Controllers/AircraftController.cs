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
    public class AircraftController : Controller
    {
        private readonly AirTransportContext _context;

        public AircraftController(AirTransportContext context)
        {
            _context = context;
        }

        // GET: Aircraft
        public async Task<IActionResult> Index()
        {
            var airTransportContext = _context.Aircraft.Include(a => a.IdCompanyNavigation);
            return View(await airTransportContext.ToListAsync());
        }

        // GET: Aircraft/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircraft
                .Include(a => a.IdCompanyNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // GET: Aircraft/Create
        public IActionResult Create()
        {
            ViewBag.IdCompany = new SelectList(_context.Companies, "Id", "Name");
            return View();
        }

        // POST: Aircraft/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Model,NumberOfSeats,IdCompany")] Aircraft aircraft)
        {
            ModelState.Remove("IdCompanyNavigation"); // finaly

            if (ModelState.IsValid)
            {
                _context.Add(aircraft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.IdCompany = new SelectList(_context.Companies, "Id", "Name", aircraft.IdCompany);
            return View(aircraft);
        }

        // GET: Aircraft/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircraft.FindAsync(id);
            if (aircraft == null)
            {
                return NotFound();
            }
            ViewData["IdCompany"] = new SelectList(_context.Companies, "Id", "Name", aircraft.IdCompany);
            return View(aircraft);
        }

        // POST: Aircraft/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Model,NumberOfSeats,IdCompany")] Aircraft aircraft)
        {
            if (id != aircraft.Id)
            {
                return NotFound();
            }

            ModelState.Remove("IdCompanyNavigation"); // finaly

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aircraft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AircraftExists(aircraft.Id))
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
            ViewData["IdCompany"] = new SelectList(_context.Companies, "Id", "Id", aircraft.IdCompany);
            return View(aircraft);
        }

        // GET: Aircraft/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _context.Aircraft
                .Include(a => a.IdCompanyNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // POST: Aircraft/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aircraft = await _context.Aircraft.FindAsync(id);
            if (aircraft != null)
            {
                _context.Aircraft.Remove(aircraft);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AircraftExists(int id)
        {
            return _context.Aircraft.Any(e => e.Id == id);
        }
    }
}
