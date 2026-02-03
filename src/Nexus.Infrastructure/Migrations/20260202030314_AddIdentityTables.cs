using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Filmes_FilmeId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Jogos_JogoId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmeGeneros_Filmes_FilmesId",
                table: "FilmeGeneros");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmeGeneros_Generos_GenerosId",
                table: "FilmeGeneros");

            migrationBuilder.DropForeignKey(
                name: "FK_JogoGeneros_Generos_GenerosId",
                table: "JogoGeneros");

            migrationBuilder.DropForeignKey(
                name: "FK_JogoGeneros_Jogos_JogosId",
                table: "JogoGeneros");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_Titulo",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Filmes_Titulo",
                table: "Filmes");

            migrationBuilder.RenameColumn(
                name: "JogosId",
                table: "JogoGeneros",
                newName: "JogoId");

            migrationBuilder.RenameColumn(
                name: "GenerosId",
                table: "JogoGeneros",
                newName: "GeneroId");

            migrationBuilder.RenameIndex(
                name: "IX_JogoGeneros_JogosId",
                table: "JogoGeneros",
                newName: "IX_JogoGeneros_JogoId");

            migrationBuilder.RenameColumn(
                name: "GenerosId",
                table: "FilmeGeneros",
                newName: "GeneroId");

            migrationBuilder.RenameColumn(
                name: "FilmesId",
                table: "FilmeGeneros",
                newName: "FilmeId");

            migrationBuilder.RenameIndex(
                name: "IX_FilmeGeneros_GenerosId",
                table: "FilmeGeneros",
                newName: "IX_FilmeGeneros_GeneroId");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Jogos",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaUsuario",
                table: "Jogos",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,1)",
                oldPrecision: 3,
                oldScale: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Generos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Filmes",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaUsuario",
                table: "Filmes",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,1)",
                oldPrecision: 3,
                oldScale: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Nota",
                table: "Avaliacoes",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,1)",
                oldPrecision: 3,
                oldScale: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Conteudo",
                table: "Avaliacoes",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NomeCompleto",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_UsuarioId",
                table: "Jogos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Filmes_UsuarioId",
                table: "Filmes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacoes_UsuarioId",
                table: "Avaliacoes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_AspNetUsers_UsuarioId",
                table: "Avaliacoes",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Filmes_FilmeId",
                table: "Avaliacoes",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Jogos_JogoId",
                table: "Avaliacoes",
                column: "JogoId",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmeGeneros_Filmes_FilmeId",
                table: "FilmeGeneros",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmeGeneros_Generos_GeneroId",
                table: "FilmeGeneros",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Filmes_AspNetUsers_UsuarioId",
                table: "Filmes",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JogoGeneros_Generos_GeneroId",
                table: "JogoGeneros",
                column: "GeneroId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JogoGeneros_Jogos_JogoId",
                table: "JogoGeneros",
                column: "JogoId",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Jogos_AspNetUsers_UsuarioId",
                table: "Jogos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_AspNetUsers_UsuarioId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Filmes_FilmeId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacoes_Jogos_JogoId",
                table: "Avaliacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmeGeneros_Filmes_FilmeId",
                table: "FilmeGeneros");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmeGeneros_Generos_GeneroId",
                table: "FilmeGeneros");

            migrationBuilder.DropForeignKey(
                name: "FK_Filmes_AspNetUsers_UsuarioId",
                table: "Filmes");

            migrationBuilder.DropForeignKey(
                name: "FK_JogoGeneros_Generos_GeneroId",
                table: "JogoGeneros");

            migrationBuilder.DropForeignKey(
                name: "FK_JogoGeneros_Jogos_JogoId",
                table: "JogoGeneros");

            migrationBuilder.DropForeignKey(
                name: "FK_Jogos_AspNetUsers_UsuarioId",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Jogos_UsuarioId",
                table: "Jogos");

            migrationBuilder.DropIndex(
                name: "IX_Filmes_UsuarioId",
                table: "Filmes");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacoes_UsuarioId",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NomeCompleto",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "JogoId",
                table: "JogoGeneros",
                newName: "JogosId");

            migrationBuilder.RenameColumn(
                name: "GeneroId",
                table: "JogoGeneros",
                newName: "GenerosId");

            migrationBuilder.RenameIndex(
                name: "IX_JogoGeneros_JogoId",
                table: "JogoGeneros",
                newName: "IX_JogoGeneros_JogosId");

            migrationBuilder.RenameColumn(
                name: "GeneroId",
                table: "FilmeGeneros",
                newName: "GenerosId");

            migrationBuilder.RenameColumn(
                name: "FilmeId",
                table: "FilmeGeneros",
                newName: "FilmesId");

            migrationBuilder.RenameIndex(
                name: "IX_FilmeGeneros_GeneroId",
                table: "FilmeGeneros",
                newName: "IX_FilmeGeneros_GenerosId");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Jogos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaUsuario",
                table: "Jogos",
                type: "numeric(3,1)",
                precision: 3,
                scale: 1,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Generos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Filmes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<decimal>(
                name: "NotaUsuario",
                table: "Filmes",
                type: "numeric(3,1)",
                precision: 3,
                scale: 1,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Nota",
                table: "Avaliacoes",
                type: "numeric(3,1)",
                precision: 3,
                scale: 1,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Conteudo",
                table: "Avaliacoes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000);

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_Titulo",
                table: "Jogos",
                column: "Titulo");

            migrationBuilder.CreateIndex(
                name: "IX_Filmes_Titulo",
                table: "Filmes",
                column: "Titulo");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Filmes_FilmeId",
                table: "Avaliacoes",
                column: "FilmeId",
                principalTable: "Filmes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacoes_Jogos_JogoId",
                table: "Avaliacoes",
                column: "JogoId",
                principalTable: "Jogos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmeGeneros_Filmes_FilmesId",
                table: "FilmeGeneros",
                column: "FilmesId",
                principalTable: "Filmes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmeGeneros_Generos_GenerosId",
                table: "FilmeGeneros",
                column: "GenerosId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JogoGeneros_Generos_GenerosId",
                table: "JogoGeneros",
                column: "GenerosId",
                principalTable: "Generos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JogoGeneros_Jogos_JogosId",
                table: "JogoGeneros",
                column: "JogosId",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
