using lab12a.Data;
using lab12a.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using lab12a.Models.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using lab12a.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace lab12a
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);


            string conn = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddDbContext<AsyncInnContext>
            (options => options.UseSqlServer(conn));
            builder.Services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<AsyncInnContext>();

            builder.Services.AddScoped<JwtTokenService>();
            builder.Services.AddTransient<IUser, IdentityUserService>();
            builder.Services.AddTransient<IHotel, HotelService>();
            builder.Services.AddTransient<IRoom, RoomService>();
            builder.Services.AddTransient<IAmenities, AmenitiesService>();
            builder.Services.AddTransient<IHotelRoom, HotelRoomService>();
          
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = JwtTokenService.GetValidationParameters(builder.Configuration);
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "AsyncInn ApI",
                    Version = "v1",
                });
            });

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
     );
            var app = builder.Build();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "AsyncInn API");
                options.RoutePrefix = "docs";
            });
            app.MapControllers();
            app.MapGet("/", () => "Hello World!");

            app.Run();
        }
    }
}