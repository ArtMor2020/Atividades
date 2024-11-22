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
    public class RegistroModalidadeIndividualsController : Controller
    {
        private readonly CampeonatoContext _context;

        public RegistroModalidadeIndividualsController(CampeonatoContext context)
        {
            _context = context;
        }

        // GET: RegistroModalidadeIndividuals
        public async Task<IActionResult> Index()
        {
            var campeonatoContext = _context.RegistroModalidadeIndividuals.Include(r => r.IdJogadorNavigation).Include(r => r.IdModalidadeNavigation);
            return View(await campeonatoContext.ToListAsync());
        }

        // GET: RegistroModalidadeIndividuals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroModalidadeIndividual = await _context.RegistroModalidadeIndividuals
                .Include(r => r.IdJogadorNavigation)
                .Include(r => r.IdModalidadeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroModalidadeIndividual == null)
            {
                return NotFound();
            }

            return View(registroModalidadeIndividual);
        }

        // GET: RegistroModalidadeIndividuals/Create
        public IActionResult Create()
        {
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name");
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades.Where(m => m.Individual), "Id", "Name");
            return View();
        }

        // POST: RegistroModalidadeIndividuals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdModalidade,IdJogador")] RegistroModalidadeIndividual registroModalidadeIndividual)
        {
            ModelState.Remove("IdJogadorNavigation");
            ModelState.Remove("IdModalidadeNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(registroModalidadeIndividual);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", registroModalidadeIndividual.IdJogador);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", registroModalidadeIndividual.IdModalidade);
            return View(registroModalidadeIndividual);
        }

        // GET: RegistroModalidadeIndividuals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroModalidadeIndividual = await _context.RegistroModalidadeIndividuals.FindAsync(id);
            if (registroModalidadeIndividual == null)
            {
                return NotFound();
            }
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", registroModalidadeIndividual.IdJogador);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades.Where(m => m.Individual), "Id", "Name", registroModalidadeIndividual.IdModalidade);
            return View(registroModalidadeIndividual);
        }

        // POST: RegistroModalidadeIndividuals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdModalidade,IdJogador")] RegistroModalidadeIndividual registroModalidadeIndividual)
        {
            if (id != registroModalidadeIndividual.Id)
            {
                return NotFound();
            }

            ModelState.Remove("IdJogadorNavigation");
            ModelState.Remove("IdModalidadeNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroModalidadeIndividual);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroModalidadeIndividualExists(registroModalidadeIndividual.Id))
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
            ViewData["IdJogador"] = new SelectList(_context.Jogadors, "Id", "Name", registroModalidadeIndividual.IdJogador);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", registroModalidadeIndividual.IdModalidade);
            return View(registroModalidadeIndividual);
        }

        // GET: RegistroModalidadeIndividuals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroModalidadeIndividual = await _context.RegistroModalidadeIndividuals
                .Include(r => r.IdJogadorNavigation)
                .Include(r => r.IdModalidadeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroModalidadeIndividual == null)
            {
                return NotFound();
            }

            return View(registroModalidadeIndividual);
        }

        // POST: RegistroModalidadeIndividuals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroModalidadeIndividual = await _context.RegistroModalidadeIndividuals.FindAsync(id);
            if (registroModalidadeIndividual != null)
            {
                _context.RegistroModalidadeIndividuals.Remove(registroModalidadeIndividual);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroModalidadeIndividualExists(int id)
        {
            return _context.RegistroModalidadeIndividuals.Any(e => e.Id == id);
        }
    }
}
