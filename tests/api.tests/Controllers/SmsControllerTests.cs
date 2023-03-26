using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using api.Interfaces;
using api.Models;
using api.Validators;
using Moq;
using Moq.AutoMock;
using Xunit.Abstractions;
using SmsController = api.Controllers.SmsController;

namespace api.tests.Controllers
{
    /// <summary>
    /// Test class to handle 
    /// </summary>
    public class SmsControllerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly AutoMocker _mocker;

        public SmsControllerTests(ITestOutputHelper output)
        {
            _output = output;
            _mocker = new AutoMocker();
        }

        private List<SmsMessageResponse> GetNewMessages(int count)
        {
            var messages = new List<SmsMessageResponse>();
            foreach (int i in Enumerable.Range(0, count))
            {
                messages.Add(new()
                {
                    ToMsisdn = i.ToString().PadLeft(10),
                    FromMsisdn = i.ToString().PadLeft(10),
                    Body = i.ToString().PadLeft(10),
                    SentDateTime = DateTime.Now.AddDays(i)
                });
            }

            return messages;
        }
        
        [Fact]
        public async Task GetMessages_ReturnsActionResult_WithCollectionOfMessageResponses()
        {
            var messages = GetNewMessages(2);
            var twilioApiService = _mocker.GetMock<ITwilioApiService>();
            twilioApiService.Setup(x => x.GetSmsMessagesAsync(It.IsAny<MessageQueryParameters>())).ReturnsAsync(messages);
            var smsController = _mocker.CreateInstance<SmsController>();
            
            var result = await smsController.GetMessages(new MessageQueryParameters());
        
            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<SmsMessageResponse>>(
                viewResult.Value);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Post_ReturnsActionResult_With200Status()
        {
            var twilioApiService = _mocker.GetMock<ITwilioApiService>();
            twilioApiService.Setup(x => x.SendSmsMessageAsync(It.IsAny<SmsMessageRequest>())).ReturnsAsync(new SmsMessageResponse());

            var validator = _mocker.GetMock<ISmsMessageValidator>();
            validator.Setup(x => x.ValidateAsync(It.IsAny<SmsMessageRequest>()))
                .ReturnsAsync(new List<ValidationIssue>());
            
            var smsController = _mocker.CreateInstance<SmsController>();
            var result = await smsController.Post(new SmsMessageRequest());
        
            // Assert
            var viewResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, viewResult.StatusCode);
        }
    }
}
