using Infra.Entidades;
using Microsoft.EntityFrameworkCore;

namespace Infra.DataBase
{
    public partial class MySqlDbContext : DbContext
    {
        public DbSet<Empresa> Empresa { get; set; }

        public DbSet<Retorno> Retorno { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               // optionsBuilder.UseMySQL("Server=10.100.1.65;User Id=administrador;Password=#uTestBLT22;Database=uTestBoleto");
                optionsBuilder.UseMySQL("Server=10.100.1.65;User Id=ubratestvpn;Password=T3sTBr#2;Database=uTestBoleto");
            }

            //optionsBuilder.UseMySQL("Server=10.100.1.65;User Id=administrador;Password=#uTestBLT22;Database=uTestBoleto", builder =>
            //{
            //    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            //});
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired();
                entity.Property(e => e.Revenda).IsRequired();
            });

            modelBuilder.Entity<Retorno>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Empresa).IsRequired();
                entity.Property(d => d.Atualizacao);
                entity.Property(d => d.TransacaoID);
                entity.Property(d => d.Status);
            });
        }
    }
}

//SRV - BOLETOPROD[Produção]
//Mysql Windows
//ip: 10.100.1.65      ip: 10.100.1.164
//banco: uProdBoleto Login: administrador
//login: ubraprod Senha: #uProdBLT22
//senha: Br4B0L#2

//SRV - BOLETOTEST[Teste]
//Mysql Windows
//ip: 10.100.1.65            ip: 10.100.1.165
//banco: uTestBoleto Login: administrador
//login: ubratest Senha: #uTestBLT22
//senha: T3sTBr#2 
