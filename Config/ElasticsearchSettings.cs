using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topy_like_asp_webapi.Config
{

// Elasticsearch": {
//     "Nodes": [
//       "https://localhost:9200"
//     ],
//     "CertificateFingerprint": "<FINGERPRINT>",

//      "ApiKey": "<API_KEY>"
//              or
//     "Username": "<USERNAME>",
//     "Password": "<PASSWORD>"

//   },

    public class ElasticsearchSettings
{
    public List<string> Nodes { get; set; }
    public string CertificateFingerprint { get; set; }
    public string ApiKey { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }
}
}