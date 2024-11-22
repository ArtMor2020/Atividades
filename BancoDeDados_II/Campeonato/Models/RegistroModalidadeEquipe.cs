using System;
using System.Collections.Generic;

namespace Campeonato.Models;

public partial class RegistroModalidadeEquipe
{
    public int Id { get; set; }

    public int IdModalidade { get; set; }

    public int IdEquipe { get; set; }

    public virtual Equipe IdEquipeNavigation { get; set; } = null!;

    public virtual Modalidade IdModalidadeNavigation { get; set; } = null!;
}
