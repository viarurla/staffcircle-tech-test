namespace api.Models;

public class SmsMessageBase
{
    /// <summary>
    /// The target MSISDN for the message
    /// </summary>
    public string? ToMsisdn { get; set; }
    /// <summary>
    /// The source MSISDN from which the message will originate
    /// </summary>
    public string? FromMsisdn { get; set; }
    /// <summary>
    /// The content of the SMS message
    /// </summary>
    public string? Body { get; set; }
}