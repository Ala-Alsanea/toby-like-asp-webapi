using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AutoMapper;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Topy_like_asp_webapi.Config.Options;
using Topy_like_asp_webapi.Domain.Cli;
using Topy_like_asp_webapi.Domain.DBContexts;
using Topy_like_asp_webapi.Domain.Entities;
using Topy_like_asp_webapi.Domain.Mappers;
using Topy_like_asp_webapi.Domain.Providers;
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


            // var jwtConfig = builder.Configuration.GetSection("JWT");
            // builder.Services.Configure<JwtOptions>(jwtConfig);

            // builder.Services.AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtBearerOptionsSetup>();

            // builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //     .AddJwtBearer();

            // builder.Services.ConfigureOptions<JwtOptionsSetup>();
            // builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

               builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(o =>
                {
                    var jwtConfig = builder.Configuration.GetSection("JWT");
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtConfig["Issuer"],
                        ValidAudience = jwtConfig["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["SecretKey"])),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true
                    };
                });


            builder.Services.AddSingleton<JwtProvider>();


            builder.Services.AddEndpointsApiExplorer();
            // builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            // builder.Services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.);
            builder.Services.AddLogging();
            // builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                        {
                            new OpenApiSecurityScheme
                            {
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }
                });
            });

            return builder;
        }
    }
}