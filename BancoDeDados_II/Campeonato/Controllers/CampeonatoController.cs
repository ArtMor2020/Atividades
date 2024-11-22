using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Campeonato.Models;

namespace Campeonato.Controllers
{
    public class CampeonatoController : Controller
    {
        private readonly CampeonatoContext _context;

        public CampeonatoController(CampeonatoContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var tournamentStatus = await _context.Tournments
                .FirstOrDefaultAsync(t => t.Id == 1);

            if (tournamentStatus == null)
            {
                return NotFound();
            }
            var modalities = await _context.Modalidades.ToListAsync();

            var model = new { TournamentStatus = tournamentStatus, Modalities = modalities };

            return View(model);
        }


        // GET
        [HttpPost]
        public async Task<IActionResult> StartTournament()
        {
            var tournamentStatus = await _context.Tournments
                .FirstOrDefaultAsync(t => t.Id == 1);

            if (tournamentStatus == null)
            {
                return NotFound();
            }

            if (tournamentStatus.Status == "running")
            {
                TempData["Message"] = "The tournament is already running!";
                return RedirectToAction(nameof(Index));
            }

            tournamentStatus.Status = "running";
            _context.Update(tournamentStatus);
            await _context.SaveChangesAsync();

            await GenerateTournamentBrackets();

            TempData["Message"] = "The tournament has started!";
            return RedirectToAction("Index", "Home");
        }

        private async Task GenerateTournamentBrackets()
        {
            var modalities = await _context.Modalidades.ToListAsync();

            foreach (var modality in modalities)
            {
                if (modality.Individual)
                {
                    await GenerateIndividualMatches(modality.Id);
                }
                else
                {
                    await GenerateTeamMatches(modality.Id);
                }
            }
        }

        private async Task GenerateIndividualMatches(int modalityId)
        {
            var players = await _context.RegistroModalidadeIndividuals
                .Where(r => r.IdModalidade == modalityId)
                .ToListAsync();

            if (!players.Any())
            {
                throw new InvalidOperationException($"No players registered for modality {modalityId}.");
            }

            var match = new PartidaIndividual
            {
                IdModalidade = modalityId,
                PosChaveamento = null 
            };

            _context.PartidaIndividuals.Add(match);
            await _context.SaveChangesAsync();

            foreach (var player in players)
            {
                var playerMatch = new JogadorEmPartidaIndividual
                {
                    IdPartidaIndividual = match.Id,
                    IdJogador = player.IdJogador
                };
                _context.JogadorEmPartidaIndividuals.Add(playerMatch);
            }

            await _context.SaveChangesAsync();
        }

        private async Task GenerateTeamMatches(int modalityId)
        {
            var modalityExists = await _context.Modalidades.AnyAsync(m => m.Id == modalityId);
            if (!modalityExists)
            {
                throw new InvalidOperationException($"Modality with ID {modalityId} does not exist.");
            }

            var registeredTeams = await _context.RegistroModalidadeEquipes
                .Where(r => r.IdModalidade == modalityId)
                .Select(r => r.IdEquipe)
                .ToListAsync();

            var validTeams = await _context.Equipes
                .Where(e => registeredTeams.Contains(e.Id))
                .Select(e => e.Id)
                .ToListAsync();

            if (validTeams.Count < 2)
            {
                throw new InvalidOperationException("Not enough valid teams to generate matches.");
            }

            var random = new Random();
            var shuffledTeams = validTeams.OrderBy(_ => random.Next()).ToList();

            int totalRounds = (int)Math.Ceiling(Math.Log2(shuffledTeams.Count));

            var currentRoundTeams = shuffledTeams;
            for (int round = 1; round <= totalRounds; round++)
            {
                int matchPosition = 1;
                var nextRoundTeams = new List<int>();

                for (int i = 0; i < currentRoundTeams.Count; i += 2)
                {
                    var match = new PartidaEquipe
                    {
                        IdModalidade = modalityId,
                        PosChaveamento = round * 100 + matchPosition
                    };
                    _context.PartidaEquipes.Add(match);
                    await _context.SaveChangesAsync();

                    await _context.SaveChangesAsync();

                    nextRoundTeams.Add(match.Id);
                    matchPosition++;
                }

                currentRoundTeams = nextRoundTeams;
            }
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> EndTournament()
        {
            var tournamentStatus = await _context.Tournments
                .FirstOrDefaultAsync(t => t.Id == 1);

            if (tournamentStatus == null)
            {
                return NotFound();
            }

            if (tournamentStatus.Status == "waiting")
            {
                TempData["Message"] = "The tournament is not running!";
                return RedirectToAction(nameof(Index));
            }

            tournamentStatus.Status = "waiting";
            _context.Update(tournamentStatus);
            await _context.SaveChangesAsync();

            TempData["Message"] = "The tournament has ended!";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ResetTournament()
        {
            _context.JogadorEmPartidaIndividuals.RemoveRange(_context.JogadorEmPartidaIndividuals);
            _context.PontosPartidaIndividuals.RemoveRange(_context.PontosPartidaIndividuals);
            _context.PartidaIndividuals.RemoveRange(_context.PartidaIndividuals);

            _context.EquipeEmPartida.RemoveRange(_context.EquipeEmPartida);
            _context.PontosPartidaEmEquipes.RemoveRange(_context.PontosPartidaEmEquipes);
            _context.PartidaEquipes.RemoveRange(_context.PartidaEquipes);

            _context.PontosCampeonatos.RemoveRange(_context.PontosCampeonatos);

            var tournament = await _context.Tournments.FirstOrDefaultAsync(t => t.Id == 1);
            if (tournament != null)
            {
                tournament.Status = "waiting";
                _context.Update(tournament);
            }


            await _context.SaveChangesAsync();

            TempData["Message"] = "The tournament has been reset!";
            return RedirectToAction("Index", "Home");
        }
    }
}
