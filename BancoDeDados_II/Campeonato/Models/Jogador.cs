using System;
using System.Collections.Generic;

namespace Campeonato.Models;

public partial class Jogador
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<JogadorEmEquipe> JogadorEmEquipes { get; set; } = new List<JogadorEmEquipe>();

    public virtual ICollection<JogadorEmPartidaIndividual> JogadorEmPartidaIndividuals { get; set; } = new List<JogadorEmPartidaIndividual>();

    public virtual ICollection<PartidaIndividual> PartidaIndividuals { get; set; } = new List<PartidaIndividual>();

    public virtual ICollection<PontosCampeonato> PontosCampeonatos { get; set; } = new List<PontosCampeonato>();

    public virtual ICollection<PontosPartidaIndividual> PontosPartidaIndividuals { get; set; } = new List<PontosPartidaIndividual>();

    public virtual ICollection<RegistroModalidadeIndividual> RegistroModalidadeIndividuals { get; set; } = new List<RegistroModalidadeIndividual>();
}
