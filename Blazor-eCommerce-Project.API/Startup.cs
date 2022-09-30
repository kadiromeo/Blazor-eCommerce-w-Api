using Blazor_eCommerce_Project.API.Helper;
using Blazor_eCommerce_Project.Business.Contracts;
using Blazor_eCommerce_Project.Business.Implementaion;
using Blazor_eCommerce_Project.Data.Access.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace Blazor_eCommerce_Project.API
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
            services.AddDbContext<CourseContext>(options =>
            options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddRouting(x=>x.LowercaseUrls=true );
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<CourseContext>().AddDefaultTokenProviders();
            services.AddCors(o => o.AddPolicy("Blazor-eCommerce-Project", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            services.AddAuthentication(opt =>
            {
               opt.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
               opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               opt.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(k => {

                k.RequireHttpsMetadata = false;
                k.SaveToken = true;
                k.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:Key"])),
                    ValidateAudience=true,
                    ValidateIssuer=true,
                    ValidAudience= Configuration["Token:Audience"],
                    ValidIssuer= Configuration["Token:Issuer"],
                    ClockSkew=TimeSpan.Zero


                };
            
            });

            services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Blazor_eCommerce_Project.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor_eCommerce_Project.API v1"));
            }
            app.UseCors("Blazor-eCommerce-Project");

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
