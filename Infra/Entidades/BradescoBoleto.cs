﻿using System;

namespace Infra.Entidades
{
    public class BradescoBoleto
    {
        public long agenciaDestino { get; set; }
        public string bairroPagador { get; set; }
        public string bairroSacadorAvalista { get; set; }
        public long cdEspecieTitulo { get; set; }
        public long cdIndCpfcnpjPagador { get; set; }
        public long cdIndCpfcnpjSacadorAvalista { get; set; }
        public string cdPagamentoParcial { get; set; }
        public long cepPagador { get; set; }
        public long cepSacadorAvalista { get; set; }
        public long codigoMoeda { get; set; }
        public long complementoCepPagador { get; set; }
        public long complementoCepSacadorAvalista { get; set; }
        public string complementoLogradouroPagador { get; set; }
        public string complementoLogradouroSacadorAvalista { get; set; }
        public string controleParticipante { get; set; }
        public long ctrlCPFCNPJ { get; set; }
        public string dataLimiteDesconto1 { get; set; }
        public string dataLimiteDesconto2 { get; set; }
        public string dataLimiteDesconto3 { get; set; }
        public long dddPagador { get; set; }
        public long dddSacadorAvalista { get; set; }
        public string dtEmissaoTitulo { get; set; }
        public string dtLimiteBonificacao { get; set; }
        public string dtVencimentoTitulo { get; set; }
        public string endEletronicoPagador { get; set; }
        public string endEletronicoSacadorAvalista { get; set; }
        public long filialCPFCNPJ { get; set; }
        public long formaEmissao { get; set; }
        public long idProduto { get; set; }
        public string logradouroPagador { get; set; }
        public string logradouroSacadorAvalista { get; set; }
        public string municipioPagador { get; set; }
        public string municipioSacadorAvalista { get; set; }
        public string nomePagador { get; set; }
        public string nomeSacadorAvalista { get; set; }
        public long nuCPFCNPJ { get; set; }
        public string nuCliente { get; set; }
        public long nuCpfcnpjPagador { get; set; }
        public long nuCpfcnpjSacadorAvalista { get; set; }
        public string nuLogradouroPagador { get; set; }
        public string nuLogradouroSacadorAvalista { get; set; }
        public long nuNegociacao { get; set; }
        public long nuTitulo { get; set; }
        public long percentualBonificacao { get; set; }
        public long percentualDesconto1 { get; set; }
        public long percentualDesconto2 { get; set; }
        public long percentualDesconto3 { get; set; }
        public long percentualJuros { get; set; }
        public long percentualMulta { get; set; }
        public long prazoBonificacao { get; set; }
        public long prazoDecurso { get; set; }
        public long prazoProtestoAutomaticoNegativacao { get; set; }
        public long qtdeDiasJuros { get; set; }
        public long qtdeDiasMulta { get; set; }
        public long qtdePagamentoParcial { get; set; }
        public long quantidadeMoeda { get; set; }
        public long registraTitulo { get; set; }
        public long telefonePagador { get; set; }
        public long telefoneSacadorAvalista { get; set; }
        public long tipoDecurso { get; set; }
        public long tpProtestoAutomaticoNegativacao { get; set; }
        public string ufPagador { get; set; }
        public string ufSacadorAvalista { get; set; }
        public long versaoLayout { get; set; }
        public decimal vlAbatimento { get; set; }
        public decimal vlBonificacao { get; set; }
        public decimal vlDesconto1 { get; set; }
        public decimal vlDesconto2 { get; set; }
        public decimal vlDesconto3 { get; set; }
        public decimal vlIOF { get; set; }
        public decimal vlJuros { get; set; }
        public decimal vlMulta { get; set; }
        public decimal vlNominalTitulo { get; set; }
    }
}
// https://github.com/wmixvideo/bradesco-boleto-registro/blob/master/src/main/java/com/github/wmixvideo/bradescoboleto/classes/BBRRegistroEntradaBoleto.java