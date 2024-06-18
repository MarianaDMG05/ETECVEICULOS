using EtecVeiculos.Api.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace EtecVeiculos.Api.Data;

    public class AppDbSeed
    {
        public AppDbSeed(ModelBuilder modelBuilder)
        {
            #region TipoVeiculo
            List<TipoVeiculo> tipoVeiculos = [
                new(){
                    Id =1,
                    Name = "Moto"
                },
                new(){
                    Id =2,
                    Name = "Carro"
                },
                new(){
                    Id =3,
                    Name = "Caminhão"
                },
            ];
            modelBuilder.Entity<TipoVeiculo>().HasData(tipoVeiculos);
            #endregion

            #region Marcas
            List<Marca> marcas = [
                new(){
                    Id= 1,
                    Nome = "Toyota"
                },
                new(){
                    Id= 2,
                    Nome = "Fiat"
                },
                new(){
                    Id=3,
                    Nome="Mitsubishi"
                },
            ];
             modelBuilder.Entity<Marca>().HasData(marcas);
            #endregion

            #region Modelo
            List<Modelo> modelos = new() {
                new() {
                    Id=1,
                    Nome = "Yaris",
                    MarcaId=1,
                },
                new() {
                    Id=2,
                    Nome="Corolla",
                    MarcaId=1,
                },
                new(){
                    Id=3,
                    Nome="Pulse",
                    MarcaId=2,
                },
                new(){
                    Id=4,
                    Nome="Mobi",
                    MarcaId=2,
                },
                new(){
                    Id=5,
                    Nome="Lancer",
                    MarcaId=3,
                },
                new(){
                    Id=6,
                    Nome="Eclipse Cross",
                    MarcaId=3,
                },
            };
             modelBuilder.Entity<Modelo>().HasData(modelos);
             #endregion

        }
    }
    