using System;

namespace api.Models;

public class SmsMessageResponse: SmsMessageBase
{
    public DateTime? SentDateTime { get; set; }
    public SmsMessageResponse()
    {
        
    }
}