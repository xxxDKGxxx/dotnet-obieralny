using LoanHub.Backend.Core.Interfaces;
using LoanHub.Backend.Infrastructure;
using LoanHub.Backend.Infrastructure.Data;
using LoanHub.Backend.Infrastructure.Email;
using Microsoft.EntityFrameworkCore;

namespace LoanHub.Backend.Web.Configurations;

public static class ServiceConfigs
{
    public static IServiceCollection AddServiceConfigs(this IServiceCollection services, Microsoft.Extensions.Logging.ILogger logger, WebApplicationBuilder builder)
    {
        services.AddInfrastructureServices(builder.Configuration, logger)
            .AddMediatrConfigs();

        if (builder.Environment.IsDevelopment())
        {
            // Use a local test email server

            // Otherwise use this:
            builder.Services.AddScoped<IEmailSender, FakeEmailSender>();
        }
        else
        {
            services.AddScoped<IEmailSender, FakeEmailSender>();
        }

        logger.LogInformation("{Project} services registered", "Mediatr and Email Sender");

        return services;
    }
}
