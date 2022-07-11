using Infra.Repositorie;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BankAPI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logar(string login, string senha)
        {
            try
            {

                var user = MySqlRepositorieUsuario.GetUsuario(login, senha);
                if(user != null)
                {
                    Startup.Login = new Models.LoginViewModel()
                    {
                        Login = user.Login,
                        Senha = user.Senha,
                        RevendaId = user.RevendaId
                    };

                    // return RedirectToAction("Index", "Home");
                    return new JsonResult(new { Success = true });
                }

                return new JsonResult(new { Success = false });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Success = false });
            }
        }
    }
}
