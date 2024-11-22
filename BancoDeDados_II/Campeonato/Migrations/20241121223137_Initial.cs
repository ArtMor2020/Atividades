using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Campeonato.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "equipe",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__equipe__3213E83F5774D61B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "jogador",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__jogador__3213E83F92D79C85", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "modalidade",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    individual = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__modalida__3213E83F4F9B1FF7", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tournment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tournmen__3213E83FA96361DC", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "jogadorEmEquipe",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idJogador = table.Column<int>(type: "int", nullable: false),
                    idEquipe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__jogadorE__3213E83FE24FA27D", x => x.id);
                    table.ForeignKey(
                        name: "fk_jogadorEmEquipe_idEquipe",
                        column: x => x.idEquipe,
                        principalTable: "equipe",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_jogadorEmEquipe_idJogador",
                        column: x => x.idJogador,
                        principalTable: "jogador",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "partidaEquipe",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idModalidade = table.Column<int>(type: "int", nullable: false),
                    idEquipeVencedora = table.Column<int>(type: "int", nullable: true),
                    posChaveamento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__partidaE__3213E83FC3ADBD29", x => x.id);
                    table.ForeignKey(
                        name: "fk_partidaEquipe_idEquipeVencedora",
                        column: x => x.idEquipeVencedora,
                        principalTable: "equipe",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_partidaEquipe_idModalidade",
                        column: x => x.idModalidade,
                        principalTable: "modalidade",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "partidaIndividual",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idModalidade = table.Column<int>(type: "int", nullable: false),
                    idJogadorVencedor = table.Column<int>(type: "int", nullable: true),
                    posChaveamento = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__partidaI__3213E83FF0C02494", x => x.id);
                    table.ForeignKey(
                        name: "fk_partidaIndividual_idJogadorVencedor",
                        column: x => x.idJogadorVencedor,
                        principalTable: "jogador",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_partidaIndividual_idModalidade",
                        column: x => x.idModalidade,
                        principalTable: "modalidade",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "pontosCampeonato",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idModalidade = table.Column<int>(type: "int", nullable: false),
                    idEquipe = table.Column<int>(type: "int", nullable: true),
                    idJogador = table.Column<int>(type: "int", nullable: true),
                    pontos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pontosCa__3213E83FDCC90CD7", x => x.id);
                    table.ForeignKey(
                        name: "fk_pontosCampeonato_idEquipe",
                        column: x => x.idEquipe,
                        principalTable: "equipe",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_pontosCampeonato_idJogador",
                        column: x => x.idJogador,
                        principalTable: "jogador",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_pontosCampeonato_idModalidade",
                        column: x => x.idModalidade,
                        principalTable: "modalidade",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "registroModalidadeEquipe",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idModalidade = table.Column<int>(type: "int", nullable: false),
                    idEquipe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__registro__3213E83FE25CF5B8", x => x.id);
                    table.ForeignKey(
                        name: "fk_registroModalidadeEquipe_idEquipe",
                        column: x => x.idEquipe,
                        principalTable: "equipe",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_registroModalidadeEquipe_idModalidade",
                        column: x => x.idModalidade,
                        principalTable: "modalidade",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "registroModalidadeIndividual",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idModalidade = table.Column<int>(type: "int", nullable: false),
                    idJogador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__registro__3213E83FE4FBF529", x => x.id);
                    table.ForeignKey(
                        name: "fk_registroModalidadeIndividual_idJogador",
                        column: x => x.idJogador,
                        principalTable: "jogador",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_registroModalidadeIndividual_idModalidade",
                        column: x => x.idModalidade,
                        principalTable: "modalidade",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "equipeEmPartida",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPartidaEquipe = table.Column<int>(type: "int", nullable: false),
                    idEquipe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__equipeEm__3213E83F0E9F375F", x => x.id);
                    table.ForeignKey(
                        name: "fk_equipeEmPartida_idEquipe",
                        column: x => x.idEquipe,
                        principalTable: "equipe",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_equipeEmPartida_idPartidaEquipe",
                        column: x => x.idPartidaEquipe,
                        principalTable: "partidaEquipe",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "pontosPartidaEmEquipe",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPartidaEmEquipe = table.Column<int>(type: "int", nullable: false),
                    idEquipe = table.Column<int>(type: "int", nullable: false),
                    pontos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pontosPa__3213E83F3CB118B7", x => x.id);
                    table.ForeignKey(
                        name: "fk_pontosPartida_idEquipe",
                        column: x => x.idEquipe,
                        principalTable: "equipe",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_pontosPartida_idPartidaEmEquipe",
                        column: x => x.idPartidaEmEquipe,
                        principalTable: "partidaEquipe",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "jogadorEmPartidaIndividual",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPartidaIndividual = table.Column<int>(type: "int", nullable: false),
                    idJogador = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__jogadorE__3213E83F196E9A10", x => x.id);
                    table.ForeignKey(
                        name: "fk_jogadorEmPartidaIndividual_idJogador",
                        column: x => x.idJogador,
                        principalTable: "jogador",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_jogadorEmPartidaIndividual_idPartidaIndividual",
                        column: x => x.idPartidaIndividual,
                        principalTable: "partidaIndividual",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "pontosPartidaIndividual",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPartidaIndividual = table.Column<int>(type: "int", nullable: false),
                    idJogador = table.Column<int>(type: "int", nullable: false),
                    pontos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__pontosPa__3213E83FE2A2ECEA", x => x.id);
                    table.ForeignKey(
                        name: "fk_pontosPartida_idJogador",
                        column: x => x.idJogador,
                        principalTable: "jogador",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_pontosPartida_idPartidaIndividual",
                        column: x => x.idPartidaIndividual,
                        principalTable: "partidaIndividual",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_equipeEmPartida_idEquipe",
                table: "equipeEmPartida",
                column: "idEquipe");

            migrationBuilder.CreateIndex(
                name: "IX_equipeEmPartida_idPartidaEquipe",
                table: "equipeEmPartida",
                column: "idPartidaEquipe");

            migrationBuilder.CreateIndex(
                name: "IX_jogadorEmEquipe_idEquipe",
                table: "jogadorEmEquipe",
                column: "idEquipe");

            migrationBuilder.CreateIndex(
                name: "IX_jogadorEmEquipe_idJogador",
                table: "jogadorEmEquipe",
                column: "idJogador");

            migrationBuilder.CreateIndex(
                name: "IX_jogadorEmPartidaIndividual_idJogador",
                table: "jogadorEmPartidaIndividual",
                column: "idJogador");

            migrationBuilder.CreateIndex(
                name: "IX_jogadorEmPartidaIndividual_idPartidaIndividual",
                table: "jogadorEmPartidaIndividual",
                column: "idPartidaIndividual");

            migrationBuilder.CreateIndex(
                name: "IX_partidaEquipe_idEquipeVencedora",
                table: "partidaEquipe",
                column: "idEquipeVencedora");

            migrationBuilder.CreateIndex(
                name: "IX_partidaEquipe_idModalidade",
                table: "partidaEquipe",
                column: "idModalidade");

            migrationBuilder.CreateIndex(
                name: "IX_partidaIndividual_idJogadorVencedor",
                table: "partidaIndividual",
                column: "idJogadorVencedor");

            migrationBuilder.CreateIndex(
                name: "IX_partidaIndividual_idModalidade",
                table: "partidaIndividual",
                column: "idModalidade");

            migrationBuilder.CreateIndex(
                name: "IX_pontosCampeonato_idEquipe",
                table: "pontosCampeonato",
                column: "idEquipe");

            migrationBuilder.CreateIndex(
                name: "IX_pontosCampeonato_idJogador",
                table: "pontosCampeonato",
                column: "idJogador");

            migrationBuilder.CreateIndex(
                name: "IX_pontosCampeonato_idModalidade",
                table: "pontosCampeonato",
                column: "idModalidade");

            migrationBuilder.CreateIndex(
                name: "IX_pontosPartidaEmEquipe_idEquipe",
                table: "pontosPartidaEmEquipe",
                column: "idEquipe");

            migrationBuilder.CreateIndex(
                name: "IX_pontosPartidaEmEquipe_idPartidaEmEquipe",
                table: "pontosPartidaEmEquipe",
                column: "idPartidaEmEquipe");

            migrationBuilder.CreateIndex(
                name: "IX_pontosPartidaIndividual_idJogador",
                table: "pontosPartidaIndividual",
                column: "idJogador");

            migrationBuilder.CreateIndex(
                name: "IX_pontosPartidaIndividual_idPartidaIndividual",
                table: "pontosPartidaIndividual",
                column: "idPartidaIndividual");

            migrationBuilder.CreateIndex(
                name: "IX_registroModalidadeEquipe_idEquipe",
                table: "registroModalidadeEquipe",
                column: "idEquipe");

            migrationBuilder.CreateIndex(
                name: "IX_registroModalidadeEquipe_idModalidade",
                table: "registroModalidadeEquipe",
                column: "idModalidade");

            migrationBuilder.CreateIndex(
                name: "IX_registroModalidadeIndividual_idJogador",
                table: "registroModalidadeIndividual",
                column: "idJogador");

            migrationBuilder.CreateIndex(
                name: "IX_registroModalidadeIndividual_idModalidade",
                table: "registroModalidadeIndividual",
                column: "idModalidade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "equipeEmPartida");

            migrationBuilder.DropTable(
                name: "jogadorEmEquipe");

            migrationBuilder.DropTable(
                name: "jogadorEmPartidaIndividual");

            migrationBuilder.DropTable(
                name: "pontosCampeonato");

            migrationBuilder.DropTable(
                name: "pontosPartidaEmEquipe");

            migrationBuilder.DropTable(
                name: "pontosPartidaIndividual");

            migrationBuilder.DropTable(
                name: "registroModalidadeEquipe");

            migrationBuilder.DropTable(
                name: "registroModalidadeIndividual");

            migrationBuilder.DropTable(
                name: "tournment");

            migrationBuilder.DropTable(
                name: "partidaEquipe");

            migrationBuilder.DropTable(
                name: "partidaIndividual");

            migrationBuilder.DropTable(
                name: "equipe");

            migrationBuilder.DropTable(
                name: "jogador");

            migrationBuilder.DropTable(
                name: "modalidade");
        }
    }
}
