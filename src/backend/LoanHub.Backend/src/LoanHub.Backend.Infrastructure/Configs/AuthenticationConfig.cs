using LoanHub.Backend.Core.Interfaces;
using LoanHub.Backend.Infrastructure.Authentication;

namespace LoanHub.Backend.Infrastructure.Configs;

public static class AuthenticationConfig
{
	public static IServiceCollection AddAuthenticationServices(
		this IServiceCollection services,
		IConfiguration configuration,
		ILogger logger)
	{
		services.AddScoped<IJwtTokenService, JwtTokenService>();
		services.AddScoped<IGoogleTokenValidator, GoogleTokenValidator>();

		var jwtSettings = configuration.GetSection("Authentication:Jwt");
		var secretKey = jwtSettings["SecretKey"]
			?? throw new InvalidOperationException("JWT SecretKey not configured");
		var issuer = jwtSettings["Issuer"]
			?? throw new InvalidOperationException("JWT Issuer not configured");
		var audience = jwtSettings["Audience"]
			?? throw new InvalidOperationException("JWT Audience not configured");

		var validationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
			ValidateIssuer = true,
			ValidIssuer = issuer,
			ValidateAudience = true,
			ValidAudience = audience,
			ValidateLifetime = true,
			ClockSkew = TimeSpan.FromMinutes(5),
			RequireExpirationTime = true
		};

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = validationParameters;

				options.Events = new JwtBearerEvents
				{
					OnAuthenticationFailed = context =>
					{
						logger.LogWarning("JWT Authentication failed: {Error}", context.Exception.Message);
						return Task.CompletedTask;
					},
					OnTokenValidated = context =>
					{
						var userId = context.Principal?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
						var email = context.Principal?.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
						logger.LogDebug("JWT Token validated for user: {UserId} ({Email})", userId, email);
						return Task.CompletedTask;
					}
				};
			});

		services.AddAuthorization(options =>
		{
			options.AddPolicy("RequireUser", policy =>
			{
				policy.RequireClaim(JwtRegisteredClaimNames.Sub);
				policy.RequireClaim(JwtRegisteredClaimNames.Email);
			});

			options.AddPolicy("RequireUserRole", policy =>
			{
				policy.RequireClaim("role", "User");
			});

			options.AddPolicy("RequireAdminRole", policy =>
			{
				policy.RequireClaim("role", "Admin");
			});
		});

		logger.LogInformation("Authentication services registered");

		return services;
	}
}