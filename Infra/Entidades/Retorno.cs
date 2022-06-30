using System;

namespace Infra.Entidades
{
    public class Retorno
    {
        public int Id { get; set; }
        public string TransacaoID { get; set; }
        public string Titulo { get; set; }
        public string Empresa { get; set; }
        public string Cliente { get; set; }
        public string Ocorrencia { get; set; }
        public string OcorrenciaCampos { get; set; }
        public string Valor { get; set; }
        public string Status { get; set; }
        public DateTime InsertData { get; set; }
        public DateTime UpdateData { get; set; }
    }
}
