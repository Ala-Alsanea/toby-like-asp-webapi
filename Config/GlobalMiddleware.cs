using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topy_like_asp_webapi.Config
{
    public class GlobalMiddleware
    {
        public static WebApplication Boot(WebApplication app)
        {


            // initialize CORS 
            app.UseCors("_myAllowSpecificOrigins");

            // initialize routes

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();

            app.UseAuthorization();
            
            app.UseHttpsRedirection();
            
            app.MapControllers();

            return app;
        }
    }
}