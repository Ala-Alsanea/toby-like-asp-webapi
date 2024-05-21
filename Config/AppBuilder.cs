using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Topt_like_asp_webapi.Domain.Commands;
using Topt_like_asp_webapi.Domain.DBContexts;
using Topt_like_asp_webapi.Domain.Entities;
using Topt_like_asp_webapi.Domain.Entities.Base;
using Topt_like_asp_webapi.Domain.Repositories;
using Topt_like_asp_webapi.Domain.Repositories.Interfaces;

namespace Topt_like_asp_webapi.Config
{
    public class AppBuilder
    {
        public static WebApplicationBuilder Boot(WebApplicationBuilder builder)
        {
            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            // builder.Services.AddDbContext<DBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnectionString")));
            builder.Services.AddDbContext<DBContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnectionString")));

            builder.Services.AddTransient<Seeder>();
            builder.Services.AddTransient<Truncate>();
            
            builder.Services.AddTransient<IRepository<User>,Repository<User>>();
            builder.Services.AddTransient<IRepository<Space>,Repository<Space>>();
            builder.Services.AddTransient<IRepository<Collection>,Repository<Collection>>();
            builder.Services.AddTransient<IRepository<Tab>,Repository<Tab>>();


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            return builder;
        }
    }
}