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
    public class JogadorEmPartidaIndividualsController : Controller
    {
        private readonly CampeonatoContext _context;

        public JogadorEmPartidaIndividualsController(CampeonatoContext context)
        {
            _context = context;
        }

        // GET: JogadorEmPartidaIndividuals
        public async Task<IActionResult> Index()
        {
            var campeonatoContext = _context.JogadorEmPartidaIndividuals.Include(j => j.IdJogadorNavigation).Include(j => j.IdPartidaIndividualNavigation);
            return View(await campeonatoContext.ToListAsync());
        }

        // GET: JogadorEmPartidaIndividuals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorEmPartidaIndividual = await _context.JogadorEmPartidaIndividuals
                .Include(j => j.IdJogadorNavigation)
                .Include(j => j.IdPartidaIndividualNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogadorEmPartidaIndividual == null)
            {
                return NotFound();
            }

            return View(jogadorEmPartidaIndividual);
        }

        // GET: JogadorEmPartidaIndividuals/Create
        public IActionResult Create()
        {
            ModelState.Remove("IdJogadorNavigation");
            ModelState.Remove("IdPartidaIndividualNavigation");

            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name");
            ViewData["IdPartidaIndividual"] = new SelectList(_context.PartidaIndividuals, "Id", "Id");
            return View();
        }

        // POST: JogadorEmPartidaIndividuals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPartidaIndividual,IdJogador")] JogadorEmPartidaIndividual jogadorEmPartidaIndividual)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jogadorEmPartidaIndividual);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", jogadorEmPartidaIndividual.IdJogador);
            ViewData["IdPartidaIndividual"] = new SelectList(_context.PartidaIndividuals, "Id", "Id", jogadorEmPartidaIndividual.IdPartidaIndividual);
            return View(jogadorEmPartidaIndividual);
        }

        // GET: JogadorEmPartidaIndividuals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorEmPartidaIndividual = await _context.JogadorEmPartidaIndividuals.FindAsync(id);
            if (jogadorEmPartidaIndividual == null)
            {
                return NotFound();
            }
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", jogadorEmPartidaIndividual.IdJogador);
            ViewData["IdPartidaIndividual"] = new SelectList(_context.PartidaIndividuals, "Id", "Id", jogadorEmPartidaIndividual.IdPartidaIndividual);
            return View(jogadorEmPartidaIndividual);
        }

        // POST: JogadorEmPartidaIndividuals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPartidaIndividual,IdJogador")] JogadorEmPartidaIndividual jogadorEmPartidaIndividual)
        {
            if (id != jogadorEmPartidaIndividual.Id)
            {
                return NotFound();
            }

            ModelState.Remove("IdJogadorNavigation");
            ModelState.Remove("IdPartidaIndividualNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogadorEmPartidaIndividual);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogadorEmPartidaIndividualExists(jogadorEmPartidaIndividual.Id))
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
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", jogadorEmPartidaIndividual.IdJogador);
            ViewData["IdPartidaIndividual"] = new SelectList(_context.PartidaIndividuals, "Id", "Id", jogadorEmPartidaIndividual.IdPartidaIndividual);
            return View(jogadorEmPartidaIndividual);
        }

        // GET: JogadorEmPartidaIndividuals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorEmPartidaIndividual = await _context.JogadorEmPartidaIndividuals
                .Include(j => j.IdJogadorNavigation)
                .Include(j => j.IdPartidaIndividualNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogadorEmPartidaIndividual == null)
            {
                return NotFound();
            }

            return View(jogadorEmPartidaIndividual);
        }

        // POST: JogadorEmPartidaIndividuals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogadorEmPartidaIndividual = await _context.JogadorEmPartidaIndividuals.FindAsync(id);
            if (jogadorEmPartidaIndividual != null)
            {
                _context.JogadorEmPartidaIndividuals.Remove(jogadorEmPartidaIndividual);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogadorEmPartidaIndividualExists(int id)
        {
            return _context.JogadorEmPartidaIndividuals.Any(e => e.Id == id);
        }
    }
}
