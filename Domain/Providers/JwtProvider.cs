using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Topy_like_asp_webapi.Config.Options;
using Topy_like_asp_webapi.Domain.Entities;

namespace Topy_like_asp_webapi.Domain.Providers
{
    public sealed class JwtProvider
    {
        private readonly JwtOptions jwtOptions;

        public JwtProvider(IOptions<JwtOptions> jwtOptions)
        {
            this.jwtOptions = jwtOptions.Value;
        }

        public string Genrate(User user)
        {
            Claim[] clamims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(nameof(user.GoogleId) ,user.GoogleId.ToString())
             };

            SigningCredentials signingCred = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                jwtOptions.Issuer,
                jwtOptions.Audience,
                clamims,
                null,
                DateTime.Now.AddMinutes(30),
                signingCred
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }
}