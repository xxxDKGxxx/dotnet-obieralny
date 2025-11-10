using System.Reflection;
using Ardalis.SharedKernel;

namespace LoanHub.Backend.Web.Configurations;

public static class MediatrConfigs
{
	public static IServiceCollection AddMediatrConfigs(this IServiceCollection services)
	{
		var mediatRAssemblies = new[]
		{
		  Assembly.GetAssembly(typeof(Program))
		};

		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssemblies(mediatRAssemblies!);
		})
			.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>))
			.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

		return services;
	}
}