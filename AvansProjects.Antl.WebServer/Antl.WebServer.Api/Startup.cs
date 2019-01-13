using System;
using System.Text;
using System.Threading.Tasks;
using Antl.WebServer.Api.AuthorizationHandlers;
using Antl.WebServer.Api.Controllers;
using Antl.WebServer.Dtos;
using Antl.WebServer.Entities;
using Antl.WebServer.Infrastructure;
using Antl.WebServer.Interfaces;
using Antl.WebServer.Repositories;
using Antl.WebServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;

namespace Antl.WebServer.Api
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
            services.AddDbContext<AntlContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<AntlContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44362.api",
                    ValidAudience = "https://localhost:44362.api",

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                    policy.Requirements.Add(new UserRole("Admin")));
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToAccessDenied = context =>
                    {
                        if (context.Request.Path.StartsWithSegments("/api"))
                        {
                            context.Response.StatusCode = 403;
                            return Task.FromResult(0);
                        }

                        context.Response.Redirect(context.RedirectUri);
                        return Task.FromResult(0);

                    },
                    OnRedirectToLogin = context =>
                    {
                        if (context.Request.Path.StartsWithSegments("/api"))
                        {
                            context.Response.StatusCode = 401;
                            return Task.FromResult(0);
                        }

                        context.Response.Redirect(context.RedirectUri);
                        return Task.FromResult(0);
                    }
                };

                options.Cookie.HttpOnly = true;
            });

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Antl API", Version = "v1" });
            });

            // Add Dependencies for injection
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton<ILoggerFactory, SerilogLoggerFactory>();

            // Generic Controllers Dependency injection
            services.AddScoped(typeof(IGenericBaseControllerAsync<EventDto>), typeof(GenericBaseControllerAsync<EventDto, Event>));
            services.AddScoped(typeof(IGenericBaseControllerAsync<UserDto>), typeof(GenericBaseControllerAsync<UserDto, ApplicationUser>));
            services.AddScoped(typeof(IGenericBaseControllerAsync<FriendshipDto>), typeof(GenericBaseControllerAsync<FriendshipDto, FriendShip>));

            // Generic Services Dependency injection
            services.AddScoped(typeof(IGenericServiceAsync<EventDto, Event>), typeof(GenericServiceAsync<EventDto, Event>));
            services.AddScoped(typeof(IGenericServiceAsync<UserDto, ApplicationUser>), typeof(GenericServiceAsync<UserDto, ApplicationUser>));
            services.AddScoped(typeof(IGenericServiceAsync<FriendshipDto, FriendShip>), typeof(ContactService));
            services.AddScoped(typeof(IAuthenticationHandlerServiceAsync), typeof(AuthenticationHandlerServiceAsyncAsync));

            // Generic Repositories Dependency injection
            services.AddScoped(typeof(IGenericRepository<Event>), typeof(GenericRepository<Event>));
            services.AddScoped(typeof(IGenericRepository<ApplicationUser>), typeof(GenericRepository<ApplicationUser>));
            services.AddScoped(typeof(IGenericRepository<FriendShip>), typeof(GenericRepository<FriendShip>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            AntlContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            AntlSeed.SeedAsync(context, userManager, roleManager).GetAwaiter().GetResult();
            app.UseStatusCodePages();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Antl API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
