using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Topy_like_asp_webapi.Api.Dtos
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public T? Data { get; set; }
    }
}