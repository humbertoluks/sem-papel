using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class guidenumbercontroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GUIA_NUMERO",
                schema: "ATENDIMENTO",
                columns: table => new
                {
                    CD_PRESTADOR = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    NR_GUIA_NUMERO = table.Column<int>(type: "int", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GUIA_NUMERO", x => x.CD_PRESTADOR);
                });

            migrationBuilder.InsertData(
                schema: "ATENDIMENTO",
                table: "GUIA_ORIGEM",
                columns: new[] { "GUIA_ORIGEM_ID", "GUIA_ORIGEM_DESCRICAO" },
                values: new object[] { 4, "TELEMEDICINA" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GUIA_NUMERO",
                schema: "ATENDIMENTO");

            migrationBuilder.DeleteData(
                schema: "ATENDIMENTO",
                table: "GUIA_ORIGEM",
                keyColumn: "GUIA_ORIGEM_ID",
                keyValue: 3);
        }
    }
}
