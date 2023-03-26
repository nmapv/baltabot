﻿using BaltaBot.Domain.Handlers;
using BaltaBot.Domain.Infra.Context;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
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
                var client = services.GetRequiredService<DiscordSocketClient>();

                client.Log += LogAsync;
                services.GetRequiredService<CommandService>().Log += LogAsync;
                await client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("TOKEN_DISCORD"));
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

        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton(new DiscordSocketConfig
                {
                    GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
                })
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<ConfigDiscord>()
                .AddSingleton<HttpClient>()
                //.AddTransient<DataContext>(s => new DataContext(conn!))
                .AddSingleton<DataContext>()
                .AddTransient<PersonHandler, PersonHandler>()
                .AddTransient<PremiumHandler, PremiumHandler>()
                .BuildServiceProvider();
        }
    }
}
