﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;

namespace backend.Models
{
    public partial class VallerContext : DbContext
    {
        public VallerContext()
        {
        }

        public VallerContext(DbContextOptions<VallerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Endereco> Endereco { get; set; }
        public virtual DbSet<Oferta> Oferta { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Reserva> Reserva { get; set; }
        public virtual DbSet<Telefone> Telefone { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-2R9DLMI\\SQLEXPRESS; Database=Valler; User Id=sa; Password=132 ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PK__Categori__CD54BC5A7A2DCD9E");

                entity.Property(e => e.Categoria1).IsUnicode(false);
            });

            modelBuilder.Entity<Endereco>(entity =>
            {
                entity.HasKey(e => e.IdEndereco)
                    .HasName("PK__Endereco__324B959EDD5BA9C9");

                entity.Property(e => e.Bairro).IsUnicode(false);

                entity.Property(e => e.Cep).IsUnicode(false);

                entity.Property(e => e.Cidade).IsUnicode(false);

                entity.Property(e => e.Numero).IsUnicode(false);

                entity.Property(e => e.Rua).IsUnicode(false);

                entity.Property(e => e.Uf).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Endereco)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Endereco__id_usu__4222D4EF");
            });

            modelBuilder.Entity<Oferta>(entity =>
            {
                entity.HasKey(e => e.IdOferta)
                    .HasName("PK__Oferta__2B7BF92F6B2C999F");

                entity.Property(e => e.DataOferta).IsUnicode(false);

                entity.Property(e => e.DataVencimento).IsUnicode(false);

                entity.Property(e => e.Imagem).IsUnicode(false);

                entity.Property(e => e.Preco).IsUnicode(false);

                entity.Property(e => e.Quantidade).IsUnicode(false);

                entity.Property(e => e.Titulo).IsUnicode(false);

                entity.HasOne(d => d.IdProdutoNavigation)
                    .WithMany(p => p.Oferta)
                    .HasForeignKey(d => d.IdProduto)
                    .HasConstraintName("FK__Oferta__id_produ__47DBAE45");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.IdProduto)
                    .HasName("PK__Produto__BA38A6B878F7CC65");

                entity.Property(e => e.Descricao).IsUnicode(false);

                entity.Property(e => e.NomeProduto).IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Produto__id_cate__3E52440B");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Produto__id_usua__3F466844");
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.IdReserva)
                    .HasName("PK__Reserva__423CBE5D2D829966");

                entity.Property(e => e.Cronometro).IsUnicode(false);

                entity.Property(e => e.QuantidadeReserva).IsUnicode(false);

                entity.Property(e => e.StatusReserva).IsUnicode(false);

                entity.HasOne(d => d.IdOfertaNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.IdOferta)
                    .HasConstraintName("FK__Reserva__id_ofer__4AB81AF0");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Reserva)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Reserva__id_usua__4BAC3F29");
            });

            modelBuilder.Entity<Telefone>(entity =>
            {
                entity.HasKey(e => e.IdTelefone)
                    .HasName("PK__Telefone__28CD683487749FC0");

                entity.Property(e => e.Telefone1).IsUnicode(false);

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.Telefone)
                    .HasForeignKey(d => d.IdUsuario)
                    .HasConstraintName("FK__Telefone__id_usu__44FF419A");
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("PK__Tipo_Usu__B17D78C86E04217F");

                entity.Property(e => e.Tipo).IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__Usuario__4E3E04AD7653D3C2");

                entity.Property(e => e.Documento).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.NomeRazaoSocial).IsUnicode(false);

                entity.Property(e => e.Senha).IsUnicode(false);

                entity.HasOne(d => d.IdTipoUsuarioNavigation)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.IdTipoUsuario)
                    .HasConstraintName("FK__Usuario__id_tipo__398D8EEE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
