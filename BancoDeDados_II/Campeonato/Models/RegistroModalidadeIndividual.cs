using System;
using System.Collections.Generic;

namespace Campeonato.Models;

public partial class RegistroModalidadeIndividual
{
    public int Id { get; set; }

    public int IdModalidade { get; set; }

    public int IdJogador { get; set; }

    public virtual Jogador IdJogadorNavigation { get; set; } = null!;

    public virtual Modalidade IdModalidadeNavigation { get; set; } = null!;
}
