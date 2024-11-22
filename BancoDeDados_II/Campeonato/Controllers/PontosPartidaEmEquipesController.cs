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
    public class PontosPartidaEmEquipesController : Controller
    {
        private readonly CampeonatoContext _context;

        public PontosPartidaEmEquipesController(CampeonatoContext context)
        {
            _context = context;
        }

        // GET: PontosPartidaEmEquipes
        public async Task<IActionResult> Index()
        {
            var campeonatoContext = _context.PontosPartidaEmEquipes.Include(p => p.IdEquipeNavigation).Include(p => p.IdPartidaEmEquipeNavigation);
            return View(await campeonatoContext.ToListAsync());
        }

        // GET: PontosPartidaEmEquipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontosPartidaEmEquipe = await _context.PontosPartidaEmEquipes
                .Include(p => p.IdEquipeNavigation)
                .Include(p => p.IdPartidaEmEquipeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontosPartidaEmEquipe == null)
            {
                return NotFound();
            }

            return View(pontosPartidaEmEquipe);
        }

        // GET: PontosPartidaEmEquipes/Create
        public IActionResult Create()
        {
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name");
            ViewData["IdPartidaEmEquipe"] = new SelectList(_context.PartidaEquipes, "Id", "Id");
            return View();
        }

        // POST: PontosPartidaEmEquipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPartidaEmEquipe,IdEquipe,Pontos")] PontosPartidaEmEquipe pontosPartidaEmEquipe)
        {
            ModelState.Remove("IdEquipeNavigation");
            ModelState.Remove("IdPartidaEmEquipeNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(pontosPartidaEmEquipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", pontosPartidaEmEquipe.IdEquipe);
            ViewData["IdPartidaEmEquipe"] = new SelectList(_context.PartidaEquipes, "Id", "Id", pontosPartidaEmEquipe.IdPartidaEmEquipe);
            return View(pontosPartidaEmEquipe);
        }

        // GET: PontosPartidaEmEquipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontosPartidaEmEquipe = await _context.PontosPartidaEmEquipes.FindAsync(id);
            if (pontosPartidaEmEquipe == null)
            {
                return NotFound();
            }
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", pontosPartidaEmEquipe.IdEquipe);
            ViewData["IdPartidaEmEquipe"] = new SelectList(_context.PartidaEquipes, "Id", "Id", pontosPartidaEmEquipe.IdPartidaEmEquipe);
            return View(pontosPartidaEmEquipe);
        }

        // POST: PontosPartidaEmEquipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPartidaEmEquipe,IdEquipe,Pontos")] PontosPartidaEmEquipe pontosPartidaEmEquipe)
        {
            if (id != pontosPartidaEmEquipe.Id)
            {
                return NotFound();
            }

            ModelState.Remove("IdEquipeNavigation");
            ModelState.Remove("IdPartidaEmEquipeNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pontosPartidaEmEquipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontosPartidaEmEquipeExists(pontosPartidaEmEquipe.Id))
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
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", pontosPartidaEmEquipe.IdEquipe);
            ViewData["IdPartidaEmEquipe"] = new SelectList(_context.PartidaEquipes, "Id", "Id", pontosPartidaEmEquipe.IdPartidaEmEquipe);
            return View(pontosPartidaEmEquipe);
        }

        // GET: PontosPartidaEmEquipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontosPartidaEmEquipe = await _context.PontosPartidaEmEquipes
                .Include(p => p.IdEquipeNavigation)
                .Include(p => p.IdPartidaEmEquipeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontosPartidaEmEquipe == null)
            {
                return NotFound();
            }

            return View(pontosPartidaEmEquipe);
        }

        // POST: PontosPartidaEmEquipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pontosPartidaEmEquipe = await _context.PontosPartidaEmEquipes.FindAsync(id);
            if (pontosPartidaEmEquipe != null)
            {
                _context.PontosPartidaEmEquipes.Remove(pontosPartidaEmEquipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontosPartidaEmEquipeExists(int id)
        {
            return _context.PontosPartidaEmEquipes.Any(e => e.Id == id);
        }
    }
}
