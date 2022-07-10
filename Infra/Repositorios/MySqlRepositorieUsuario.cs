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
                return usuario.FirstOrDefault(ret => ret.Login == login && ret.Senha == senha);
            }
        }
        #endregion
    }
}
