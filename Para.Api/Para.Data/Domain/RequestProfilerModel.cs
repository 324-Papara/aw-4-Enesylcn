using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Para.Data.Domain
{
    public class RequestProfilerModel
    {
        public DateTimeOffset RequestTime { get; set; }
        public DateTimeOffset ResponseTime { get; set; }
        public HttpContext Context { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
    }
}