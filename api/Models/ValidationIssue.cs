namespace api.Models;

/// <summary>
/// Basic class to act as a POC for how to return validation issues
/// In a real world scenario this may be fleshed out with loglevel information and object properties
/// </summary>
public class ValidationIssue
{
    /// <summary>
    /// The description of the issue that has been identified
    /// </summary>
    public string? Message { get; set; }
}