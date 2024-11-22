using System;
using System.Collections.Generic;

namespace Campeonato.Models;

public partial class EquipeEmPartidum
{
    public int Id { get; set; }

    public int IdPartidaEquipe { get; set; }

    public int IdEquipe { get; set; }

    public virtual Equipe IdEquipeNavigation { get; set; } = null!;

    public virtual PartidaEquipe IdPartidaEquipeNavigation { get; set; } = null!;
}
