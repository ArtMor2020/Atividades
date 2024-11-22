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
    public class PontosCampeonatoesController : Controller
    {
        private readonly CampeonatoContext _context;

        public PontosCampeonatoesController(CampeonatoContext context)
        {
            _context = context;
        }

        // GET: PontosCampeonatoes
        public async Task<IActionResult> Index()
        {
            var campeonatoContext = _context.PontosCampeonatos.Include(p => p.IdEquipeNavigation).Include(p => p.IdJogadorNavigation).Include(p => p.IdModalidadeNavigation);
            return View(await campeonatoContext.ToListAsync());
        }

        // GET: PontosCampeonatoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontosCampeonato = await _context.PontosCampeonatos
                .Include(p => p.IdEquipeNavigation)
                .Include(p => p.IdJogadorNavigation)
                .Include(p => p.IdModalidadeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontosCampeonato == null)
            {
                return NotFound();
            }

            return View(pontosCampeonato);
        }

        // GET: PontosCampeonatoes/Create
        public IActionResult Create()
        {
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name");
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name");
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name");
            return View();
        }

        // POST: PontosCampeonatoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdModalidade,IdEquipe,IdJogador,Pontos")] PontosCampeonato pontosCampeonato)
        {
            ModelState.Remove("IdEquipeNavigation");
            ModelState.Remove("IdJogadorNavigation");
            ModelState.Remove("IdModalidadeNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(pontosCampeonato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", pontosCampeonato.IdEquipe);
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", pontosCampeonato.IdJogador);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", pontosCampeonato.IdModalidade);
            return View(pontosCampeonato);
        }

        // GET: PontosCampeonatoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontosCampeonato = await _context.PontosCampeonatos.FindAsync(id);
            if (pontosCampeonato == null)
            {
                return NotFound();
            }
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", pontosCampeonato.IdEquipe);
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", pontosCampeonato.IdJogador);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", pontosCampeonato.IdModalidade);
            return View(pontosCampeonato);
        }

        // POST: PontosCampeonatoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdModalidade,IdEquipe,IdJogador,Pontos")] PontosCampeonato pontosCampeonato)
        {
            if (id != pontosCampeonato.Id)
            {
                return NotFound();
            }

            ModelState.Remove("IdEquipeNavigation");
            ModelState.Remove("IdJogadorNavigation");
            ModelState.Remove("IdModalidadeNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pontosCampeonato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontosCampeonatoExists(pontosCampeonato.Id))
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
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", pontosCampeonato.IdEquipe);
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", pontosCampeonato.IdJogador);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", pontosCampeonato.IdModalidade);
            return View(pontosCampeonato);
        }

        // GET: PontosCampeonatoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontosCampeonato = await _context.PontosCampeonatos
                .Include(p => p.IdEquipeNavigation)
                .Include(p => p.IdJogadorNavigation)
                .Include(p => p.IdModalidadeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontosCampeonato == null)
            {
                return NotFound();
            }

            return View(pontosCampeonato);
        }

        // POST: PontosCampeonatoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pontosCampeonato = await _context.PontosCampeonatos.FindAsync(id);
            if (pontosCampeonato != null)
            {
                _context.PontosCampeonatos.Remove(pontosCampeonato);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontosCampeonatoExists(int id)
        {
            return _context.PontosCampeonatos.Any(e => e.Id == id);
        }
    }
}
