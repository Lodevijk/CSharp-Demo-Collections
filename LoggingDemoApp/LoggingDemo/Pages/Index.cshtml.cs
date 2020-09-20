using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LoggingDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _logger;

        //// standard way to capture the category
        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}
        public IndexModel(ILoggerFactory factory)
        {
            _logger = factory.CreateLogger("DemoCategory");
        }

        public void OnGet()
        {
            //// Different logging levels
            //_logger.LogTrace("This is a trace log");
            //_logger.LogDebug("This is a debug log");
            //// Demo Code
            //_logger.LogInformation(LoggingId.DemoCode, "This is an info message");
            //_logger.LogWarning("This is a warning log");
            //_logger.LogError("This is an error log");
            //_logger.LogCritical("This is a critical log");

            //// log exceptions
            _logger.LogError("The server is offline temporarily at {Time}", DateTime.UtcNow);

            try
            {
                throw new Exception("You forgot to catch me");
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "There was a bad exception at {Time}", DateTime.UtcNow);
            }
        }
    }

    public class LoggingId
    {
        public const int DemoCode = 1001;
    }
}
