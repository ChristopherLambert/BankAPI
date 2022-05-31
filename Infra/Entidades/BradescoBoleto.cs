﻿using System;

namespace Infra.Entidades
{
    public class BradescoBoleto
    {
        public int agenciaDestino { get; set; }
        public string bairroPagador { get; set; }
        public string bairroSacadorAvalista { get; set; }
        public int cdEspecieTitulo { get; set; }
        public int cdIndCpfcnpjPagador { get; set; }
        public int cdIndCpfcnpjSacadorAvalista { get; set; }
        public string cdPagamentoParcial { get; set; }
        public int cepPagador { get; set; }
        public int cepSacadorAvalista { get; set; }
        public int codigoMoeda { get; set; }
        public int complementoCepPagador { get; set; }
        public int complementoCepSacadorAvalista { get; set; }
        public string complementoLogradouroPagador { get; set; }
        public string complementoLogradouroSacadorAvalista { get; set; }
        public string controleParticipante { get; set; }
        public int ctrlCPFCNPJ { get; set; }
        public string dataLimiteDesconto1 { get; set; }
        public string dataLimiteDesconto2 { get; set; }
        public string dataLimiteDesconto3 { get; set; }
        public int dddPagador { get; set; }
        public int dddSacadorAvalista { get; set; }
        public string dtEmissaoTitulo { get; set; }
        public string dtLimiteBonificacao { get; set; }
        public string dtVencimentoTitulo { get; set; }
        public string endEletronicoPagador { get; set; }
        public string endEletronicoSacadorAvalista { get; set; }
        public int filialCPFCNPJ { get; set; }
        public int formaEmissao { get; set; }
        public int idProduto { get; set; }
        public string logradouroPagador { get; set; }
        public string logradouroSacadorAvalista { get; set; }
        public string municipioPagador { get; set; }
        public string municipioSacadorAvalista { get; set; }
        public string nomePagador { get; set; }
        public string nomeSacadorAvalista { get; set; }
        public int nuCPFCNPJ { get; set; }
        public string nuCliente { get; set; }
        public int nuCpfcnpjPagador { get; set; }
        public int nuCpfcnpjSacadorAvalista { get; set; }
        public string nuLogradouroPagador { get; set; }
        public string nuLogradouroSacadorAvalista { get; set; }
        public int nuNegociacao { get; set; }
        public int nuTitulo { get; set; }
        public int percentualBonificacao { get; set; }
        public int percentualDesconto1 { get; set; }
        public int percentualDesconto2 { get; set; }
        public int percentualDesconto3 { get; set; }
        public int percentualJuros { get; set; }
        public int percentualMulta { get; set; }
        public int prazoBonificacao { get; set; }
        public int prazoDecurso { get; set; }
        public int prazoProtestoAutomaticoNegativacao { get; set; }
        public int qtdeDiasJuros { get; set; }
        public int qtdeDiasMulta { get; set; }
        public int qtdePagamentoParcial { get; set; }
        public int quantidadeMoeda { get; set; }
        public int registraTitulo { get; set; }
        public int telefonePagador { get; set; }
        public int telefoneSacadorAvalista { get; set; }
        public int tipoDecurso { get; set; }
        public int tpProtestoAutomaticoNegativacao { get; set; }
        public string ufPagador { get; set; }
        public string ufSacadorAvalista { get; set; }
        public int versaoLayout { get; set; }
        public int vlAbatimento { get; set; }
        public int vlBonificacao { get; set; }
        public int vlDesconto1 { get; set; }
        public int vlDesconto2 { get; set; }
        public int vlDesconto3 { get; set; }
        public int vlIOF { get; set; }
        public int vlJuros { get; set; }
        public int vlMulta { get; set; }
        public int vlNominalTitulo { get; set; }
    }
}