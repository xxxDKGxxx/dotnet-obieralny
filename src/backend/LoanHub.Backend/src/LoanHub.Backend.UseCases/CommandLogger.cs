using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints;
using Microsoft.Extensions.Logging;

namespace LoanHub.Backend.UseCases;
public sealed class CommandLogger<TCommand, TResult>(ILogger<TCommand> logger)
: ICommandMiddleware<TCommand, TResult> where TCommand : FastEndpoints.ICommand<TResult>
{
    private readonly ILogger<TCommand> _logger = logger;

    public async Task<TResult> ExecuteAsync(TCommand command,
                                            CommandDelegate<TResult> next,
                                            CancellationToken ct)
    {
        string commandName = command.GetType().Name;
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Handling {RequestName}", commandName);

            // Reflection! Could be a performance concern
            Type myType = command.GetType();
            IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo prop in props)
            {
                object? propValue = prop?.GetValue(command, null);
                _logger.LogInformation("Property {Property} : {@Value}", prop?.Name, propValue);
            }
        }

        var sw = Stopwatch.StartNew();

        var result = await next();

        _logger.LogInformation("Handled {CommandName} with {Result} in {ms} ms", commandName, result, sw.ElapsedMilliseconds);
        sw.Stop();

        return result;
    }
}
