using System;
using System.Threading.Tasks;
using Domain.Models;
using Newtonsoft.Json;
using Infra.DataBase;
using Domain.Helper;

namespace Domain.Services
{
    public static class IntegrationService
    {
        //Urls
        private static readonly string EditorSwwager = "https://proxy.api.prebanco.com.br/auth/server/v1.1/token";
        private static readonly string GetTokenBradesco = "https://proxy.api.prebanco.com.br/auth/server/v1.1/token";  //URL Para pegar o token
                                                                                                                       //private static readonly string RegistrarBoleto = "https://openapi.bradesco.com.br/v1 e TH=proxy.api.prebanco.com.br/v1/v1";
        private static readonly string RegistrarBoleto = "https://openapi.bradesco.com.br/v1";

        // Variaveis
        private static string tokenBradesco = string.Empty;
        // private static string tokenBradesco = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzUxMiJ9.ew0KICJ2ZXIiOiAiMS4wIiwNCiAiaXNzIjogImh0dHBzOi8vcHJveHkuYXBpLnByZWJhbmNvLmNvbS5ici9hdXRoL3NlcnZlci92MS4xL3Rva2VuIiwNCiAiYXVkIjogImh0dHBzOi8vcHJveHkuYXBpLnByZWJhbmNvLmNvbS5icjo4NDQzIiwNCiAiaWF0IjogMTY1MzM0MjUwNSwNCiAiZXhwIjogMTY1MzM0NTg0MSwNCiAic2NwIjogIkJvbGV0byIsDQogImp0aSI6ICJtb2NrU2Vzc2lvbjg1OTUxNTc5NTQ1MzY4MDAxMTU1MzE2OTc2NDE4MTEzNzkwMTc5IiwNCiAidG9rZW5UeXBlIjogImFjY2VzcyIsDQogImNsaWVudFR5cGUiOiAic2VydmVyIiwNCiAib3duZXJUeXBlIiA6ICJzZXJ2ZXIiLA0KICJhdXRoRGF0YSI6ICJleUowZVhBaU9pSktWMVFpTENKamRIa2lPaUpLVjFRaUxDSmhiR2NpT2lKU1UwRXRUMEZGVUNJc0ltVnVZeUk2SWtFeU5UWkRRa010U0ZNMU1USWlmUS5maGRlaTBkUGpoelB6LWdvd1BxbFhKTTZGcGhvTFRFdF9SRE5aQnNMWDgzcWR4eExST0h1VS1hVGRPdkZfMnhfVi1UOUl1MjEwZEpETnVqWWpWempCbmdhY05ZeWdmNFNnOVBkQzQ0VVprZkxpbVNOcExUMTA3SW5OcWlNNmYyV3YtdHlZcjNvbXkyaEpYQ2xQMzBmWDk4NnlhZXRjNFRrRGtvQzlnWHNLVTNnRUFzOFVnWDBCeUkya2JoRzh5UkEwa01uY2ZjQUNGWEZNekJrZXZ0dnFTd3ZuYnpIQ3lMYWVjcTY4cWRIYWFZNzdFZlVkVE9yRmh5S0pjaWpiS0RiOGR6dzlCOHN3TnFzVzdXeVlXakpfSWRmNndFcl9SWWM0U2JQOW4xZTdQUmRGQ0o2Y2F4dlFRNkIzWEx4dXJud0dLSnR1bDcwX0RlSzBQN2hqMnhmcW9pQjc2NW5MQ3lKUXdFME5XWmQ5cmZzY2lVUUN2VUJpcGx2cjEtcTREZVRsOE1vd21UdWxGNGpiWUczWHhGMDBsZDVmM0tLejZQRkVMeDJZU3gwejhDLVR2LXNLYmFDRTUxOUlRMndOVFI0QlZBM3dRRkRkTkUzQ1JwWHBLY0lXa09JQzR4VTZpRG80bmlOLVI5bWpBT1BnSWJ6Mk02cmtjY0toRC1QNmNHTkVFaHZsQnFuUVozMno5NmdEenhDcjJlTnhBamdqakdPcTBSMnp6anNpN2Jfd1dQRGZtajNsVDVSMHh0RkRnUGpLVHN1ajVycm1zQlkxS2taNHdidGt3Tm1GeVNhc0RpVEdyZVZpTlZmYUdtendPNmh3LVFGX2RSbUNFc0paZktUeGhtNlpyUl8yRG1OTDhEQTNQZUtWN3FWMUw3b0I0LTFsZ3BpYi1wNkhIdy5qX3RCTWZ1ckxkOFU2dVpFUVpXXzVBLjFRaHQ3bEtWOUt1QjAtblh1SENya1o1SmRGeE5SMzlIMGhpdnJ4RGFvbkNkVmlYZHZHTzhZRlVadVJNVlFqZ3l2U0lsQjJnSlFiWVVta1drNXlnNG9LSG9QSk1Ndk1Rekt0OFNCOGkwRkpubVdTSVhkcXBoOGJGcUhfZXFScndFc1g0d0gyN0VCejQ0aENKVmxzV1NWU1ZPYnNDTVUwTzk5di1vUjQxSXVCV3lJNHJFOEdJYjBxZ214NGJfTFB3ZHZQNUJvdWRESk5CSXVoU1ZkOXMyWmZnaTY4aUZQLWdxZW1WeEw0TDFaeXNHaVByTjI3bm1wdlUxS3hUQTdoWVZla2ViUmtnTzJIc2F0RUNiQkRMTkZQWi1PZS1wRmFrRlJia2x5YzBsZXhUZVB2ZjBNNEcxSmgxeWtrZ0VmWmc0d2FGd2hXQnBYUnFYeGlBTzdCUG5hOWVSTE9oaTgxS2IxUnhJR2ZhbWZZUDhNajJMUnNzMGt2MjM3Y2NReWI4U1Y2ZG5YQW1EeW85UDNNNjAxcy1LV3hKV2w2MmRWdW9lTkQ5U25sZ3pkTmduNGVXS21lUm5IOTVqaVVSelZQb3k1YktIc2t6ODRMTnNUdGRBWl93am1jYmtpUzVOdk41YnhZUWx5d1o4dnd0aUZyX1VFMDM4dGl5NGJXbWszVEgxRFZ5eVZUTWsxX1VWN0dXaC1WdVZzNVUxTDl6SHZTblN4ZWluOWZGbEF6bkQ1U2JVWjI3R3RfbExac2JIVlAwZGtZUWNSZVNNeUY5TGJieWVBN0pLYVhkcEJEUXpEbktLaVVRQ1RwSDZaQ05HLTFMUVpHZkhYa0lOYXFnNFAyWjVlWDIwaWNmT1hSZVk4d2pYNm41OE1TSlJfckM5RzN0OFRzUEJBbWE5T1ZkcEhaMnptVWgyYnVXU0JJZjJMaE9sME8zY0JVWlhXMmhKcEFtS2V3aUsxNXZpWU43eUFZSzdtc0VuZ1RaRHZzelhZalRtcWRKRVFsb3V3ZWJBaHVoU2dCMjZTblhlZGk5cWJiWEY5bnRWVU56V01BVDlGeWdTVUxnanF1UmFJUWNvdTlDcE1MLV9FYzZfa0tkMmhTTUxjZjZkS2s3dkk0M0t4Vno3dkE3eVVWd19kTmVpQnZvZWtIOGEyTzZtcTNrOEctbktzeGk1RFBGU2hRemlkVGpLOS1KLWwwYjk2OHhhUnY5aHh4Y180b3AxTUVMNVJNSVg2M1dsaU9TdW8xQWltbkZzU3IyTVFQWjNIN2ViR1dUWXliSVZRV2czMXpxOUZKYWd6X3BXVmhXREJUTnFUZzRqRlRmZkhyRUNmWkRFbExGZnZIRExjd1kyMGJqUTBFLTZrMjFNTzN4MW1zd1h0YXVVYjJrVnkyaWVzZGp4bGQ0eW9vdC1tSExLVUNVak1TaWVQaHp2Z254TTVQWU1RSi1qVjFIaGhGWG0zajRrcUV4b1Y3UTY1Nm5tZlBOV1ctN1NZdW5BdjhSd3JfMVRmcWZOTmROaU9yWkcyd0hpUktrWERfYjRjek9vRXdMZTlkVTBtSmV1ZzdWMExVVHpULTNvekhldC1XWG1lM1JxbUU1dWhReXJDN1Z4SXUzc1ZoSTlHaE9LR3N1UUxXUnhGQnpxU092N2JxNzc2SDFKU2hueDRMakhmVkxGMFZOSkw5MVhzUlp5ZTMzNk44OUlrM1J2allmUjdEakxVMUVNaVN0QkgtNXFhYWFRMXVhLVBqOVdyQ0RlNXJseXF1Z2g5WXlGQ0Nzd2JmTGFZTGlMVFRzNkN4a0hmZEgyTjNndWNmQ2JGU3hEU2hBUVJ0ZXpxVGVrVWw5TEVQZ0hwaWlJVnQ4UFNub3ZZLWkxZWJ5c3hJcjJzVTVVaW5kalhXX216dVRDYmh2NGFBLkVRWWFzRExkTExYSWVDZGduZEVQMjkzcFRfZGZ4cXoyeUpkWlVKdjdoeGMiDQp9.s_Z6XRKmlVLXz1i_ZngR_7RprwjV8_R7E4CqIi1fA2c1SeHxrvoNbLHLj1fqlSoQ0ngAvyVips3IOlL0-OEuprh3HSTamRSCSQPmSLtUIYCeEDAwkVVWti223nhO0z7K4HUmmKJoP9nLvNeZRecS5KkW4ksqETWAn2yuPMwLD_u1QPaQmOeQ_P7q31GmeGWVQ1qsiqlXX8XjNeggAMd3s-OVvdKeZZ-VmkoiuCVB6kc3znoG9CayvbWgEcbt0DRPVf1qu6pp_vnDa3G2J1YJd3ImeZEhCF6i0JzlfxcMYItDtRU3xxYVsYOd6gUZBr0oEtnP5fbDki6fmVjQD4GkZJvFcOoTzke-8000OoylgLlbRvm7Xml6uyePYpUwd_xea1BSDxE1Fhd2C-vzsUZl3Sloop9ZAXSViwgnXOBo13b4slpfWNh9NzLxa4tGCpkYQd9CbVtggRAjWcFC9Kq1tYCgqRSKC_iE1QyYUwAXng7qha04rbe0E9vb2D1A87KEEF7ossqfITmYSaYSPsXyAT1Qmf6cEOagrvw2nCe24nH5liLNxnxF0nQlMoGg86l0u04sYmbaBbJDDFyqCyBBljDmBkNcZbVPdlRIGKBJ62vBeT0lUBBnd1g1W8WC8z4syMhh-XE1-86q2EajxqXXaRq-uevFL2koqlvLw3krKFA";
        private static string clientKey = "e55e6ce8-c55d-4bb0-b546-c19ec90a3f11";
        private static string clientKSecret = "b7beb315-2067-4084-9b66-4b2ecc019d7d";

