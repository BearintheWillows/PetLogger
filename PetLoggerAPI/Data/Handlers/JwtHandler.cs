namespace PetLoggerAPI.Data.Handlers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Models;
using static System.Security.Claims.ClaimTypes;

public class JwtHandler {
	private readonly IConfiguration _configuration;
	private readonly UserManager<AppUser> _userManager;

	public JwtHandler(IConfiguration configuration, UserManager<AppUser> userManager) {
		_configuration = configuration;
		_userManager = userManager;
	}

	
	public async Task<JwtSecurityToken> GetTokenAsync(AppUser user) {
		JwtSecurityToken jwtOptions = new(issuer: _configuration[ "Jwt:Issuer" ],
		                                  audience: _configuration[ "Jwt:Audience" ],
		                                  claims: await GetClaimsAsync( user ),
		                                  expires: DateTime.Now.AddMinutes(
			                                  Convert.ToDouble( _configuration[ "Jwt:ExpirationMinutes" ] )
		                                  ),
		                                  signingCredentials: GetSigningCredentials()
		);
		return jwtOptions;
	}
	
	private SigningCredentials GetSigningCredentials() {
		var key = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes( _configuration[ "Jwt:SecretKey" ] )
		);
		var creds = new SigningCredentials(
			key,
			SecurityAlgorithms.HmacSha256
		);
		return creds;
	}

	private async Task<List<Claim>> GetClaimAsync(AppUser user) {
		var claims = new List<Claim> { new Claim( ClaimTypes.Name, user.Email ) };

		foreach ( var role in await _userManager.GetClaimsAsync( user )  ) {
			claims.Add(new Claim( ClaimTypes.Role, role ) );
		}

		return claims;
	}
}