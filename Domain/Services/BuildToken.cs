using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Domain.Models;

namespace Domain.Services
{
    public static class BuildToken
    {
        public static UserToken BuildTokenV1(string GetTokenBradesco)
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

        public static UserToken BuildTokenV2(string GetTokenBradesco)
        {
            try
            {
                string privateKeyPem = File.ReadAllText("C:\\temp\\Servopa.key.pem");

                privateKeyPem = privateKeyPem.Replace("-----BEGIN PRIVATE KEY-----", "");
                privateKeyPem = privateKeyPem.Replace("-----END PRIVATE KEY-----", "");

                byte[] privateKeyRaw = Convert.FromBase64String(privateKeyPem);

                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.ImportPkcs8PrivateKey(new ReadOnlySpan<byte>(privateKeyRaw), out _);
                RsaSecurityKey rsaSecurityKey = new RsaSecurityKey(provider);

                var now = DateTime.UtcNow;

                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Aud, GetTokenBradesco),
                    new Claim(JwtRegisteredClaimNames.Sub, "a666ae3f-0e0d-426c-bdc1-72992123016f"),
                    new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp, new DateTime().AddMonths(2).Second.ToString("yyyyMMddHHmmssffff")),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("ver", "1.1")
                };

                var handler = new JwtSecurityTokenHandler();

                var expiration = now.AddMinutes(60);

                var token = new JwtSecurityToken(
                           issuer: null,
                           audience: null,
                           claims: claims,
                           expires: expiration,
                           signingCredentials: new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256)
                );

                return new UserToken()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = expiration
                };
            }

            catch (Exception e)
            {
                return new UserToken();
            }

        }
    }
}
