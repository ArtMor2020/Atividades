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
    public class EquipeEmPartidumsController : Controller
    {
        private readonly CampeonatoContext _context;

        public EquipeEmPartidumsController(CampeonatoContext context)
        {
            _context = context;
        }

        // GET: EquipeEmPartidums
        public async Task<IActionResult> Index()
        {
            var campeonatoContext = _context.EquipeEmPartida.Include(e => e.IdEquipeNavigation).Include(e => e.IdPartidaEquipeNavigation);
            return View(await campeonatoContext.ToListAsync());
        }

        // GET: EquipeEmPartidums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipeEmPartidum = await _context.EquipeEmPartida
                .Include(e => e.IdEquipeNavigation)
                .Include(e => e.IdPartidaEquipeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipeEmPartidum == null)
            {
                return NotFound();
            }

            return View(equipeEmPartidum);
        }

        // GET: EquipeEmPartidums/Create
        public IActionResult Create()
        {
            ModelState.Remove("IdEquipeNavigation");
            ModelState.Remove("IdPartidaEquipeNavigation");

            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name");
            ViewData["IdPartidaEquipe"] = new SelectList(_context.PartidaEquipes, "Id", "Id");
            return View();
        }

        // POST: EquipeEmPartidums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPartidaEquipe,IdEquipe")] EquipeEmPartidum equipeEmPartidum)
        {
            ModelState.Remove("IdEquipeNavigation");
            ModelState.Remove("IdPartidaEquipeNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(equipeEmPartidum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", equipeEmPartidum.IdEquipe);
            ViewData["IdPartidaEquipe"] = new SelectList(_context.PartidaEquipes, "Id", "Id", equipeEmPartidum.IdPartidaEquipe);
            return View(equipeEmPartidum);
        }

        // GET: EquipeEmPartidums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipeEmPartidum = await _context.EquipeEmPartida.FindAsync(id);
            if (equipeEmPartidum == null)
            {
                return NotFound();
            }
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", equipeEmPartidum.IdEquipe);
            ViewData["IdPartidaEquipe"] = new SelectList(_context.PartidaEquipes, "Id", "Id", equipeEmPartidum.IdPartidaEquipe);
            return View(equipeEmPartidum);
        }

        // POST: EquipeEmPartidums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPartidaEquipe,IdEquipe")] EquipeEmPartidum equipeEmPartidum)
        {
            if (id != equipeEmPartidum.Id)
            {
                return NotFound();
            }

            ModelState.Remove("IdEquipeNavigation");
            ModelState.Remove("IdPartidaEquipeNavigation");

            ModelState.Remove("IdEquipeNavigation");
            ModelState.Remove("IdPartidaEquipeNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipeEmPartidum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipeEmPartidumExists(equipeEmPartidum.Id))
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
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", equipeEmPartidum.IdEquipe);
            ViewData["IdPartidaEquipe"] = new SelectList(_context.PartidaEquipes, "Id", "Id", equipeEmPartidum.IdPartidaEquipe);
            return View(equipeEmPartidum);
        }

        // GET: EquipeEmPartidums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipeEmPartidum = await _context.EquipeEmPartida
                .Include(e => e.IdEquipeNavigation)
                .Include(e => e.IdPartidaEquipeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipeEmPartidum == null)
            {
                return NotFound();
            }

            return View(equipeEmPartidum);
        }

        // POST: EquipeEmPartidums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipeEmPartidum = await _context.EquipeEmPartida.FindAsync(id);
            if (equipeEmPartidum != null)
            {
                _context.EquipeEmPartida.Remove(equipeEmPartidum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipeEmPartidumExists(int id)
        {
            return _context.EquipeEmPartida.Any(e => e.Id == id);
        }
    }
}
