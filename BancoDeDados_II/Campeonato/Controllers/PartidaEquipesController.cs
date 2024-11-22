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
    public class PartidaEquipesController : Controller
    {
        private readonly CampeonatoContext _context;

        public PartidaEquipesController(CampeonatoContext context)
        {
            _context = context;
        }

        // GET: PartidaEquipes

        public async Task<IActionResult> Index()
        {
            var campeonatoContext = _context.PartidaEquipes.Include(p => p.IdEquipeVencedoraNavigation).Include(p => p.IdModalidadeNavigation);
            return View(await campeonatoContext.ToListAsync());
        }

        // GET: PartidaEquipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partidaEquipe = await _context.PartidaEquipes
                .Include(p => p.IdEquipeVencedoraNavigation)
                .Include(p => p.IdModalidadeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partidaEquipe == null)
            {
                return NotFound();
            }

            return View(partidaEquipe);
        }

        // GET: PartidaEquipes/Create
        public IActionResult Create()
        {
            ViewData["IdEquipeVencedora"] = new SelectList(_context.Equipes, "Id", "Name");
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name");
            return View();
        }

        // POST: PartidaEquipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdModalidade,IdEquipeVencedora,PosChaveamento")] PartidaEquipe partidaEquipe)
        {
            ModelState.Remove("IdEquipeVencedoraNavigation");
            ModelState.Remove("IdModalidadeNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(partidaEquipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEquipeVencedora"] = new SelectList(_context.Equipes, "Id", "Name", partidaEquipe.IdEquipeVencedora);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", partidaEquipe.IdModalidade);
            return View(partidaEquipe);
        }

        // GET: PartidaEquipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partidaEquipe = await _context.PartidaEquipes.FindAsync(id);
            if (partidaEquipe == null)
            {
                return NotFound();
            }
            ViewData["IdEquipeVencedora"] = new SelectList(_context.Equipes, "Id", "Name", partidaEquipe.IdEquipeVencedora);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", partidaEquipe.IdModalidade);
            return View(partidaEquipe);
        }

        // POST: PartidaEquipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdModalidade,IdEquipeVencedora,PosChaveamento")] PartidaEquipe partidaEquipe)
        {
            if (id != partidaEquipe.Id)
            {
                return NotFound();
            }

            ModelState.Remove("IdEquipeVencedoraNavigation");
            ModelState.Remove("IdModalidadeNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partidaEquipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartidaEquipeExists(partidaEquipe.Id))
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
            ViewData["IdEquipeVencedora"] = new SelectList(_context.Equipes, "Id", "Name", partidaEquipe.IdEquipeVencedora);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", partidaEquipe.IdModalidade);
            return View(partidaEquipe);
        }

        // GET: PartidaEquipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partidaEquipe = await _context.PartidaEquipes
                .Include(p => p.IdEquipeVencedoraNavigation)
                .Include(p => p.IdModalidadeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partidaEquipe == null)
            {
                return NotFound();
            }

            return View(partidaEquipe);
        }

        // POST: PartidaEquipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partidaEquipe = await _context.PartidaEquipes.FindAsync(id);
            if (partidaEquipe != null)
            {
                _context.PartidaEquipes.Remove(partidaEquipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartidaEquipeExists(int id)
        {
            return _context.PartidaEquipes.Any(e => e.Id == id);
        }
    }
}
