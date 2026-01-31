namespace LoanHub.Aggregator.Web.Configurations;

public static class AuthConfig
{
	public static IServiceCollection AddAuthConfig(
		this IServiceCollection services,
		IConfiguration configuration,
		ILogger logger)
	{

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

		services.AddAuthorizationBuilder()
			.AddDefaultPolicy("DefaultPolicy", policy =>
			{
				policy.RequireAuthenticatedUser();
			})
			.AddPolicy("UserPolicy", policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireClaim(ClaimTypes.Role, "User");
			})
			.AddPolicy("AdminPolicy", policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireClaim(ClaimTypes.Role, "Admin");
			})
			.AddPolicy("EmployeePolicy", policy =>
			{
				policy.RequireAuthenticatedUser();
				policy.RequireClaim(ClaimTypes.Role, "Employee");
			});

		logger.LogInformation("Authentication services registered");

		return services;
	}
}