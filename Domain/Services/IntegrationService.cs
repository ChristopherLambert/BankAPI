using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Domain.Models;
using System.Security.Claims;

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

        public static UserToken BuildToken()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Aud, GetTokenBradesco),
                new Claim(JwtRegisteredClaimNames.Sub, "e55e6ce8-c55d-4bb0-b546-c19ec90a3f11"),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.Second.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddMonths(1).Second.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("ver", "1.1")
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("BankAPIBoletoBradescoServopa"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            // tempo de expiração do token: 1 hora
            var expiration = DateTime.UtcNow.AddHours(1);
            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
