using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ATENDIMENTO");

            migrationBuilder.CreateTable(
                name: "BENEFICIARIO_CHECKIN_STATUS",
                schema: "ATENDIMENTO",
                columns: table => new
                {
                    BENEFICIARIO_CHECKIN_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BENEFICIARIO_CHECKIN_DESCRICAO = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BENEFICIARIO_CHECKIN_STATUS", x => x.BENEFICIARIO_CHECKIN_ID);
                });

            migrationBuilder.CreateTable(
                name: "GUIA_ORIGEM",
                schema: "ATENDIMENTO",
                columns: table => new
                {
                    GUIA_ORIGEM_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUIA_ORIGEM_DESCRICAO = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GUIA_ORIGEM", x => x.GUIA_ORIGEM_ID);
                });

            migrationBuilder.CreateTable(
                name: "GUIA_STATUS",
                schema: "ATENDIMENTO",
                columns: table => new
                {
                    GUIA_STATUS_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUIA_STATUS_DESCRICAO = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GUIA_STATUS", x => x.GUIA_STATUS_ID);
                });

            migrationBuilder.CreateTable(
                name: "GUIA_TIPO",
                schema: "ATENDIMENTO",
                columns: table => new
                {
                    GUIA_TIPO_ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GUIA_TIPO_DESCRICAO = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    GUIA_TIPO_LOCAL = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GUIA_TIPO", x => x.GUIA_TIPO_ID);
                });

            migrationBuilder.CreateTable(
                name: "GUIA",
                schema: "ATENDIMENTO",
                columns: table => new
                {
                    GUIA_ID = table.Column<decimal>(type: "decimal(18,0)", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOTE_ID = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    PRESTADOR_ID = table.Column<string>(type: "varchar(50)", nullable: true),
                    PRESTADOR_LOGIN_ID = table.Column<int>(nullable: true),
                    PRESTADOR_UNIDADE_ID = table.Column<string>(type: "varchar(50)", nullable: true),
                    GUIA_TOKEN = table.Column<string>(type: "varchar(50)", nullable: true),
                    GUIA_BENEFICIARIO_TOKEN = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    GUIA_NUMERO = table.Column<string>(type: "varchar(50)", nullable: true),
                    GUIA_NUMERO_OPERADORA = table.Column<string>(type: "varchar(50)", nullable: true),
                    GUIA_BENEFICIARIO_CARTAO = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    GUIA_BENEFICIARIO = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    GUIA_VALOR = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GUIA_DATA = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "getdate()"),
                    GUIA_XML = table.Column<string>(type: "varchar(max)", nullable: true),
                    GUIA_DELETADA = table.Column<bool>(nullable: false),
                    GUIA_ORIGEM_ID = table.Column<int>(nullable: false),
                    GUIA_STATUS_ID = table.Column<int>(nullable: false),
                    GUIA_TIPO_ID = table.Column<int>(nullable: false),
                    GUIA_BENEFICIARIO_CHECKIN_STATUS_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GUIA", x => x.GUIA_ID);
                    table.ForeignKey(
                        name: "FK_GUIA_GUIA_ORIGEM_GUIA_ORIGEM_ID",
                        column: x => x.GUIA_ORIGEM_ID,
                        principalSchema: "ATENDIMENTO",
                        principalTable: "GUIA_ORIGEM",
                        principalColumn: "GUIA_ORIGEM_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GUIA_GUIA_STATUS_GUIA_STATUS_ID",
                        column: x => x.GUIA_STATUS_ID,
                        principalSchema: "ATENDIMENTO",
                        principalTable: "GUIA_STATUS",
                        principalColumn: "GUIA_STATUS_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GUIA_GUIA_TIPO_GUIA_TIPO_ID",
                        column: x => x.GUIA_TIPO_ID,
                        principalSchema: "ATENDIMENTO",
                        principalTable: "GUIA_TIPO",
                        principalColumn: "GUIA_TIPO_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GUIA_BENEFICIARIO_CHECKIN_STATUS_GUIA_BENEFICIARIO_CHECKIN_STATUS_ID",
                        column: x => x.GUIA_BENEFICIARIO_CHECKIN_STATUS_ID,
                        principalSchema: "ATENDIMENTO",
                        principalTable: "BENEFICIARIO_CHECKIN_STATUS",
                        principalColumn: "BENEFICIARIO_CHECKIN_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "ATENDIMENTO",
                table: "BENEFICIARIO_CHECKIN_STATUS",
                columns: new[] { "BENEFICIARIO_CHECKIN_ID", "BENEFICIARIO_CHECKIN_DESCRICAO" },
                values: new object[,]
                {
                    { 1, "VALIDADO" },
                    { 2, "NÃO VALIDADO" },
                    { 3, "NÃO RESPONDEU A TEMPO - TIMEOUT" },
                    { 4, "ATENDIMENTO MENOR - ACOMPANHANTE SEM PLANO" }
                });

            migrationBuilder.InsertData(
                schema: "ATENDIMENTO",
                table: "GUIA_ORIGEM",
                columns: new[] { "GUIA_ORIGEM_ID", "GUIA_ORIGEM_DESCRICAO" },
                values: new object[,]
                {
                    { 1, "URL" },
                    { 2, "PORTAL" }
                });

            migrationBuilder.InsertData(
                schema: "ATENDIMENTO",
                table: "GUIA_STATUS",
                columns: new[] { "GUIA_STATUS_ID", "GUIA_STATUS_DESCRICAO" },
                values: new object[,]
                {
                    { 1, "ABERTA" },
                    { 2, "FECHADA" },
                    { 3, "NÃO VALIDADA" }
                });

            migrationBuilder.InsertData(
                schema: "ATENDIMENTO",
                table: "GUIA_TIPO",
                columns: new[] { "GUIA_TIPO_ID", "GUIA_TIPO_DESCRICAO", "GUIA_TIPO_LOCAL" },
                values: new object[,]
                {
                    { 1, "SP-SATD", 0 },
                    { 2, "RESUMO INTERNAÇÃO", 0 },
                    { 3, "HONORÁRIOS", 0 },
                    { 4, "CONSULTA", 0 },
                    { 5, "ODONTOLOGIA", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GUIA_GUIA_ORIGEM_ID",
                schema: "ATENDIMENTO",
                table: "GUIA",
                column: "GUIA_ORIGEM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GUIA_GUIA_STATUS_ID",
                schema: "ATENDIMENTO",
                table: "GUIA",
                column: "GUIA_STATUS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GUIA_GUIA_TIPO_ID",
                schema: "ATENDIMENTO",
                table: "GUIA",
                column: "GUIA_TIPO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_GUIA_GUIA_BENEFICIARIO_CHECKIN_STATUS_ID",
                schema: "ATENDIMENTO",
                table: "GUIA",
                column: "GUIA_BENEFICIARIO_CHECKIN_STATUS_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GUIA",
                schema: "ATENDIMENTO");

            migrationBuilder.DropTable(
                name: "GUIA_ORIGEM",
                schema: "ATENDIMENTO");

            migrationBuilder.DropTable(
                name: "GUIA_STATUS",
                schema: "ATENDIMENTO");

            migrationBuilder.DropTable(
                name: "GUIA_TIPO",
                schema: "ATENDIMENTO");

            migrationBuilder.DropTable(
                name: "BENEFICIARIO_CHECKIN_STATUS",
                schema: "ATENDIMENTO");
        }
    }
}
