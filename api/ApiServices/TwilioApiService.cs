using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace api.ApiServices;

public class TwilioApiService : ITwilioApiService
{
    private readonly ILogger<TwilioApiService> _logger;
    private readonly IOptions<TwilioConfigValues> _options;
    
    public TwilioApiService(ILogger<TwilioApiService> logger, IOptions<TwilioConfigValues> options)
    {
        _logger = logger;
        _options = options;
        TwilioClient.Init(_options.Value.AccountSid, _options.Value.AuthToken);
    }

    public async Task<SmsMessageResponse> SendSmsMessageAsync(SmsMessageRequest request)
    {
        try
        {
            var message = await MessageResource.CreateAsync(
                to:  new PhoneNumber(request.ToMsisdn), 
                from: new PhoneNumber(request.FromMsisdn),
                body: request.Body);
            return new()
            {
                ToMsisdn = message.To,
                FromMsisdn = message.From.ToString(),
                // todo: this currently will return null, as there is a time discrepancy between creation and sending
                SentDateTime = message.DateSent,
                Body = message.Body
            };
        }
        // todo: handle exception
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public Task<SmsMessageResponse> GetSmsMessageAsync()
    {
        // todo: flesh out
        throw new NotImplementedException();
    }

    public async Task<List<SmsMessageResponse>> GetSmsMessagesAsync(MessageQueryParameters queryParameters)
    {
        try
        {
            // todo: pagination and date filtering
            var messagesResourceSet = await MessageResource.ReadAsync();
            var messages = messagesResourceSet.Select(m => new SmsMessageResponse
            {
                ToMsisdn = m.To,
                FromMsisdn = m.From.ToString(),
                Body = m.Body,
                SentDateTime = m.DateSent
            });
            return messages.ToList();
        }
        // todo: handle exception properly
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

}