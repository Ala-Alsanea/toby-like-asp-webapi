using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Topy_like_asp_webapi.Domain.DBContexts;

namespace Topy_like_asp_webapi.Domain.Cli
{
    public class Truncate(DBContext context)
    {
        public void TruncateDatabase()
        {
            context.Database.EnsureDeleted();
        }
    }
}