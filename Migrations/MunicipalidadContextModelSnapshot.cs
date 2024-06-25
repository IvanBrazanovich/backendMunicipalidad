﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MunicipalidadTPApi.Data;

#nullable disable

namespace WebMunicipalidadTP.Migrations
{
    [DbContext(typeof(MunicipalidadContext))]
    partial class MunicipalidadContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebMunicipalidadTP.Models.Bien", b =>
                {
                    b.Property<int>("Idbien")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Idbien"));

                    b.Property<int?>("Anio")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marca")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nomenclatura_catastral")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patente")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Superficie")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Idbien");

                    b.HasIndex("Tipo");

                    b.ToTable("Bien");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.BienPropietario", b =>
                {
                    b.Property<int>("Id_Bien")
                        .HasColumnType("int");

                    b.Property<int>("Id_Propietario")
                        .HasColumnType("int");

                    b.Property<decimal?>("Porcentaje_Propiedad")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id_Bien", "Id_Propietario");

                    b.HasIndex("Id_Propietario");

                    b.ToTable("BienPropietario");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.CDCuotaestado", b =>
                {
                    b.Property<int>("Id_estado")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_estado"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_estado");

                    b.ToTable("CDCuotaestado");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.CDest_civ", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codigo"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codigo");

                    b.ToTable("CDest_civ");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.CDtipoBien", b =>
                {
                    b.Property<int>("Cod_tipo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Cod_tipo"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Cod_tipo");

                    b.ToTable("CDtipoBien");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.MunicipalidadTPApi.Models.Cuota", b =>
                {
                    b.Property<int>("Idcuota")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Idcuota"));

                    b.Property<int?>("CodEstado")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Fecha_vencimiento")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Id_bien")
                        .HasColumnType("int");

                    b.Property<decimal?>("Monto")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Idcuota");

                    b.HasIndex("CodEstado");

                    b.HasIndex("Id_bien");

                    b.ToTable("Cuota");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.Pago", b =>
                {
                    b.Property<int>("Id_Pago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Pago"));

                    b.Property<DateTime?>("Fecha_pago")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Id_cuota")
                        .HasColumnType("int");

                    b.Property<decimal?>("Monto_pagado")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id_Pago");

                    b.HasIndex("Id_cuota");

                    b.ToTable("Pago");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.PagoPropietario", b =>
                {
                    b.Property<int>("Id_Pago")
                        .HasColumnType("int");

                    b.Property<int>("Id_Propietario")
                        .HasColumnType("int");

                    b.Property<int>("Id_Bien")
                        .HasColumnType("int");

                    b.HasKey("Id_Pago", "Id_Propietario", "Id_Bien");

                    b.HasIndex("Id_Bien");

                    b.HasIndex("Id_Propietario");

                    b.ToTable("PagoPropietario");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.Propietario", b =>
                {
                    b.Property<int>("IDPropietario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IDPropietario"));

                    b.Property<string>("ApeyNombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Cod_estado_Civil")
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Fechanac")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Numdoc")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("IDPropietario");

                    b.HasIndex("Cod_estado_Civil");

                    b.ToTable("Propietario");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.Bien", b =>
                {
                    b.HasOne("WebMunicipalidadTP.Models.CDtipoBien", "TipoBien")
                        .WithMany("Bienes")
                        .HasForeignKey("Tipo");

                    b.Navigation("TipoBien");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.BienPropietario", b =>
                {
                    b.HasOne("WebMunicipalidadTP.Models.Bien", "Bien")
                        .WithMany("BienesPropietarios")
                        .HasForeignKey("Id_Bien")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMunicipalidadTP.Models.Propietario", "Propietario")
                        .WithMany("BienesPropietarios")
                        .HasForeignKey("Id_Propietario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bien");

                    b.Navigation("Propietario");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.MunicipalidadTPApi.Models.Cuota", b =>
                {
                    b.HasOne("WebMunicipalidadTP.Models.CDCuotaestado", "Estado")
                        .WithMany("Cuotas")
                        .HasForeignKey("CodEstado");

                    b.HasOne("WebMunicipalidadTP.Models.Bien", "Bien")
                        .WithMany("Cuotas")
                        .HasForeignKey("Id_bien");

                    b.Navigation("Bien");

                    b.Navigation("Estado");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.Pago", b =>
                {
                    b.HasOne("WebMunicipalidadTP.Models.MunicipalidadTPApi.Models.Cuota", "Cuota")
                        .WithMany("Pagos")
                        .HasForeignKey("Id_cuota");

                    b.Navigation("Cuota");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.PagoPropietario", b =>
                {
                    b.HasOne("WebMunicipalidadTP.Models.Bien", "Bien")
                        .WithMany("PagosPropietarios")
                        .HasForeignKey("Id_Bien")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMunicipalidadTP.Models.Pago", "Pago")
                        .WithMany("PagosPropietarios")
                        .HasForeignKey("Id_Pago")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebMunicipalidadTP.Models.Propietario", "Propietario")
                        .WithMany("PagosPropietarios")
                        .HasForeignKey("Id_Propietario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bien");

                    b.Navigation("Pago");

                    b.Navigation("Propietario");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.Propietario", b =>
                {
                    b.HasOne("WebMunicipalidadTP.Models.CDest_civ", "EstadoCivil")
                        .WithMany("Propietarios")
                        .HasForeignKey("Cod_estado_Civil");

                    b.Navigation("EstadoCivil");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.Bien", b =>
                {
                    b.Navigation("BienesPropietarios");

                    b.Navigation("Cuotas");

                    b.Navigation("PagosPropietarios");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.CDCuotaestado", b =>
                {
                    b.Navigation("Cuotas");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.CDest_civ", b =>
                {
                    b.Navigation("Propietarios");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.CDtipoBien", b =>
                {
                    b.Navigation("Bienes");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.MunicipalidadTPApi.Models.Cuota", b =>
                {
                    b.Navigation("Pagos");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.Pago", b =>
                {
                    b.Navigation("PagosPropietarios");
                });

            modelBuilder.Entity("WebMunicipalidadTP.Models.Propietario", b =>
                {
                    b.Navigation("BienesPropietarios");

                    b.Navigation("PagosPropietarios");
                });
#pragma warning restore 612, 618
        }
    }
}
