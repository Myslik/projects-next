﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NArchitecture;
using Projects.DataAccess;
using Projects.DataAccess.Account;
using Projects.WebNext.Extensions;
using Projects.WebNext.Security;

namespace Projects.WebNext
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add database context.
            services.AddDbContext<ProjectsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add session.
            services.AddDistributedMemoryCache();
            services.AddSession();

            // Add framework services.
            services.AddMvc();

            services.AddScoped<PrincipalFactory>();

            // Add bus and handlers.
            services.AddServiceBus(configure =>
            {
                configure.Requests.AddRequestHandler<UserQueryHandler>();
                configure.Requests.AddRequestHandler<GetUserCommandHandler>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationScheme = "ProjectsCookie",
                LoginPath = new PathString("/login"),
                AccessDeniedPath = new PathString("/error"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                SessionStore = new MemoryCacheTicketStore()
            });

            app.UseMvc();
        }
    }
}
