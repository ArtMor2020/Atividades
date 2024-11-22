using System;
using System.Collections.Generic;

namespace Campeonato.Models;

public partial class Equipe
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<EquipeEmPartidum> EquipeEmPartida { get; set; } = new List<EquipeEmPartidum>();

    public virtual ICollection<JogadorEmEquipe> JogadorEmEquipes { get; set; } = new List<JogadorEmEquipe>();

    public virtual ICollection<PartidaEquipe> PartidaEquipes { get; set; } = new List<PartidaEquipe>();

    public virtual ICollection<PontosCampeonato> PontosCampeonatos { get; set; } = new List<PontosCampeonato>();

    public virtual ICollection<PontosPartidaEmEquipe> PontosPartidaEmEquipes { get; set; } = new List<PontosPartidaEmEquipe>();

    public virtual ICollection<RegistroModalidadeEquipe> RegistroModalidadeEquipes { get; set; } = new List<RegistroModalidadeEquipe>();
}
