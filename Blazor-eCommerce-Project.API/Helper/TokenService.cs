using Blazor_eCommerce_Project.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Blazor_eCommerce_Project.API.Helper
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _symmetricSecurityKey;

        public TokenService(IConfiguration config)
        {
            _config = config;
            _symmetricSecurityKey =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
        }

        public string CreateToken(IdentityUser user)
        {
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Id",user.Id)
            };

            var role = ResultConstant.Role_Admin;
            claim.Add(new Claim(ClaimTypes.Role,role));

            var cred = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new JwtSecurityToken
            (
                issuer:_config["Token:Issuer"],
                audience: _config["Token:Audience"],
                claims: claim,
                expires:DateTime.Now.AddDays(10),
                signingCredentials:cred
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            return token;

        }
    }
}
