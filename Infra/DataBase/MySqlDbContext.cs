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
                optionsBuilder.UseMySQL("Server=10.100.1.65;User Id=administrador;Password=#uTestBLT22;Database=uTestBoleto");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }
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
