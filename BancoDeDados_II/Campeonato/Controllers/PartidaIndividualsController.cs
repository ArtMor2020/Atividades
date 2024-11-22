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
    public class PartidaIndividualsController : Controller
    {
        private readonly CampeonatoContext _context;

        public PartidaIndividualsController(CampeonatoContext context)
        {
            _context = context;
        }

        // GET: PartidaIndividuals
        public async Task<IActionResult> Index()
        {
            var campeonatoContext = _context.PartidaIndividuals.Include(p => p.IdJogadorVencedorNavigation).Include(p => p.IdModalidadeNavigation);
            return View(await campeonatoContext.ToListAsync());
        }

        // GET: PartidaIndividuals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partidaIndividual = await _context.PartidaIndividuals
                .Include(p => p.IdJogadorVencedorNavigation)
                .Include(p => p.IdModalidadeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partidaIndividual == null)
            {
                return NotFound();
            }

            return View(partidaIndividual);
        }

        // GET: PartidaIndividuals/Create
        public IActionResult Create()
        {
            ViewData["IdJogadorVencedor"] = new SelectList(_context.Jogadors, "Id", "Name");
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name");
            return View();
        }

        // POST: PartidaIndividuals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdModalidade,IdJogadorVencedor,PosChaveamento")] PartidaIndividual partidaIndividual)
        {
            ModelState.Remove("JogadorEmPartidaIndividuals");
            ModelState.Remove("PontosPartidaIndividuals");

            if (ModelState.IsValid)
            {
                _context.Add(partidaIndividual);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdJogadorVencedor"] = new SelectList(_context.Jogadors, "Id", "Name", partidaIndividual.IdJogadorVencedor);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", partidaIndividual.IdModalidade);
            return View(partidaIndividual);
        }

        // GET: PartidaIndividuals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partidaIndividual = await _context.PartidaIndividuals.FindAsync(id);
            if (partidaIndividual == null)
            {
                return NotFound();
            }
            ViewData["IdJogadorVencedor"] = new SelectList(_context.Jogadors, "Id", "Name", partidaIndividual.IdJogadorVencedor);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", partidaIndividual.IdModalidade);
            return View(partidaIndividual);
        }

        // POST: PartidaIndividuals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdModalidade,IdJogadorVencedor,PosChaveamento")] PartidaIndividual partidaIndividual)
        {
            if (id != partidaIndividual.Id)
            {
                return NotFound();
            }

            ModelState.Remove("JogadorEmPartidaIndividuals");
            ModelState.Remove("PontosPartidaIndividuals");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(partidaIndividual);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PartidaIndividualExists(partidaIndividual.Id))
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
            ViewData["IdJogadorVencedor"] = new SelectList(_context.Jogadors, "Id", "Name", partidaIndividual.IdJogadorVencedor);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", partidaIndividual.IdModalidade);
            return View(partidaIndividual);
        }

        // GET: PartidaIndividuals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partidaIndividual = await _context.PartidaIndividuals
                .Include(p => p.IdJogadorVencedorNavigation)
                .Include(p => p.IdModalidadeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partidaIndividual == null)
            {
                return NotFound();
            }

            return View(partidaIndividual);
        }

        // POST: PartidaIndividuals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partidaIndividual = await _context.PartidaIndividuals.FindAsync(id);
            if (partidaIndividual != null)
            {
                _context.PartidaIndividuals.Remove(partidaIndividual);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartidaIndividualExists(int id)
        {
            return _context.PartidaIndividuals.Any(e => e.Id == id);
        }
    }
}
