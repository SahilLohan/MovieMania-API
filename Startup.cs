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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieMania", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
            services.Configure<ConnectionString>(Configuration.GetSection("ConnectionString"));
            // adding custom services
            services.TryAddSingleton<IActorRepository,ActorRepository> ();
            services.TryAddSingleton<IGenreRepository, GenreRepository>();
            services.TryAddSingleton<IMovieRepository, MovieRepository>();
            services.TryAddSingleton<IProducerRepository, ProducerRepository>();
            services.TryAddSingleton<IReviewRepository, ReviewRepository>();

            services.TryAddSingleton<IActorService, ActorService>();
            services.TryAddSingleton<IGenreService, GenreService>();
            services.TryAddSingleton<IMovieService, MovieService>();
            services.TryAddSingleton<IProducerService, ProducerService>();
            services.TryAddSingleton<IReviewService, ReviewService>();

            

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
