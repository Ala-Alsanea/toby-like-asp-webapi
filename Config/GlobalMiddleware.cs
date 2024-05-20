using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topt_like_asp_webapi.Config
{
    public class GlobalMiddleware
    {
        public static WebApplication Boot(WebApplication app)
        {


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            return app;
        }
    }
}