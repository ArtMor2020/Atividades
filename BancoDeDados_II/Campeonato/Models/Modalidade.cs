using System;
using System.Collections.Generic;

namespace Campeonato.Models;

public partial class Modalidade
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool Individual { get; set; }

    public virtual ICollection<PartidaEquipe> PartidaEquipes { get; set; } = new List<PartidaEquipe>();

    public virtual ICollection<PartidaIndividual> PartidaIndividuals { get; set; } = new List<PartidaIndividual>();

    public virtual ICollection<PontosCampeonato> PontosCampeonatos { get; set; } = new List<PontosCampeonato>();

    public virtual ICollection<RegistroModalidadeEquipe> RegistroModalidadeEquipes { get; set; } = new List<RegistroModalidadeEquipe>();

    public virtual ICollection<RegistroModalidadeIndividual> RegistroModalidadeIndividuals { get; set; } = new List<RegistroModalidadeIndividual>();
}
