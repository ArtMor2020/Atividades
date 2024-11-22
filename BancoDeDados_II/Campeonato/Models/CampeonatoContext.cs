using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Campeonato.Models;

public partial class CampeonatoContext : DbContext
{
    public CampeonatoContext()
    {
    }

    public CampeonatoContext(DbContextOptions<CampeonatoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipe> Equipes { get; set; }

    public virtual DbSet<EquipeEmPartidum> EquipeEmPartida { get; set; }

    public virtual DbSet<Jogador> Jogadors { get; set; }

    public virtual DbSet<JogadorEmEquipe> JogadorEmEquipes { get; set; }

    public virtual DbSet<JogadorEmPartidaIndividual> JogadorEmPartidaIndividuals { get; set; }

    public virtual DbSet<Modalidade> Modalidades { get; set; }

    public virtual DbSet<PartidaEquipe> PartidaEquipes { get; set; }

    public virtual DbSet<PartidaIndividual> PartidaIndividuals { get; set; }

    public virtual DbSet<PontosCampeonato> PontosCampeonatos { get; set; }

    public virtual DbSet<PontosPartidaEmEquipe> PontosPartidaEmEquipes { get; set; }

    public virtual DbSet<PontosPartidaIndividual> PontosPartidaIndividuals { get; set; }

    public virtual DbSet<RegistroModalidadeEquipe> RegistroModalidadeEquipes { get; set; }

    public virtual DbSet<RegistroModalidadeIndividual> RegistroModalidadeIndividuals { get; set; }

    public virtual DbSet<Tournment> Tournments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=campeonato");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__equipe__3213E83F5774D61B");

            entity.ToTable("equipe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<EquipeEmPartidum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__equipeEm__3213E83F0E9F375F");

