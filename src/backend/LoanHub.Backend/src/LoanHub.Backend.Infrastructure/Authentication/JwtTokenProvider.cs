namespace LoanHub.Backend.Infrastructure.Authentication;

public sealed class JwtTokenProvider(
	IConfiguration configuration,
	ILogger<JwtTokenProvider> logger) : ITokenProvider
{
	private const int TokenExpirationHours = 1;

	private readonly string _secretKey = configuration["Authentication:Jwt:SecretKey"]
		?? throw new InvalidOperationException("JWT SecretKey not configured");
	private readonly string _issuer = configuration["Authentication:Jwt:Issuer"]
		?? throw new InvalidOperationException("JWT Issuer not configured");
	private readonly string _audience = configuration["Authentication:Jwt:Audience"]
		?? throw new InvalidOperationException("JWT Audience not configured");

	public string GenerateToken(User user)
	{
		Guard.Against.Null(user, nameof(user));

		var now = DateTime.UtcNow;
		var issuedAt = new DateTimeOffset(now).ToUnixTimeSeconds().ToString();
		var jti = Guid.NewGuid().ToString();

		var claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.Email, user.Email),
			new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
			new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName ?? string.Empty),
			new Claim(JwtRegisteredClaimNames.Jti, jti),
			new Claim(JwtRegisteredClaimNames.Iat, issuedAt, ClaimValueTypes.Integer64),
			new Claim("role", user.Role.Value)
		};

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
		var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _issuer,
			audience: _audience,
			claims: claims,
			expires: now.AddHours(TokenExpirationHours),
			signingCredentials: credentials);

		var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

		logger.LogInformation("Generated JWT token for user: {UserId}", user.Id);

		return tokenString;
	}

	public Task<bool> ValidateTokenAsync(string token)
	{
		try
		{
			Guard.Against.NullOrEmpty(token, nameof(token));

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

			var validationParameters = new TokenValidationParameters
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = key,
				ValidateIssuer = true,
				ValidIssuer = _issuer,
				ValidateAudience = true,
				ValidAudience = _audience,
				ValidateLifetime = true,
				ClockSkew = TimeSpan.FromMinutes(5),
				RequireExpirationTime = true
			};

			var principal = tokenHandler.ValidateToken(token, validationParameters, out _);

			logger.LogDebug("Successfully validated JWT token");
			return Task.FromResult(true);
		}
		catch (SecurityTokenExpiredException)
		{
			logger.LogWarning("JWT token has expired");
			return Task.FromResult(false);
		}
		catch (SecurityTokenInvalidSignatureException)
		{
			logger.LogWarning("JWT token has invalid signature");
			return Task.FromResult(false);
		}
		catch (SecurityTokenValidationException ex)
		{
			logger.LogWarning("JWT token validation failed: {Error}", ex.Message);
			return Task.FromResult(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Unexpected error during JWT token validation");
			return Task.FromResult(false);
		}
	}
}