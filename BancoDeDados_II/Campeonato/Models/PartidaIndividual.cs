using System;
using System.Collections.Generic;

namespace Campeonato.Models;

public partial class PartidaIndividual
{
    public int Id { get; set; }

    public int IdModalidade { get; set; }

    public int? IdJogadorVencedor { get; set; }

    public int? PosChaveamento { get; set; }

    public virtual Jogador? IdJogadorVencedorNavigation { get; set; }

    public virtual Modalidade IdModalidadeNavigation { get; set; } = null!;

    public virtual ICollection<JogadorEmPartidaIndividual> JogadorEmPartidaIndividuals { get; set; } = new List<JogadorEmPartidaIndividual>();

    public virtual ICollection<PontosPartidaIndividual> PontosPartidaIndividuals { get; set; } = new List<PontosPartidaIndividual>();
}
