using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Topy_like_asp_webapi.Config.Options
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private const string ConfigurationSectionName = "Jwt";
        private readonly IConfiguration _configuration;


        public JwtOptionsSetup(IConfiguration configuration) => _configuration = configuration;

        public void Configure(JwtOptions options) => _configuration.GetSection(ConfigurationSectionName).Bind(options);

    }
}