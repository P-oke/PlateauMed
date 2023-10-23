using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlateauMedTask.Application.Interfaces;
using PlateauMedTask.Domain.Entities;
using PlateauMedTask.Infrastructure.Context;
using PlateauMedTask.Infrastructure.Implementations;

namespace PlateauMedTask.API
{
    /// <summary>
    /// SERVICE EXTENSION CLASS
    /// </summary>
    public static class ServiceExtension
    {
        /// <summary>
        /// CONFIGURE IDENTITY
        /// </summary>
        /// <param name="services">the services</param>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Password.RequiredUniqueChars = 1;

            }).AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(24);
            });
        }

        /// <summary>
        /// ADD ENTITY FRAMEWORK DATABASE CONTEXT
        /// </summary>
        /// <param name="services">the services</param>
        /// <param name="configuration">the configuration</param>
        public static void AddEntityFrameworkDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string dbConnectionString = configuration.GetConnectionString("Default");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    dbConnectionString,
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }

        /// REGISTER SERVICE
        /// </summary>
        /// <param name="services">the services</param>
        /// <param name="iConfiguration">the configuration</param>
        public static void RegisterService(this IServiceCollection services, IConfiguration iConfiguration)
        {
            services.AddTransient<DbContext, ApplicationDbContext>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IStudentService, StudentService>();

        }

    }
}
