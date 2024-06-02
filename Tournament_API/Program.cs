
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

using Tournament_API.Extensions;
using Tournament_Core.Repositories;
using Tournament_Data.Data;
using Tournament_Data.Data.Repositories;

namespace Tournament_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<TournamentApiContext>(options =>
            options.UseSqlServer(builder.Configuration
            .GetConnectionString("TournamentApiContext") ?? throw new InvalidOperationException("Connection string not found.")));

            // +
            //var connectionString = builder.Configuration.GetConnectionString("TournamentApiContext");
            //builder.Services.AddDbContext<TournamentApiContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<TournamentApiContext>();


            builder.Services.AddScoped<IUoW, UoW>();
            builder.Services.AddScoped<ITournamentRepository, TournamentRepository>();
            builder.Services.AddScoped<IGameRepository, GameRepository>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddControllers(
                opt =>
                {
                    opt.ReturnHttpNotAcceptable = true;
                    //opt.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
                })
            .AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.SeedDataAsync();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            // +
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
