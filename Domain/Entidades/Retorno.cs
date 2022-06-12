using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entidades
{
    public class Retorno
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string Cliente { get; set; }
        public string Atualizacao { get; set; }
        public string TransacaoID { get; set; }
        public string Status { get; set; }
    }
}
