using System;
using System.Collections.Generic;

namespace Campeonato.Models;

public partial class PontosPartidaEmEquipe
{
    public int Id { get; set; }

    public int IdPartidaEmEquipe { get; set; }

    public int IdEquipe { get; set; }

    public int Pontos { get; set; }

    public virtual Equipe IdEquipeNavigation { get; set; } = null!;

    public virtual PartidaEquipe IdPartidaEmEquipeNavigation { get; set; } = null!;
}
