namespace api.Options;

/// <summary>
/// An options class to encapsulate the configuration parameters needed for Twilio
/// </summary>
public class TwilioConfigValues
{
    /// <summary>
    /// The AccountSID as defined in the Twilio Developer Console
    /// </summary>
    public string AccountSid { get; set; } = null!;

    /// <summary>
    /// The AuthToken as defined in the Twilio Developer Console
    /// </summary>
    public string AuthToken { get; set; } = null!;
}