using MovieMania.Repositories;
using MovieMania.Repositories.Interfaces;
using MovieMania.Services;
using MovieMania.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MovieMania.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
namespace MovieMania
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
            services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.WithOrigins("https://cinemacraze.netlify.app")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

            // services.AddCors(options =>
            // {
            //     options.AddPolicy("AllowAnyOrigin",
            //         builder =>
            //         {
            //             builder.AllowAnyOrigin()
            //                    .AllowAnyHeader()
            //                    .AllowAnyMethod();
            //         });
            // });
            // entity for user authentication
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(Configuration["ConnectionString:IMDBDatabaseConnectionString"]));

            services.AddIdentity<ApplicationUser,IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            // adding Jwt bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            // other services
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieMania", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionString"));
            // adding custom services
            services.TryAddScoped<IActorRepository,ActorRepository> ();
            services.TryAddScoped<IGenreRepository, GenreRepository>();
            services.TryAddScoped<IMovieRepository, MovieRepository>();
            services.TryAddScoped<IProducerRepository, ProducerRepository>();
            services.TryAddScoped<IReviewRepository, ReviewRepository>();

            services.TryAddScoped<IActorService, ActorService>();
            services.TryAddScoped<IGenreService, GenreService>();
            services.TryAddScoped<IMovieService, MovieService>();
            services.TryAddScoped<IProducerService, ProducerService>();
            services.TryAddScoped<IReviewService, ReviewService>();

            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieMania v1"));
            //}

            app.UseHttpsRedirection();

            app.UseRouting();
            // CORS must be between Routing and Authentication
    app.UseCors("CorsPolicy");
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
