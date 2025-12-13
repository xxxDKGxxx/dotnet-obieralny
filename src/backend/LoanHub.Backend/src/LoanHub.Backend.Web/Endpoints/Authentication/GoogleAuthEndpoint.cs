using LoanHub.Backend.Core.Interfaces;
using LoanHub.Backend.Core.UserAggregate;
using LoanHub.Backend.Core.UserAggregate.Specifications;
using LoanHub.Backend.Web.Endpoints.Shared;

namespace LoanHub.Backend.Web.Endpoints.Authentication;

public sealed class GoogleAuthEndpoint(
	IGoogleTokenValidator googleTokenValidator,
	IJwtTokenService jwtTokenService,
	IRepositoryBase<User> userRepository,  // Use fully qualified name
	ILogger<GoogleAuthEndpoint> logger) : Endpoint<GoogleAuthRequest, GoogleAuthResponse>
{
	public override void Configure()
	{
		Post("/api/auth/google");
		AllowAnonymous();
		Summary(s =>
		{
			s.Summary = "Authenticate with Google";
			s.Description = "Validates Google token and returns JWT access token";
		});
	}

	public override async Task HandleAsync(GoogleAuthRequest request, CancellationToken ct)
	{
		var validationResult = await googleTokenValidator.ValidateTokenAsync(request.Token);

		if (!validationResult.IsValid)
		{
			logger.LogWarning("Google token validation failed: {Error}", validationResult.ErrorMessage);
			await SendUnauthorizedAsync(ct);
			return;
		}

		if (string.IsNullOrEmpty(validationResult.Email))
		{
			logger.LogWarning("Google token validation returned no email");
			await SendUnauthorizedAsync(ct);
			return;
		}

		var firstName = string.IsNullOrWhiteSpace(validationResult.FirstName) ? "User" : validationResult.FirstName.Trim();
		var lastName = string.IsNullOrWhiteSpace(validationResult.LastName) ? null : validationResult.LastName.Trim();

		logger.LogInformation("Processing user: Email={Email}, FirstName={FirstName}, LastName={LastName}",
			validationResult.Email, firstName, lastName);

		var existingUser = await userRepository.FirstOrDefaultAsync(
			new UserByEmailSpec(validationResult.Email), ct);

		User user;
		if (existingUser is null)
		{
			user = new User(validationResult.Email, firstName, lastName, UserRole.User);

			await userRepository.AddAsync(user, ct);
			await userRepository.SaveChangesAsync(ct);

			logger.LogInformation("Created new user: {Email}", validationResult.Email);
		}
		else
		{
			user = existingUser;
		}

		var accessToken = jwtTokenService.GenerateToken(user);

		var response = new GoogleAuthResponse
		{
			AccessToken = accessToken,
			User = new UserDto
			{
				Id = user.Id,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				Role = user.Role.Value
			}
		};

		await SendOkAsync(response, ct);
	}
}