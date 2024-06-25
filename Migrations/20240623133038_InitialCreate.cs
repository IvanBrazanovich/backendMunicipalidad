using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMunicipalidadTP.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CDCuotaestado",
                columns: table => new
                {
                    Id_estado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDCuotaestado", x => x.Id_estado);
                });

            migrationBuilder.CreateTable(
                name: "CDest_civ",
                columns: table => new
                {
                    Codigo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDest_civ", x => x.Codigo);
                });

            migrationBuilder.CreateTable(
                name: "CDtipoBien",
                columns: table => new
                {
                    Cod_tipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CDtipoBien", x => x.Cod_tipo);
                });

            migrationBuilder.CreateTable(
                name: "Propietario",
                columns: table => new
                {
                    IDPropietario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApeyNombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numdoc = table.Column<int>(type: "int", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fechanac = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cod_estado_Civil = table.Column<int>(type: "int", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propietario", x => x.IDPropietario);
                    table.ForeignKey(
                        name: "FK_Propietario_CDest_civ_Cod_estado_Civil",
                        column: x => x.Cod_estado_Civil,
                        principalTable: "CDest_civ",
                        principalColumn: "Codigo");
                });

            migrationBuilder.CreateTable(
                name: "Bien",
                columns: table => new
                {
                    Idbien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<int>(type: "int", nullable: true),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Superficie = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Nomenclatura_catastral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Anio = table.Column<int>(type: "int", nullable: true),
                    Patente = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bien", x => x.Idbien);
                    table.ForeignKey(
                        name: "FK_Bien_CDtipoBien_Tipo",
                        column: x => x.Tipo,
                        principalTable: "CDtipoBien",
                        principalColumn: "Cod_tipo");
                });

            migrationBuilder.CreateTable(
                name: "BienPropietario",
                columns: table => new
                {
                    Id_Bien = table.Column<int>(type: "int", nullable: false),
                    Id_Propietario = table.Column<int>(type: "int", nullable: false),
                    Porcentaje_Propiedad = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienPropietario", x => new { x.Id_Bien, x.Id_Propietario });
                    table.ForeignKey(
                        name: "FK_BienPropietario_Bien_Id_Bien",
                        column: x => x.Id_Bien,
                        principalTable: "Bien",
                        principalColumn: "Idbien",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BienPropietario_Propietario_Id_Propietario",
                        column: x => x.Id_Propietario,
                        principalTable: "Propietario",
                        principalColumn: "IDPropietario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cuota",
                columns: table => new
                {
                    Idcuota = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_bien = table.Column<int>(type: "int", nullable: true),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Fecha_vencimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CodEstado = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuota", x => x.Idcuota);
                    table.ForeignKey(
                        name: "FK_Cuota_Bien_Id_bien",
                        column: x => x.Id_bien,
                        principalTable: "Bien",
                        principalColumn: "Idbien");
                    table.ForeignKey(
                        name: "FK_Cuota_CDCuotaestado_CodEstado",
                        column: x => x.CodEstado,
                        principalTable: "CDCuotaestado",
                        principalColumn: "Id_estado");
                });

            migrationBuilder.CreateTable(
                name: "Pago",
                columns: table => new
                {
                    Id_Pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_cuota = table.Column<int>(type: "int", nullable: true),
                    Fecha_pago = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Monto_pagado = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pago", x => x.Id_Pago);
                    table.ForeignKey(
                        name: "FK_Pago_Cuota_Id_cuota",
                        column: x => x.Id_cuota,
                        principalTable: "Cuota",
                        principalColumn: "Idcuota");
                });

            migrationBuilder.CreateTable(
                name: "PagoPropietario",
                columns: table => new
                {
                    Id_Pago = table.Column<int>(type: "int", nullable: false),
                    Id_Propietario = table.Column<int>(type: "int", nullable: false),
                    Id_Bien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagoPropietario", x => new { x.Id_Pago, x.Id_Propietario, x.Id_Bien });
                    table.ForeignKey(
                        name: "FK_PagoPropietario_Bien_Id_Bien",
                        column: x => x.Id_Bien,
                        principalTable: "Bien",
                        principalColumn: "Idbien",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PagoPropietario_Pago_Id_Pago",
                        column: x => x.Id_Pago,
                        principalTable: "Pago",
                        principalColumn: "Id_Pago",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PagoPropietario_Propietario_Id_Propietario",
                        column: x => x.Id_Propietario,
                        principalTable: "Propietario",
                        principalColumn: "IDPropietario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bien_Tipo",
                table: "Bien",
                column: "Tipo");

            migrationBuilder.CreateIndex(
                name: "IX_BienPropietario_Id_Propietario",
                table: "BienPropietario",
                column: "Id_Propietario");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_CodEstado",
                table: "Cuota",
                column: "CodEstado");

            migrationBuilder.CreateIndex(
                name: "IX_Cuota_Id_bien",
                table: "Cuota",
                column: "Id_bien");

            migrationBuilder.CreateIndex(
                name: "IX_Pago_Id_cuota",
                table: "Pago",
                column: "Id_cuota");

            migrationBuilder.CreateIndex(
                name: "IX_PagoPropietario_Id_Bien",
                table: "PagoPropietario",
                column: "Id_Bien");

            migrationBuilder.CreateIndex(
                name: "IX_PagoPropietario_Id_Propietario",
                table: "PagoPropietario",
                column: "Id_Propietario");

            migrationBuilder.CreateIndex(
                name: "IX_Propietario_Cod_estado_Civil",
                table: "Propietario",
                column: "Cod_estado_Civil");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BienPropietario");

            migrationBuilder.DropTable(
                name: "PagoPropietario");

            migrationBuilder.DropTable(
                name: "Pago");

            migrationBuilder.DropTable(
                name: "Propietario");

            migrationBuilder.DropTable(
                name: "Cuota");

            migrationBuilder.DropTable(
                name: "CDest_civ");

            migrationBuilder.DropTable(
                name: "Bien");

            migrationBuilder.DropTable(
                name: "CDCuotaestado");

            migrationBuilder.DropTable(
                name: "CDtipoBien");
        }
    }
}
