using Newtonsoft.Json;
using RestSharp;
using Domain.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Domain.Services
{
    public static class RestApiV2
    {
        public static AcessToken PostToken(string url, string tokenBradesco)
        {
            var client = new RestClient("https://proxy.api.prebanco.com.br");
            //var client = new RestClient(url);
            var request = new RestRequest("/auth/server/v1.1/token", Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "urn:ietf:params:oauth:grant-type:jwt-bearer", ParameterType.GetOrPost);
            request.AddParameter("assertion", tokenBradesco);
            request.RequestFormat = DataFormat.Json;

            RestResponse response = client.ExecuteAsync(request).GetAwaiter().GetResult();
            return JsonConvert.DeserializeObject<AcessToken>(response.Content);
        }

        public static string PostBradesco(string token, BradescoBoleto boleto)
        {
            var now = DateTime.UtcNow;
            //BradescoBoleto boleto = new BradescoBoleto();

            var privateKeyPem = File.ReadAllText("C:\\temp\\Servopa.key.pem");
            privateKeyPem = privateKeyPem.Replace("-----BEGIN PRIVATE KEY-----", "");
            privateKeyPem = privateKeyPem.Replace("-----END PRIVATE KEY-----", "");

            string timeStamp = now.ToString("yyyy-MM-ddTHH:mm:ss-03:00");
            string requestTXT = "POST" + "\n" +
                                "/v1/boleto/registrarBoleto" + "\n" +
                                "\n" +
                                JsonConvert.SerializeObject(boleto) + "\n" +
                                // "{\"agenciaDestino\":1,\"bairroPagador\":null,\"bairroSacadorAvalista\":null,\"cdEspecieTitulo\":0,\"cdIndCpfcnpjPagador\":0,\"cdIndCpfcnpjSacadorAvalista\":0,\"cdPagamentoParcial\":null,\"cepPagador\":0,\"cepSacadorAvalista\":0,\"codigoMoeda\":0,\"complementoCepPagador\":0,\"complementoCepSacadorAvalista\":0,\"complementoLogradouroPagador\":null,\"complementoLogradouroSacadorAvalista\":null,\"controleParticipante\":null,\"ctrlCPFCNPJ\":0,\"dataLimiteDesconto1\":null,\"dataLimiteDesconto2\":null,\"dataLimiteDesconto3\":null,\"dddPagador\":0,\"dddSacadorAvalista\":0,\"dtEmissaoTitulo\":null,\"dtLimiteBonificacao\":null,\"dtVencimentoTitulo\":null,\"endEletronicoPagador\":null,\"endEletronicoSacadorAvalista\":null,\"filialCPFCNPJ\":0,\"formaEmissao\":0,\"idProduto\":0,\"logradouroPagador\":null,\"logradouroSacadorAvalista\":null,\"municipioPagador\":null,\"municipioSacadorAvalista\":null,\"nomePagador\":null,\"nomeSacadorAvalista\":null,\"nuCPFCNPJ\":0,\"nuCliente\":null,\"nuCpfcnpjPagador\":0,\"nuCpfcnpjSacadorAvalista\":0,\"nuLogradouroPagador\":null,\"nuLogradouroSacadorAvalista\":null,\"nuNegociacao\":0,\"nuTitulo\":0,\"percentualBonificacao\":0,\"percentualDesconto1\":0,\"percentualDesconto2\":0,\"percentualDesconto3\":0,\"percentualJuros\":0,\"percentualMulta\":0,\"prazoBonificacao\":0,\"prazoDecurso\":0,\"prazoProtestoAutomaticoNegativacao\":0,\"qtdeDiasJuros\":0,\"qtdeDiasMulta\":0,\"qtdePagamentoParcial\":0,\"quantidadeMoeda\":0,\"registraTitulo\":0,\"telefonePagador\":0,\"telefoneSacadorAvalista\":0,\"tipoDecurso\":0,\"tpProtestoAutomaticoNegativacao\":0,\"ufPagador\":null,\"ufSacadorAvalista\":null,\"versaoLayout\":0,\"vlAbatimento\":0,\"vlBonificacao\":0,\"vlDesconto1\":0,\"vlDesconto2\":0,\"vlDesconto3\":0,\"vlIOF\":0,\"vlJuros\":0,\"vlMulta\":0,\"vlNominalTitulo\":0}" + "\n" +
                                token + "\n" +
                                "1" + "\n" +
                                 timeStamp + "\n" +
                                "SHA256";

            byte[] privateKeyRaw = Convert.FromBase64String(privateKeyPem);
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
            provider.ImportPkcs8PrivateKey(new ReadOnlySpan<byte>(privateKeyRaw), out _);

            byte[] byteSign = Encoding.Default.GetBytes(requestTXT);

            var byteRSA = provider.SignData(byteSign, CryptoConfig.MapNameToOID("SHA256"));
            string Signature = Convert.ToBase64String(byteRSA);


            return RestApiV2.RequestBradesco(Signature, JsonConvert.SerializeObject(boleto), token, timeStamp);
        }

        public static string RequestBradesco(string signature, string body, string token, string timeStamp)
        {
            var client = new RestClient("https://proxy.api.prebanco.com.br");
            var request = new RestRequest("/v1/boleto/registrarBoleto", Method.Post);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("content-type", "application/json");
            request.AddHeader("x-brad-algorithm", "SHA256");
            request.AddHeader("x-brad-timestamp", timeStamp);
            request.AddHeader("x-brad-signature", signature);
            request.AddHeader("x-brad-nonce", "1");
            request.AddHeader("authorization", token);
            request.AddStringBody(body, DataFormat.Json);
            RestResponse response = client.ExecuteAsync(request).GetAwaiter().GetResult();

            return response.Content.ToString();
        }
    }
}
