using CQRS.Gate;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace CQRS.Decorator
{
    public class NLogCommandDecorator : IGate
    {
        private readonly IGate _commandGateway;
        private readonly ILogger _logger;

        public NLogCommandDecorator(IGate commandGateway, ILoggerFactory loggerFactory)
        {
            _commandGateway = commandGateway;
            _logger = loggerFactory.CreateLogger<NLogCommandDecorator>();
        }

        public void Call<TCommand>(TCommand command)
        {
            using (_logger.BeginScope(command))
                try
                {
                    _logger.LogInformation($"Invoking command {typeof(TCommand)} with arguments {JsonConvert.SerializeObject(command)}");
                    _commandGateway.Call(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    throw;
                }
        }

        public TCommandResult Call<TCommand, TCommandResult>(TCommand command)
        {
            using (_logger.BeginScope(command))
                try
                {
                    _logger.LogInformation($"Invoking command {typeof(TCommand)} with arguments {JsonConvert.SerializeObject(command)}");
                    return _commandGateway.Call<TCommand, TCommandResult>(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    throw;
                }
        }
    }
}
