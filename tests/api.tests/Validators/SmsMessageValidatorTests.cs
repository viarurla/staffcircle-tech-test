using System.Threading.Tasks;
using api.Models;
using api.Validators;
using FluentAssertions;
using Moq.AutoMock;
using Xunit;
using Xunit.Abstractions;

namespace api.tests.Validators;

public class SmsMessageValidatorTests
{
    private readonly ITestOutputHelper _output;
    private readonly AutoMocker _mocker;

    public SmsMessageValidatorTests(ITestOutputHelper output)
    {
        _output = output;
        _mocker = new AutoMocker();
    }
    
    [Theory]
    [InlineData("", 1)]
    [InlineData("+447559994335", 0)]
    [InlineData("+123456789101112131415161718", 1)]
    public async Task Should_Validate_Msisdn_Length(string msisdn, int expectedIssueCount)
    {
        var validator = _mocker.CreateInstance<SmsMessageValidator>();
        var messageRequest = new SmsMessageRequest
        {
            ToMsisdn = msisdn,
            FromMsisdn = "+4475575558449",
            Body = "Hello there"
        };
        var result = await validator.ValidateAsync(messageRequest);
        foreach (ValidationIssue issue in result)
        {
            _output.WriteLine(issue.Message);
        }
        result.Count.Should().Be(expectedIssueCount);
    }
    
    [Theory]
    [InlineData(200, 1)]
    [InlineData(10, 0)]
    [InlineData(0, 1)]
    public async Task Should_Validate_Sms_Body_Length(int bodyLength, int expectedIssueCount)
    {
        var validator = _mocker.CreateInstance<SmsMessageValidator>();
        var messageRequest = new SmsMessageRequest
        {
            ToMsisdn = "+123456789",
            FromMsisdn = "+123456789",
            // Programmatically create a message to match our test case
            Body = bodyLength == 0 ? string.Empty : "a".PadLeft(bodyLength, 'a')
        };
        var result = await validator.ValidateAsync(messageRequest);
        foreach (ValidationIssue issue in result)
        {
            _output.WriteLine(issue.Message);
        }
        result.Count.Should().Be(expectedIssueCount);
    }
}