using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandler
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestResponseMiddleware> logger;

        public RequestResponseMiddleware(RequestDelegate Next, ILogger<RequestResponseMiddleware> Logger)
        {
            next = Next;
            logger = Logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            // original stream backup
            var originalBodyStream = httpContext.Response.Body;

            //Request
            logger.LogInformation($"Query Keys: {String.Join(',', httpContext.Request.Query.Keys)}");
          
            MemoryStream requestBody = new MemoryStream();
            await httpContext.Request.Body.CopyToAsync(requestBody);
            requestBody.Seek(0, SeekOrigin.Begin);
            String requestText = await new StreamReader(requestBody).ReadToEndAsync();
            requestBody.Seek(0, SeekOrigin.Begin);

            var tempStream = new MemoryStream();
            httpContext.Response.Body = tempStream;
            await next.Invoke(httpContext); // response created
            tempStream.Seek(0, SeekOrigin.Begin);
            String responseText = await new StreamReader(tempStream, Encoding.UTF8).ReadToEndAsync();
            tempStream.Seek(0, SeekOrigin.Begin);

            await httpContext.Response.Body.CopyToAsync(originalBodyStream);
        }
    }
}
