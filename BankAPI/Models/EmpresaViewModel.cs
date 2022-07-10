using System.Collections.Generic;

namespace BankAPI.Models
{
    public class HomeViewModel
    {
        public int EmpresaId { get; set; }
        public EmpresaViewModel MainEmpresa { get; set; }
        public List<EmpresaViewModel> Empresas { get; set; }
    }

    public class EmpresaViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Numero { get; set; }
        public string Revenda { get; set; }
        public string RevendaNumero { get; set; }
        public string Banco { get; set; }
        public string Origem { get; set; }
        public string Departamento { get; set; }
    }
}
