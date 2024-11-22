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
    public class RegistroModalidadeEquipesController : Controller
    {
        private readonly CampeonatoContext _context;

        public RegistroModalidadeEquipesController(CampeonatoContext context)
        {
            _context = context;
        }

        // GET: RegistroModalidadeEquipes
        public async Task<IActionResult> Index()
        {
            var campeonatoContext = _context.RegistroModalidadeEquipes.Include(r => r.IdEquipeNavigation).Include(r => r.IdModalidadeNavigation);
            return View(await campeonatoContext.ToListAsync());
        }

        // GET: RegistroModalidadeEquipes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroModalidadeEquipe = await _context.RegistroModalidadeEquipes
                .Include(r => r.IdEquipeNavigation)
                .Include(r => r.IdModalidadeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroModalidadeEquipe == null)
            {
                return NotFound();
            }

            return View(registroModalidadeEquipe);
        }

        // GET: RegistroModalidadeEquipes/Create
        public IActionResult Create()
        {
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name");
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades.Where(m => !m.Individual), "Id", "Name");
            return View();
        }

        // POST: RegistroModalidadeEquipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdModalidade,IdEquipe")] RegistroModalidadeEquipe registroModalidadeEquipe)
        {
            ModelState.Remove("IdEquipeNavigation");
            ModelState.Remove("IdModalidadeNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(registroModalidadeEquipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", registroModalidadeEquipe.IdEquipe);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", registroModalidadeEquipe.IdModalidade);
            return View(registroModalidadeEquipe);
        }

        // GET: RegistroModalidadeEquipes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroModalidadeEquipe = await _context.RegistroModalidadeEquipes.FindAsync(id);
            if (registroModalidadeEquipe == null)
            {
                return NotFound();
            }
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", registroModalidadeEquipe.IdEquipe);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades.Where(m => !m.Individual), "Id", "Name", registroModalidadeEquipe.IdModalidade);
            return View(registroModalidadeEquipe);
        }

        // POST: RegistroModalidadeEquipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdModalidade,IdEquipe")] RegistroModalidadeEquipe registroModalidadeEquipe)
        {
            if (id != registroModalidadeEquipe.Id)
            {
                return NotFound();
            }

            ModelState.Remove("IdEquipeNavigation");
            ModelState.Remove("IdModalidadeNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroModalidadeEquipe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroModalidadeEquipeExists(registroModalidadeEquipe.Id))
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
            ViewData["IdEquipe"] = new SelectList(_context.Equipes, "Id", "Name", registroModalidadeEquipe.IdEquipe);
            ViewData["IdModalidade"] = new SelectList(_context.Modalidades, "Id", "Name", registroModalidadeEquipe.IdModalidade);
            return View(registroModalidadeEquipe);
        }

        // GET: RegistroModalidadeEquipes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroModalidadeEquipe = await _context.RegistroModalidadeEquipes
                .Include(r => r.IdEquipeNavigation)
                .Include(r => r.IdModalidadeNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroModalidadeEquipe == null)
            {
                return NotFound();
            }

            return View(registroModalidadeEquipe);
        }

        // POST: RegistroModalidadeEquipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registroModalidadeEquipe = await _context.RegistroModalidadeEquipes.FindAsync(id);
            if (registroModalidadeEquipe != null)
            {
                _context.RegistroModalidadeEquipes.Remove(registroModalidadeEquipe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroModalidadeEquipeExists(int id)
        {
            return _context.RegistroModalidadeEquipes.Any(e => e.Id == id);
        }
    }
}
