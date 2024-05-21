using Topy_like_asp_webapi.Config;

var builder = WebApplication.CreateBuilder(args);


var appBuilder = AppBuilder.Boot(builder);

var app = appBuilder.Build();

CliRegistry.Boot(app, args);

var server = GlobalMiddleware.Boot(app);

server.Run();
