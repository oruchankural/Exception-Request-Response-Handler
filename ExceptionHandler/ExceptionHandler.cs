using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionHandler
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandler> logger;

        public ExceptionHandler(RequestDelegate Next, ILogger<ExceptionHandler> Logger)
        {
            next = Next;
            logger = Logger;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
               
            }

        
        }
    }
}
