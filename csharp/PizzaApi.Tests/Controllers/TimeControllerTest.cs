using System;
using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PizzaAPI.Controllers;
using PizzaAPI.Data.Repositories;
using Xunit;

namespace PizzaAPI.Tests.Controllers
{
    public class TimeControllerTest
    {
        private readonly TimeController _controller;
        private readonly Mock<ITimeRepository> _timeRepoMock;
        private readonly DateTime _expectedDateTime;
        
        public TimeControllerTest()
        {
            _timeRepoMock = new Mock<ITimeRepository>();
            _expectedDateTime = new Bogus.DataSets.Date().Soon();

            _timeRepoMock.Setup(time => time.GetTime()).Returns(_expectedDateTime);
            _controller = new TimeController(_timeRepoMock.Object);
        }

        [Fact]
        public void ShouldReturnOkStatusCode()
        {
            var result = _controller.Get();
            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();
            okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Fact]
        public void ShouldReturnTimeResultAsResponseValue()
        {
            var result = _controller.Get();
            var okResult = result as OkObjectResult;

            okResult.Should().NotBeNull();
            okResult?.Value.Should().Be(_expectedDateTime);
        }
    }
}
