using System;
using System.Collections.Generic;

namespace Campeonato.Models;

public partial class PontosCampeonato
{
    public int Id { get; set; }

    public int IdModalidade { get; set; }

    public int? IdEquipe { get; set; }

    public int? IdJogador { get; set; }

    public int Pontos { get; set; }

    public virtual Equipe? IdEquipeNavigation { get; set; }

    public virtual Jogador? IdJogadorNavigation { get; set; }

    public virtual Modalidade IdModalidadeNavigation { get; set; } = null!;
}
