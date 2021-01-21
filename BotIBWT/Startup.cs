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
using TestIBWT.Data;
using TestIBWT.Handlers;
using TestIBWT.Handlers.Comands;
using TestIBWT.Repositories;
using TestIBWT.Repositories.BaseRepositories;

namespace TestIBWT
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
            /*
             * Questions to ask: 
             * 1. What is delegate - next(IUpdate, CancelationToken)? 
             * 2. How actually work long polling?
             * 
             */

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
