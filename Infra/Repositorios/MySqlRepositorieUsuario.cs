using Infra.DataBase;
using Infra.Entidades;
using System.Linq;

namespace Infra.Repositorie
{
    public static class MySqlRepositorieUsuario
    {
        // RETORNO
        #region Retorno
        public static Usuario GetUsuario(string login, string senha)
        {
            using (var context = new MySqlDbContext())
            {
                // Creates the database if not exists
                context.Database.EnsureCreated();

                var usuario = context.Usuario;

                if(usuario.ToList().Count == 0)
                {
                    context.Usuario.Add(new Usuario()
                    {
                        Login = "Admin",
                        Senha = "Admin",
                        RevendaId = "0"
                    });

                    context.Usuario.Add(new Usuario()
                    {
                        Login = "Revenda",
                        Senha = "Revenda",
                        RevendaId = "1"
                    });

                    context.SaveChanges();
                }
                return usuario.FirstOrDefault(ret => ret.Login == login && ret.Senha == senha);
            }
        }
        #endregion
    }
}