        public static async Task Startntegration()
        {
            if (string.IsNullOrEmpty(IntegrationService.tokenBradesco))
            {
                try
                {
                    var token = BuildToken.BuildTokenV2(GetTokenBradesco);
                    var resp = RestApiV2.PostToken(IntegrationService.GetTokenBradesco, token.Token);
                    IntegrationService.tokenBradesco = resp.access_token;
                    Console.WriteLine("Resp Token Bradesco");
                    Console.WriteLine(resp);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Resp Token Bradesco Expecytion: " + ex.Message);
                }
            }

            new Task(async () =>
            {
                SericeOracle();
            }).Start();
        }

        public static async Task PostRegistrarBoleto(BradescoBoleto boletoModel)
        {
            try
            {
                string jsonString = JsonConvert.SerializeObject(boletoModel);
                var resp = await RestApi.PostAsync(IntegrationService.RegistrarBoleto, jsonString, IntegrationService.tokenBradesco);
                IntegrationService.tokenBradesco = resp.Content.ToString();
                Console.WriteLine("Resp Resistrar Boleto Bradesco");
                Console.WriteLine(resp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Resp Token Bradesco Expecytion: " + ex.Message);
            }
        }

        public static void SericeOracle()
        {
            //STARTING ORACLE
            OracleDB oracleDB = new OracleDB();
            var empresa = MySqlServices.GetEmpresa(1);
            var listTitulos = OracleDB.GetFINTitulo(new Infra.Entidades.Empresa()
            {
                Nome = empresa.Nome,
                Revenda = empresa.Revenda,
                Banco = empresa.Banco,
                Origem = empresa.Origem,
                Departamento = empresa.Departamento
            });
            //var listTitulos = OracleDB.GetFINTitulo(null);
            foreach (var boleto in listTitulos)
            {
                try
                {
                    var resp = JsonConvert.DeserializeObject<RespBradesco>(RestApiV2.PostBradesco(IntegrationService.tokenBradesco, boleto.ToModel()));
                    if(resp.codigo.Equals("0")) //REMESSA GERADA
                    {
                       // MySqlServices.UpdateRetorno()
                        OracleDB.UpdateTitulo(1, boleto.nuTitulo); //NOSSO NUMERO
                    }
                    else {
                        // MySqlServices.UpdateRetorno()
                        OracleDB.UpdateTitulo(4, boleto.nuTitulo); // Remessa Rejeitada
                    }
                }
                catch (Exception ex)
                {
                    OracleDB.UpdateTitulo(4, boleto.nuTitulo);
                    Console.WriteLine("ERROR POST BRADESCO:" + ex.Message);
                    throw;
                }
            }
        }
    }
}

//RETORNO TABELA SERVOPA
//0 - Sem Remessa
//1 - Remessa Gerada
//2 - Baixado / Retorno
//3 - Agendado / Retorno
//4 - Rejeitado / Retorno
//5 - DDA
//6 - Carteira