            entity.ToTable("equipeEmPartida");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEquipe).HasColumnName("idEquipe");
            entity.Property(e => e.IdPartidaEquipe).HasColumnName("idPartidaEquipe");

            entity.HasOne(d => d.IdEquipeNavigation).WithMany(p => p.EquipeEmPartida)
                .HasForeignKey(d => d.IdEquipe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_equipeEmPartida_idEquipe");

            entity.HasOne(d => d.IdPartidaEquipeNavigation).WithMany(p => p.EquipeEmPartida)
                .HasForeignKey(d => d.IdPartidaEquipe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_equipeEmPartida_idPartidaEquipe");
        });

        modelBuilder.Entity<Jogador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jogador__3213E83F92D79C85");

            entity.ToTable("jogador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<JogadorEmEquipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jogadorE__3213E83FE24FA27D");

            entity.ToTable("jogadorEmEquipe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEquipe).HasColumnName("idEquipe");
            entity.Property(e => e.IdJogador).HasColumnName("idJogador");

            entity.HasOne(d => d.IdEquipeNavigation).WithMany(p => p.JogadorEmEquipes)
                .HasForeignKey(d => d.IdEquipe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_jogadorEmEquipe_idEquipe");

            entity.HasOne(d => d.IdJogadorNavigation).WithMany(p => p.JogadorEmEquipes)
                .HasForeignKey(d => d.IdJogador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_jogadorEmEquipe_idJogador");
        });

        modelBuilder.Entity<JogadorEmPartidaIndividual>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jogadorE__3213E83F196E9A10");

            entity.ToTable("jogadorEmPartidaIndividual");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdJogador).HasColumnName("idJogador");
            entity.Property(e => e.IdPartidaIndividual).HasColumnName("idPartidaIndividual");

            entity.HasOne(d => d.IdJogadorNavigation).WithMany(p => p.JogadorEmPartidaIndividuals)
                .HasForeignKey(d => d.IdJogador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_jogadorEmPartidaIndividual_idJogador");

            entity.HasOne(d => d.IdPartidaIndividualNavigation).WithMany(p => p.JogadorEmPartidaIndividuals)
                .HasForeignKey(d => d.IdPartidaIndividual)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_jogadorEmPartidaIndividual_idPartidaIndividual");
        });

        modelBuilder.Entity<Modalidade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__modalida__3213E83F4F9B1FF7");

            entity.ToTable("modalidade");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Individual).HasColumnName("individual");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<PartidaEquipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__partidaE__3213E83FC3ADBD29");

            entity.ToTable("partidaEquipe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEquipeVencedora).HasColumnName("idEquipeVencedora");
            entity.Property(e => e.IdModalidade).HasColumnName("idModalidade");
            entity.Property(e => e.PosChaveamento).HasColumnName("posChaveamento");

            entity.HasOne(d => d.IdEquipeVencedoraNavigation).WithMany(p => p.PartidaEquipes)
                .HasForeignKey(d => d.IdEquipeVencedora)
                .HasConstraintName("fk_partidaEquipe_idEquipeVencedora");

            entity.HasOne(d => d.IdModalidadeNavigation).WithMany(p => p.PartidaEquipes)
                .HasForeignKey(d => d.IdModalidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_partidaEquipe_idModalidade");
        });

        modelBuilder.Entity<PartidaIndividual>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__partidaI__3213E83FF0C02494");

            entity.ToTable("partidaIndividual");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdJogadorVencedor).HasColumnName("idJogadorVencedor");
            entity.Property(e => e.IdModalidade).HasColumnName("idModalidade");
            entity.Property(e => e.PosChaveamento).HasColumnName("posChaveamento");

            entity.HasOne(d => d.IdJogadorVencedorNavigation).WithMany(p => p.PartidaIndividuals)
                .HasForeignKey(d => d.IdJogadorVencedor)
                .HasConstraintName("fk_partidaIndividual_idJogadorVencedor");

            entity.HasOne(d => d.IdModalidadeNavigation).WithMany(p => p.PartidaIndividuals)
                .HasForeignKey(d => d.IdModalidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_partidaIndividual_idModalidade");
        });

        modelBuilder.Entity<PontosCampeonato>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pontosCa__3213E83FDCC90CD7");

            entity.ToTable("pontosCampeonato");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEquipe).HasColumnName("idEquipe");
            entity.Property(e => e.IdJogador).HasColumnName("idJogador");
            entity.Property(e => e.IdModalidade).HasColumnName("idModalidade");
            entity.Property(e => e.Pontos).HasColumnName("pontos");

            entity.HasOne(d => d.IdEquipeNavigation).WithMany(p => p.PontosCampeonatos)
                .HasForeignKey(d => d.IdEquipe)
                .HasConstraintName("fk_pontosCampeonato_idEquipe");

            entity.HasOne(d => d.IdJogadorNavigation).WithMany(p => p.PontosCampeonatos)
                .HasForeignKey(d => d.IdJogador)
                .HasConstraintName("fk_pontosCampeonato_idJogador");

            entity.HasOne(d => d.IdModalidadeNavigation).WithMany(p => p.PontosCampeonatos)
                .HasForeignKey(d => d.IdModalidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pontosCampeonato_idModalidade");
        });

        modelBuilder.Entity<PontosPartidaEmEquipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pontosPa__3213E83F3CB118B7");

            entity.ToTable("pontosPartidaEmEquipe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEquipe).HasColumnName("idEquipe");
            entity.Property(e => e.IdPartidaEmEquipe).HasColumnName("idPartidaEmEquipe");
            entity.Property(e => e.Pontos).HasColumnName("pontos");

            entity.HasOne(d => d.IdEquipeNavigation).WithMany(p => p.PontosPartidaEmEquipes)
                .HasForeignKey(d => d.IdEquipe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pontosPartida_idEquipe");

            entity.HasOne(d => d.IdPartidaEmEquipeNavigation).WithMany(p => p.PontosPartidaEmEquipes)
                .HasForeignKey(d => d.IdPartidaEmEquipe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pontosPartida_idPartidaEmEquipe");
        });

        modelBuilder.Entity<PontosPartidaIndividual>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__pontosPa__3213E83FE2A2ECEA");

            entity.ToTable("pontosPartidaIndividual");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdJogador).HasColumnName("idJogador");
            entity.Property(e => e.IdPartidaIndividual).HasColumnName("idPartidaIndividual");
            entity.Property(e => e.Pontos).HasColumnName("pontos");

            entity.HasOne(d => d.IdJogadorNavigation).WithMany(p => p.PontosPartidaIndividuals)
                .HasForeignKey(d => d.IdJogador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pontosPartida_idJogador");

            entity.HasOne(d => d.IdPartidaIndividualNavigation).WithMany(p => p.PontosPartidaIndividuals)
                .HasForeignKey(d => d.IdPartidaIndividual)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_pontosPartida_idPartidaIndividual");
        });

        modelBuilder.Entity<RegistroModalidadeEquipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__registro__3213E83FE25CF5B8");

            entity.ToTable("registroModalidadeEquipe");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEquipe).HasColumnName("idEquipe");
            entity.Property(e => e.IdModalidade).HasColumnName("idModalidade");

            entity.HasOne(d => d.IdEquipeNavigation).WithMany(p => p.RegistroModalidadeEquipes)
                .HasForeignKey(d => d.IdEquipe)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_registroModalidadeEquipe_idEquipe");

            entity.HasOne(d => d.IdModalidadeNavigation).WithMany(p => p.RegistroModalidadeEquipes)
                .HasForeignKey(d => d.IdModalidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_registroModalidadeEquipe_idModalidade");
        });

        modelBuilder.Entity<RegistroModalidadeIndividual>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__registro__3213E83FE4FBF529");

            entity.ToTable("registroModalidadeIndividual");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdJogador).HasColumnName("idJogador");
            entity.Property(e => e.IdModalidade).HasColumnName("idModalidade");

            entity.HasOne(d => d.IdJogadorNavigation).WithMany(p => p.RegistroModalidadeIndividuals)
                .HasForeignKey(d => d.IdJogador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_registroModalidadeIndividual_idJogador");

            entity.HasOne(d => d.IdModalidadeNavigation).WithMany(p => p.RegistroModalidadeIndividuals)
                .HasForeignKey(d => d.IdModalidade)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_registroModalidadeIndividual_idModalidade");
        });

        modelBuilder.Entity<Tournment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tournmen__3213E83FA96361DC");

            entity.ToTable("tournment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
