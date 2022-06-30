using System.Collections.Generic;

namespace Domain.Models
{
    public class RespBradesco
    {
        public int codigo { get; set; }
        public string mensagem { get; set; }
        public List<RespBradescoError> errosValidacao { get; set; }
    }

    public class RespBradescoError
    {
        public string campo { get; set; }
        public string mensagem { get; set; }
    }
}

