using Api.Models;
using Api.Service.Controllers;
using Domain.Abstractions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Service.Tests.Api.Service.Tests
{
    public class CustomersControllerTetsts
    {
        private Mock<ICustomerService> _mockCustomerService;
        private Mock<ILogger<CustomersController>> _mockLogger;
        private CustomersController _controller;

        [SetUp]
        public void Setup()
        {
            _mockCustomerService = new Mock<ICustomerService>(MockBehavior.Strict);
            _mockLogger = new Mock<ILogger<CustomersController>>(MockBehavior.Loose);

            _controller = new CustomersController(
                _mockLogger.Object,
                _mockCustomerService.Object
            );
        }

        [TearDown]
        public void TearDown()
        {
            _mockCustomerService.VerifyAll();
            _mockLogger.VerifyAll();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_IfLoggerIsNull()
        {
            // Arrange
            // Act
            Action action = () => new CustomersController(
                null,
                _mockCustomerService.Object
            );

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_IfCustomerServiceIsNull()
        {
            // Arrange
            // Act
            Action action = () => new CustomersController(
                _mockLogger.Object,
                null
            );

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Test]
        public async Task ListAsync_ThrowsException()
        {
            // Arrange
            var mockedRecords = new List<Customer>()
            {
                new() {
                    Id = 1,
                    Name = "Alan",
                    Phone = "0123456789",
                    Email = "test@test.com"
                }
            };
            var mockedResponse = new OkObjectResult(mockedRecords);
            _mockCustomerService.Setup(x => x.ListCustomers()).ReturnsAsync(mockedRecords);

            // Act
            var result = await _controller.ListAll();

            // Assert
            result.Should().BeEquivalentTo(mockedResponse);
        }
    }
}
