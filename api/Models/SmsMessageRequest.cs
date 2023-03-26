using System.Collections.Generic;

namespace api.Models;

public class SmsMessageRequest: SmsMessageBase
{
    
    /// <summary>
    /// Returns the MSISDN related properties of <see cref="SmsMessageRequest"/>
    /// As a dictionary, which helps with validation message creation.
    /// </summary>
    /// <returns><see cref="Dictionary{TKey,TValue}"/></returns>
    public Dictionary<string, string> GetMsisdnValues()
    {
        var msisdns = new Dictionary<string, string>
        {
            {nameof(ToMsisdn), ToMsisdn ?? string.Empty},
            {nameof(FromMsisdn), FromMsisdn ?? string.Empty}
        };
        
        return msisdns;
    }

}