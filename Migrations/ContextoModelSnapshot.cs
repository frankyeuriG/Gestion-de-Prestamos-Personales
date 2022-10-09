﻿// <auto-generated />
using System;
using GestionPrestamos2022.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestionPrestamos2022.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("GestionPrestamos2022.Models.Ocupaciones", b =>
                {
                    b.Property<int>("OcupacionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Salario")
                        .HasColumnType("REAL");

                    b.HasKey("OcupacionId");

                    b.ToTable("Ocupaciones");
                });

            modelBuilder.Entity("GestionPrestamos2022.Models.Personas", b =>
                {
                    b.Property<int>("PersonaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Balance")
                        .HasColumnType("REAL");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaNacimiento")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OcupacionId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Telefono")
                        .HasColumnType("TEXT");

                    b.HasKey("PersonaId");

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("GestionPrestamos2022.Models.Prestamo", b =>
                {
                    b.Property<int>("PrestamosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Balance")
                        .HasColumnType("REAL");

                    b.Property<string>("Concepto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaPrestamo")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("FechaVence")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Monto")
                        .HasColumnType("REAL");

                    b.Property<int>("PersonaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PrestamosId");

                    b.ToTable("Prestamo");
                });

            modelBuilder.Entity("GestionPrestamos2022.Pagos", b =>
                {
                    b.Property<int>("PagosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Concepto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<double>("Monto")
                        .HasColumnType("REAL");

                    b.Property<int>("PersonaId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PagosId");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("GestionPrestamos2022.PagosDetalle", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PagosId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PrestamosId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("ValorPagado")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("PagosDetalle");
                });

            modelBuilder.Entity("GestionPrestamos2022.PagosDetalle", b =>
                {
                    b.HasOne("GestionPrestamos2022.Pagos", null)
                        .WithMany("PagosDetalle")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GestionPrestamos2022.Pagos", b =>
                {
                    b.Navigation("PagosDetalle");
                });
#pragma warning restore 612, 618
        }
    }
}
