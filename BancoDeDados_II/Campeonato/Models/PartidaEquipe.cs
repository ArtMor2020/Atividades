using System;
using System.Collections.Generic;

namespace Campeonato.Models;

public partial class PartidaEquipe
{
    public int Id { get; set; }

    public int IdModalidade { get; set; }

    public int? IdEquipeVencedora { get; set; }

    public int? PosChaveamento { get; set; }

    public virtual ICollection<EquipeEmPartidum> EquipeEmPartida { get; set; } = new List<EquipeEmPartidum>();

    public virtual Equipe? IdEquipeVencedoraNavigation { get; set; }

    public virtual Modalidade IdModalidadeNavigation { get; set; } = null!;

    public virtual ICollection<PontosPartidaEmEquipe> PontosPartidaEmEquipes { get; set; } = new List<PontosPartidaEmEquipe>();
}
