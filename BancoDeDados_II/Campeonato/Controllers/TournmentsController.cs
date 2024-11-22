using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Campeonato.Models;

namespace Campeonato.Controllers
{
    public class TournmentsController : Controller
    {
        private readonly CampeonatoContext _context;

        public TournmentsController(CampeonatoContext context)
        {
            _context = context;
        }

        // GET: Tournments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tournments.ToListAsync());
        }

        // GET: Tournments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournment = await _context.Tournments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournment == null)
            {
                return NotFound();
            }

            return View(tournment);
        }

        // GET: Tournments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tournments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Status")] Tournment tournment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tournment);
        }

        // GET: Tournments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournment = await _context.Tournments.FindAsync(id);
            if (tournment == null)
            {
                return NotFound();
            }
            return View(tournment);
        }

        // POST: Tournments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status")] Tournment tournment)
        {
            if (id != tournment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tournment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournmentExists(tournment.Id))
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
            return View(tournment);
        }

        // GET: Tournments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournment = await _context.Tournments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tournment == null)
            {
                return NotFound();
            }

            return View(tournment);
        }

        // POST: Tournments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tournment = await _context.Tournments.FindAsync(id);
            if (tournment != null)
            {
                _context.Tournments.Remove(tournment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournmentExists(int id)
        {
            return _context.Tournments.Any(e => e.Id == id);
        }
    }
}
