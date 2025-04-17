using Blog.Application.Features.Users.Commands.CreateUser;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Blog.Worker;

public class Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    private IConnection _connection;
    private IModel _channel;

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker starting at: {time}", DateTimeOffset.Now);
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            ConfigureRabbitMQ();

            // Esperar hasta que el servicio solicite la cancelación
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(6000, stoppingToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing worker service");

            // Reintentar la configuración con un retraso si RabbitMQ no está disponible
            await Task.Delay(5000, stoppingToken);
            if (!stoppingToken.IsCancellationRequested)
            {
                await ExecuteAsync(stoppingToken);
            }
        }
    }

    private void ConfigureRabbitMQ()
    {
        _logger.LogInformation("Configuring RabbitMQ at: {time}", DateTimeOffset.Now);

        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "guest",
            Password = "guest",
            // Reintentos automáticos
            RequestedConnectionTimeout = TimeSpan.FromSeconds(30),
            AutomaticRecoveryEnabled = true,
            NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "user_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        _logger.LogInformation("RabbitMQ connection established");

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation("Received message: {message}", message);

                var userData = JsonSerializer.Deserialize<UserData>(message, options);
                if (userData != null)
                {
                    var createUserCommand = new CreateUserCommand
                    {
                        AuthUserId = userData.UserId,
                        Username = userData.Username,
                        IsActive = userData.IsActive
                    };

                    using var scope = _serviceScopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(createUserCommand);

                    _logger.LogInformation("User created: {userId}", userData.UserId);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
            }
        };

        _channel.BasicConsume(queue: "user_queue", autoAck: true, consumer: consumer);
        _logger.LogInformation("Consumer registered for user_queue");
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker stopping at: {time}", DateTimeOffset.Now);
        return base.StopAsync(cancellationToken);
    }
}
