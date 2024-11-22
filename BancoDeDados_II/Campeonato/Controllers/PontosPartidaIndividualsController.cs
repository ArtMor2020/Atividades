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
    public class PontosPartidaIndividualsController : Controller
    {
        private readonly CampeonatoContext _context;

        public PontosPartidaIndividualsController(CampeonatoContext context)
        {
            _context = context;
        }

        // GET: PontosPartidaIndividuals
        public async Task<IActionResult> Index()
        {
            var campeonatoContext = _context.PontosPartidaIndividuals.Include(p => p.IdJogadorNavigation).Include(p => p.IdPartidaIndividualNavigation);
            return View(await campeonatoContext.ToListAsync());
        }

        // GET: PontosPartidaIndividuals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontosPartidaIndividual = await _context.PontosPartidaIndividuals
                .Include(p => p.IdJogadorNavigation)
                .Include(p => p.IdPartidaIndividualNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontosPartidaIndividual == null)
            {
                return NotFound();
            }

            return View(pontosPartidaIndividual);
        }

        // GET: PontosPartidaIndividuals/Create
        public IActionResult Create()
        {
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name");
            ViewData["IdPartidaIndividual"] = new SelectList(_context.PartidaIndividuals, "Id", "Id");
            return View();
        }

        // POST: PontosPartidaIndividuals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPartidaIndividual,IdJogador,Pontos")] PontosPartidaIndividual pontosPartidaIndividual)
        {
            ModelState.Remove("IdJogadorNavigation");
            ModelState.Remove("IdPartidaIndividualNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(pontosPartidaIndividual);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", pontosPartidaIndividual.IdJogador);
            ViewData["IdPartidaIndividual"] = new SelectList(_context.PartidaIndividuals, "Id", "Id", pontosPartidaIndividual.IdPartidaIndividual);
            return View(pontosPartidaIndividual);
        }

        // GET: PontosPartidaIndividuals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontosPartidaIndividual = await _context.PontosPartidaIndividuals.FindAsync(id);
            if (pontosPartidaIndividual == null)
            {
                return NotFound();
            }
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", pontosPartidaIndividual.IdJogador);
            ViewData["IdPartidaIndividual"] = new SelectList(_context.PartidaIndividuals, "Id", "Id", pontosPartidaIndividual.IdPartidaIndividual);
            return View(pontosPartidaIndividual);
        }

        // POST: PontosPartidaIndividuals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPartidaIndividual,IdJogador,Pontos")] PontosPartidaIndividual pontosPartidaIndividual)
        {
            if (id != pontosPartidaIndividual.Id)
            {
                return NotFound();
            }

            ModelState.Remove("IdJogadorNavigation");
            ModelState.Remove("IdPartidaIndividualNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pontosPartidaIndividual);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontosPartidaIndividualExists(pontosPartidaIndividual.Id))
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
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", pontosPartidaIndividual.IdJogador);
            ViewData["IdPartidaIndividual"] = new SelectList(_context.PartidaIndividuals, "Id", "Id", pontosPartidaIndividual.IdPartidaIndividual);
            return View(pontosPartidaIndividual);
        }

        // GET: PontosPartidaIndividuals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontosPartidaIndividual = await _context.PontosPartidaIndividuals
                .Include(p => p.IdJogadorNavigation)
                .Include(p => p.IdPartidaIndividualNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontosPartidaIndividual == null)
            {
                return NotFound();
            }

            return View(pontosPartidaIndividual);
        }

        // POST: PontosPartidaIndividuals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pontosPartidaIndividual = await _context.PontosPartidaIndividuals.FindAsync(id);
            if (pontosPartidaIndividual != null)
            {
                _context.PontosPartidaIndividuals.Remove(pontosPartidaIndividual);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontosPartidaIndividualExists(int id)
        {
            return _context.PontosPartidaIndividuals.Any(e => e.Id == id);
        }
    }
}
