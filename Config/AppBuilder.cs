using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Topy_like_asp_webapi.Domain.Cli;
using Topy_like_asp_webapi.Domain.DBContexts;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Domain.Mappers;
using Topy_like_asp_webapi.Infrastructure.Repositories;
using Topy_like_asp_webapi.Infrastructure.Repositories.Interfaces;


namespace Topy_like_asp_webapi.Config
{
    public class AppBuilder
    {
        public static WebApplicationBuilder Boot(WebApplicationBuilder builder)
        {

            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            // R-Database registration
            // builder.Services.AddDbContext<DBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString")));
            builder.Services.AddDbContext<DBContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionString")));

            // Elasticsearch  registration
            builder.Services.Configure<ElasticsearchSettings>(builder.Configuration.GetSection("Elasticsearch"));
            builder.Services.AddSingleton<ElasticsearchClient>(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<ElasticsearchSettings>>().Value;
                var nodes = settings.Nodes.Select(n => new Uri(n)).ToArray();
                var pool = new StaticNodePool(nodes);
                var clientSettings = new ElasticsearchClientSettings(pool)
                // .CertificateFingerprint(settings.CertificateFingerprint)
                // .Authentication(new ApiKey(settings.ApiKey))
                // .Authentication(new BasicAuthentication(settings.Username, settings.Password));

                ;
                return new ElasticsearchClient(clientSettings);
            });

            // configure midator
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));


            // configure mapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // repository registration
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped(typeof(IElasticsearchRepository<>), typeof(ElasticsearchRepository<>));
            // CLI commands registration
            builder.Services.AddTransient<Seeder>();
            builder.Services.AddTransient<ElasticsearchSeedr>();
            builder.Services.AddTransient<Truncate>();

            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.);
            builder.Services.AddSwaggerGen();
            builder.Services.AddLogging();

            return builder;
        }
    }
}