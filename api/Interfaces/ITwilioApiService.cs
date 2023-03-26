using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces;

/// <summary>
/// Interface to define the behaviour of the Twilio API service
/// </summary>
public interface ITwilioApiService
{
    /// <summary>
    /// Using an input <see cref="SmsMessageRequest"/>, sends a request to create a new SMS to the Twilio API
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<SmsMessageResponse> SendSmsMessageAsync(SmsMessageRequest request);
    /// <summary>
    /// Retrieves the desired <see cref="SmsMessageResponse"/> via identifier
    /// </summary>
    /// <returns></returns>
    Task<SmsMessageResponse> GetSmsMessageAsync();
    /// <summary>
    /// Using <see cref="MessageQueryParameters"/> as a filter, return a collection of <see cref="SmsMessageResponse"/>
    /// associated with the Twilio Account defined elsewhere in the application.
    /// </summary>
    /// <param name="queryParameters"></param>
    /// <returns></returns>
    Task<List<SmsMessageResponse>> GetSmsMessagesAsync(MessageQueryParameters queryParameters);
}