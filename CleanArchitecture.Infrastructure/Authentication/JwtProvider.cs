﻿using CleanArchitecture.Application.Abstractions;
using CleanArchitecture.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Authentication
{
    public sealed class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(JwtOptions jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public string CreateToken(User user)
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Name,user.UserName),
                new Claim("NameLastName",user.NameLastName)
            };

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer:_jwtOptions.Issuer,
                audience:_jwtOptions.Audience,
                claims: claims,
                notBefore:DateTime.Now,
                expires:DateTime.Now.AddHours(1),
                signingCredentials:new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),SecurityAlgorithms.HmacSha256));  

            string token=new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return token;

        }
    }
}