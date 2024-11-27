﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TrisGPOI.Core.JWT.Interfaces;

namespace TrisGPOI.Core.JWT
{
    public class JWTManager : IJWTManager
    {
        private readonly IGetOptionManager _getOptionManager;
        public JWTManager(IGetOptionManager getOptionManager)
        {
            _getOptionManager = getOptionManager;
        }
        public string JWTGenerate(string? data)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentNullException("data");
            }
#pragma warning disable 8602, 8604
            //legge la configurazione di TokenOptions
            var tokenOptions = _getOptionManager.GetTokenOptions();
            //prende sicret
            var key = Encoding.ASCII.GetBytes(tokenOptions.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //configurazione del JWT:
                //utente
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, data)
                }),
                //Issuer: colui che ha creato il token
                Issuer = tokenOptions.Issuer,
                //Audience: chi utilizzera questo token, cioè quali sono server e API 
                Audience = tokenOptions.Audience,
                //scadenza
                Expires = DateTime.UtcNow.AddDays(tokenOptions.ExpiryDays),
                //algoritmo di generazione della firma, serve per controllare che la token hai creato tu
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //seguente codice è per creare token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

#pragma warning restore 8602, 8604
            return tokenHandler.WriteToken(token);
        }
        
    }
}
