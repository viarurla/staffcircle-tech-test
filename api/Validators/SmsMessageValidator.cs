using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.Extensions.Logging;

namespace api.Validators;

public class SmsMessageValidator : ISmsMessageValidator
{
    private readonly ILogger<SmsMessageValidator> _logger;
    private readonly List<ValidationIssue> _issues = new();

    public SmsMessageValidator(ILogger<SmsMessageValidator> logger)
    {
        _logger = logger;
    }

    public async Task<List<ValidationIssue>> ValidateAsync(SmsMessageRequest request)
    {
        await ValidateMessageLength(request);
        await ValidateMsisdnValues(request);
        return _issues;
    }
    
    /// <summary>
    /// Validates the acceptance criteria for MSISDN values
    /// This primarily focuses on the length of the value
    /// </summary>
    /// <param name="request"></param>
    private Task ValidateMsisdnValues(SmsMessageRequest request)
    {
        foreach (var msisdn in request.GetMsisdnValues())
        {
            if (string.IsNullOrEmpty(msisdn.Value))
            {
                _issues.Add(new()
                {
                    Message = $"{msisdn.Key} MSISDN cannot be empty"
                });
            }
            // This would likely be kept as a configuration value
            // Despite the unlikely change in the near future, it would be good practice to avoid magic numbers
            else if (msisdn.Value.Length > 15)
            {
                _issues.Add(new()
                {
                    Message = $"{msisdn.Key} value exceeds maximum allowed length: ({msisdn.Key}: {msisdn.Value})"
                });
            }
        }

        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Validates whether the incoming message body exceeds the character limit
    /// Please note that this does not currently account for unicode characters, which can limit the size to 70
    /// In the real world the behaviour for such a factor can be configured in Twilio itself, but it wouldn't be
    /// bad to do so in the validation as a means of ensuring that this cannot break throughout each stage.
    /// </summary>
    /// <param name="request"></param>
    private Task ValidateMessageLength(SmsMessageRequest request)
    {
        if (string.IsNullOrEmpty(request.Body))
        {
            _issues.Add(new()
            {
                Message = "Message cannot be empty"
            });
        }
        // Again, this will likely never change but in the real world we would probably add this as a config value
        else if (request.Body.Length > 160)
        {
            _issues.Add(new()
            {
                Message = "Message exceeds the allowed character limit for SMS content"
            });
        }

        return Task.CompletedTask;
    }

}