using System.Collections.Generic;

namespace BankAPI.Models
{
    public class HistoricoViewModel
    {
        public int EmpresaId { get; set; } = 0;
        public int RevendaId { get; set; }
        public List<RetornoViewModel> Retornos { get; set; }
        public List<DropDownViewModel> Empresas { get; set; } = new List<DropDownViewModel>();
        public List<DropDownViewModel> Revendas { get; set; } = new List<DropDownViewModel>();
    }

    public class DropDownViewModel
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }

    public class RetornoViewModel
    {
        public int Id { get; set; }
        public string TransacaoID { get; set; }
        public string Titulo { get; set; }
        public string Empresa { get; set; }
        public string Revenda { get; set; }
        public string RevendaCodigo { get; set; }
        public string Parcela { get; set; }
        public string Cliente { get; set; }
        public string Ocorrencia { get; set; }
        public string OcorrenciaCampos { get; set; }
        public string Valor { get; set; }
        public string Status { get; set; }
        public string InsertData { get; set; }
    }
}
