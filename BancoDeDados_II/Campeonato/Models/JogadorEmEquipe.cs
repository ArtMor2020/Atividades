using System;
using System.Collections.Generic;

namespace Campeonato.Models;

public partial class JogadorEmEquipe
{
    public int Id { get; set; }

    public int IdJogador { get; set; }

    public int IdEquipe { get; set; }

    public virtual Equipe IdEquipeNavigation { get; set; } = null!;

    public virtual Jogador IdJogadorNavigation { get; set; } = null!;
}
