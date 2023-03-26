using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces;

/// <summary>
/// Interface that defines the behaviour to be implemented by SMS validators
/// </summary>
public interface ISmsMessageValidator
{
    /// <summary>
    /// Takes a <see cref="SmsMessageRequest"/> as input and performs validation on the relevant properties
    /// </summary>
    /// <param name="request"></param>
    /// <returns>A list of <see cref="ValidationIssue"/>, where a count of 0 indicates no issues</returns>
    Task<List<ValidationIssue>> ValidateAsync(SmsMessageRequest request);
}