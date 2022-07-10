using BankAPI.Models;
using Domain.Entidades;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace BankAPI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            try
            {
                List<Empresa> empresas = MySqlServicesEmpresa.GetAllEmpresa();

                var homeModel = new HomeViewModel();
                homeModel.EmpresaId = 0;
                homeModel.Empresas = empresas.Select((item) =>
                {
                    return new EmpresaViewModel()
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Numero = item.Numero,
                        Revenda = item.Revenda,
                        RevendaNumero = item.RevendaNumero,
                        Banco = item.Banco,
                        Origem = item.Origem,
                        Departamento = item.Departamento
                    };
                }).ToList();

                if (id != 0)
                {
                    var empresa = empresas.FirstOrDefault(emp => emp.Id == id);
                    if (empresa != null)
                    {
                        homeModel.EmpresaId = id;
                        homeModel.MainEmpresa = new EmpresaViewModel()
                        {
                            Id = empresa.Id,
                            Nome = empresa.Nome,
                            Numero = empresa.Numero,
                            Revenda = empresa.Revenda,
                            RevendaNumero = empresa.RevendaNumero,
                            Banco = empresa.Banco,
                            Origem = empresa.Origem,
                            Departamento = empresa.Departamento
                        };
                    }
                    return View(homeModel);

                }
                else
                {
                    var empresa = empresas.FirstOrDefault();

                    if (empresa != null)
                    {
                        homeModel.EmpresaId = empresa.Id;
                        homeModel.MainEmpresa = new EmpresaViewModel()
                        {
                            Id = empresa.Id,
                            Nome = empresa.Nome,
                            Numero = empresa.Numero,
                            Revenda = empresa.Revenda,
                            RevendaNumero = empresa.RevendaNumero,
                            Banco = empresa.Banco,
                            Origem = empresa.Origem,
                            Departamento = empresa.Departamento
                        };
                    }
                    return View(homeModel);
                }

            }
            catch (Exception ex)
            {
                return View(new HomeViewModel());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Salvar(int id, string empresaNome, string empresaNumero, string revenda, string revendaNumero, string banco, string departamento, string origem)
        {
            try
            {
                var empresa = new Empresa()
                {
                    Id = id,
                    Nome = empresaNome,
                    Numero = empresaNumero,
                    Revenda = revenda,
                    RevendaNumero = revendaNumero,
                    Origem = origem,
                    Departamento = departamento,
                    Banco = banco
                };

                var resp = MySqlServicesEmpresa.UpdateEmpresa(empresa);

                if (!resp.Success)
                    resp = MySqlServicesEmpresa.AddEmpresa(empresa);

                var resultado = new
                {
                    Id = resp.Id,
                    Success = resp.Success
                };

                return new JsonResult(resultado);
            }
            catch (Exception)
            {
                return new JsonResult(new { Success = false });
            }
        }

        public IActionResult Deletar(int id)
        {
            try
            {
                MySqlServicesEmpresa.DeleteEmpresa(id);
                return new JsonResult(new { Success = true });
            }
            catch (Exception)
            {
                return new JsonResult(new { Success = false });
            }
        }
    }
}
