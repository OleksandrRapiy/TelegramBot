using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramBotApp.Configurationss;
using TelegramBotApp.Data;
using TelegramBotApp.Repositories;
using TelegramBotApp.Repositories.BaseRepositories;
using TelegramBotApp.Services;

namespace TelegramBotApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDbContext<TelegramBotContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("LocalDatabase"));
            });
            
            services.AddSingleton<IBotService, BotService>();

            services.AddControllers().AddNewtonsoftJson();

            services.AddScoped<IBaseRepository<MessageReceiver>, MessageReceiverRepository>();
            services.AddScoped<IBaseRepository<Message>, MessageRepository>();

            services.Configure<BotConfiguration>(Configuration.GetSection("BotConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
        }
    }
}
