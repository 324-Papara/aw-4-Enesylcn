using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Para.Data.Domain
{
    public class RequestProfilerModel
    {
        DateTimeOffset RequestTime { get; set; }
        DateTimeOffset ResponseTime { get; set; }
        HttpContext? Context { get; set; }
        string? Request { get; set; }
        string? Response { get; set; }
    }
}