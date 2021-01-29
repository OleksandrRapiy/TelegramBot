using BotIBWT;
using BotIBWT.Services;
using IBWT.Framework;
using IBWT.Framework.Abstractions;
using IBWT.Framework.Extentions;
using IBWT.Framework.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using BotIBWT.Data;
using BotIBWT.Handlers;
using BotIBWT.Handlers.Comands;
using BotIBWT.Repositories;
using BotIBWT.Repositories.BaseRepositories;

namespace BotIBWT
{
    public class Startup
    {

        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDbContext<BotIBWTContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("LocalDatabase")
            ));

            services.Configure<BotOptions>(Configuration.GetSection("BotOptionsTest"));

            services.AddControllers().AddNewtonsoftJson();

            services.AddScoped<IBaseRepository<Message>, MessageRepository>();
            services.AddScoped<IBaseRepository<MessageReceiver>, MessageReceiverRepository>();

            services.AddScoped<IMessageService, MessageService>();

            services.AddTelegramBot()
                    .AddScoped<StartCommand>()
                    .AddScoped<ButtonCommand>()
                    .AddScoped<CallbackHandler>()
                    .AddScoped<TextHandler>()
                    .AddScoped<EmojiHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(options => options.AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseTelegramBotLongPolling(ConfigureBot(), startAfter: TimeSpan.FromSeconds(2));
        }

        private IBotBuilder ConfigureBot()
        {
            return new BotBuilder()
                       .MapWhen(When.NewMessage, comand =>
                            comand
                            .UseWhen(When.NewCommand, command => comand.UseCommand<StartCommand>("start"))
                            .UseWhen(When.NewCommand, command => comand.UseCommand<ButtonCommand>("button"))
                            .UseWhen(When.NewTextMessage, text => text.Use<TextHandler>())
                            .UseWhen(When.StickerMessage, config => config.Use<EmojiHandler>())
                       )
                       .UseWhen(When.CallbackQuery, callback => callback.Use<CallbackHandler>());
        }
    }
}
