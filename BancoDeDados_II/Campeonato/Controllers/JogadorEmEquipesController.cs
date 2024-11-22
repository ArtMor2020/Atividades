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
    public class JogadorEmEquipesController : Controller
    {
        private readonly CampeonatoContext _context;

        public JogadorEmEquipesController(CampeonatoContext context)
        {
            _context = context;
        }

        // GET: JogadorEmEquipes
        public async Task<IActionResult> Index()
        {
            var campeonatoContext = _context.JogadorEmEquipes.Include(j => j.IdEquipeNavigation).Include(j => j.IdJogadorNavigation);
            return View(await campeonatoContext.ToListAsync());
        }

        // GET: JogadorEmEquipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorEmEquipe = await _context.JogadorEmEquipes
                .Include(j => j.IdEquipeNavigation)
                .Include(j => j.IdJogadorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogadorEmEquipe == null)
            {
                return NotFound();
            }

            return View(jogadorEmEquipe);
        }

        // GET: JogadorEmEquipes/Create
        public IActionResult Create()
        {
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name");
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name");
            return View();
        }

        // POST: JogadorEmEquipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdJogador,IdEquipe")] JogadorEmEquipe jogadorEmEquipe)
        {
            ModelState.Remove("IdEquipeNavigation");
            ModelState.Remove("IdJogadorNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(jogadorEmEquipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", jogadorEmEquipe.IdEquipe);
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", jogadorEmEquipe.IdJogador);
            return View(jogadorEmEquipe);
        }

        // GET: JogadorEmEquipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorEmEquipe = await _context.JogadorEmEquipes.FindAsync(id);
            if (jogadorEmEquipe == null)
            {
                return NotFound();
            }
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", jogadorEmEquipe.IdEquipe);
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", jogadorEmEquipe.IdJogador);
            return View(jogadorEmEquipe);
        }

        // POST: JogadorEmEquipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdJogador,IdEquipe")] JogadorEmEquipe jogadorEmEquipe)
        {
            if (id != jogadorEmEquipe.Id)
            {
                return NotFound();
            }

            ModelState.Remove("IdEquipeNavigation");
            ModelState.Remove("IdJogadorNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jogadorEmEquipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JogadorEmEquipeExists(jogadorEmEquipe.Id))
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
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", jogadorEmEquipe.IdEquipe);
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", jogadorEmEquipe.IdJogador);
            return View(jogadorEmEquipe);
        }

        // GET: JogadorEmEquipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogadorEmEquipe = await _context.JogadorEmEquipes
                .Include(j => j.IdEquipeNavigation)
                .Include(j => j.IdJogadorNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jogadorEmEquipe == null)
            {
                return NotFound();
            }

            return View(jogadorEmEquipe);
        }

        // POST: JogadorEmEquipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jogadorEmEquipe = await _context.JogadorEmEquipes.FindAsync(id);
            if (jogadorEmEquipe != null)
            {
                _context.JogadorEmEquipes.Remove(jogadorEmEquipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JogadorEmEquipeExists(int id)
        {
            return _context.JogadorEmEquipes.Any(e => e.Id == id);
        }
    }
}
