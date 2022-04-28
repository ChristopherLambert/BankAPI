using System;
using System.Threading.Tasks;

namespace Domain.Services
{
    public static class IntegrationService
    { 
        //URL Para pegar o token
        private static readonly string GetTokenBradesco = "https://proxy.api.prebanco.com.br/auth/server/v1.1/token";
        private static string tokenBradesco = "";
        private static string clientKey = "e55e6ce8-c55d-4bb0-b546-c19ec90a3f11";
        private static string clientKSecret = "b7beb315-2067-4084-9b66-4b2ecc019d7d";

        public static async Task Startntegration()
        {
            if (string.IsNullOrEmpty(IntegrationService.tokenBradesco))
            {
                try
                {
                    var resp = await RestApi.PostAsync(IntegrationService.GetTokenBradesco);
                    IntegrationService.tokenBradesco = resp.Content.ToString();
                    Console.WriteLine("Resp Token Bradesco");
                    Console.WriteLine(resp);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Resp Token Bradesco Expecytion: " + ex.Message);
                }
            }
        }
    }
}
