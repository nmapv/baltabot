using BaltaBot.Domain.Handlers;
using BaltaBot.Domain.Infra.Context;
using BaltaBot.Domain.Infra.Repositories;
using BaltaBot.Domain.Repositories;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace BaltaBot.Domain.Api
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            using (var services = ConfigureServices())
            {
                using (var scope = services.CreateScope())
                {
                    UpdateDatabase(scope.ServiceProvider);
                }

                var client = services.GetRequiredService<DiscordSocketClient>();

                client.Log += LogAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;
                //await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN_DISCORD"));
                await client.LoginAsync(TokenType.Bot, "MTA0ODIwOTg4MDg1NjY3MDIyMA.GQ9HyW._4IlSEPPB9gqS6nPhGNa5Qg_vrNAczoIW_FgxQ");
                await client.StartAsync();

                await services.GetRequiredService<ConfigDiscord>().InitializeAsync();

                await Task.Delay(Timeout.Infinite);
            }
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices() => new ServiceCollection()
                .AddSingleton(new DiscordSocketConfig
                {
                    GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
                })
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<ConfigDiscord>()
                .AddSingleton<HttpClient>()
                .AddSingleton<DataContext>()
                .AddTransient<IPremiumApiRepository, PremiumApiRepository>()
                .AddTransient<IPersonRepository, PersonRepository>()
                .AddTransient<IPremiumRepository, PremiumRepository>()
                .AddTransient<PersonHandler, PersonHandler>()
                .AddTransient<PremiumHandler, PremiumHandler>()
                .AddFluentMigratorCore()
                .ConfigureRunner(c => c
                    .AddSQLite()
                    .WithGlobalConnectionString("DataSource=file::memory:?cache=shared")
                    .ScanIn(AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("BaltaBot.Domain.Infra")).ToArray()).For.Migrations())
                .AddLogging(c => c.AddFluentMigratorConsole())
                .BuildServiceProvider();

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
