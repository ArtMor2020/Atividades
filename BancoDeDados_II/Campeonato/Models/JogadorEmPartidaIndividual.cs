using System;
using System.Collections.Generic;

namespace Campeonato.Models;

public partial class JogadorEmPartidaIndividual
{
    public int Id { get; set; }

    public int IdPartidaIndividual { get; set; }

    public int IdJogador { get; set; }

    public virtual Jogador IdJogadorNavigation { get; set; } = null!;

    public virtual PartidaIndividual IdPartidaIndividualNavigation { get; set; } = null!;
}
