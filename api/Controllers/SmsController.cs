using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ILogger<SmsController> _logger;
        private readonly ISmsMessageValidator _messageValidator;
        private readonly ITwilioApiService _apiService;

        public SmsController(ILogger<SmsController> logger, ISmsMessageValidator messageValidator, ITwilioApiService apiService)
        {
            _logger = logger;
            _messageValidator = messageValidator;
            _apiService = apiService;
        }

        [HttpGet("GetMessages")]
        public async Task<OkObjectResult> GetMessages([FromQuery]MessageQueryParameters queryParameters)
        {
            try
            {
                var messages = await _apiService.GetSmsMessagesAsync(queryParameters);
                return new OkObjectResult(messages);
            }
            // todo: handle exception more specifically
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost("PostMessage")]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Post(SmsMessageRequest request)
        {
            // todo: remove hard-coding of the account SMS in a real application
            request.FromMsisdn = "+447700154542";
            var issues = await _messageValidator.ValidateAsync(request);
            if (issues.Any())
            {
                // Very rudimentary, but suitable as a proof of concept
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    ReasonPhrase = string.Join(", ", issues.Select(msg => msg.Message))
                };
                return new BadRequestObjectResult(resp);
            }

            var result = await _apiService.SendSmsMessageAsync(request);
            return new OkObjectResult(result);
        }
        
    }
}
