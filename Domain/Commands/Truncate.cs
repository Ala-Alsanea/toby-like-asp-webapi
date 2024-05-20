using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topt_like_asp_webapi.Domain.DBContexts;

namespace Topt_like_asp_webapi.Domain.Commands
{
    public class Truncate(PostgresContext context)
    {
        public void TruncateDatabase()
        {
            context.Database.EnsureDeleted();
        }
    }
}