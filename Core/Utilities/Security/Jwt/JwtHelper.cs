using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Utilities.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get;  }
        private TokenOption _tokenOption;
        private DateTime _accessTokenExpiration;
        public JwtHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOption = configuration.GetSection("TokenOptions").Get<TokenOption>();
            _accessTokenExpiration=DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
        }

        public AccessToken CreateToken(User user, List<OperationClaim> operationClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOption.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOption, user, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOption tokenOption, User user,
            SigningCredentials signingCredentials
            , List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer:tokenOption.Issuer,
                audience:tokenOption.Audience,
                expires:_accessTokenExpiration,
                notBefore :DateTime.Now,
                claims : SetClaims(user,operationClaims),
                signingCredentials:signingCredentials
                );

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(User user,List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(operationClaims.Select(c=>c.Name).ToArray());

            return claims;
        }
    }
}
