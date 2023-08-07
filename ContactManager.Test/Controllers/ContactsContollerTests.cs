using ContactManager.API.Controllers;
using ContactManager.API.Entities;
using ContactManager.API.Exceptions;
using ContactManager.API.Helper;
using ContactManager.API.Models;
using ContactManager.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Test.Controllers
{
    public class ContactsContollerTests
    {
        private ContactsController _controller;
        private Mock<IContactService> _mockService;
        private Mock<IGetUser> _mockGetUser;

        public ContactsContollerTests()
        {
            _mockService = new Mock<IContactService>();
            _mockGetUser = new Mock<IGetUser>();
            _controller = new ContactsController(_mockService.Object, _mockGetUser.Object);
        }

        [Fact]
        public async Task GetContactsAsync_ValidUser_ReturnContacts()
        {
            //ARRANGE
            var userId = 1;
            var expectedContacts = new List<ContactWithoutDetailsDto>
            {
                new ContactWithoutDetailsDto
                    {
                        FirstName = "Test",
                        LastName = "Test",
                        Favorite = false,
                        Emergency = false
                    },
                new ContactWithoutDetailsDto
                {
                    FirstName = "Test2",
                    LastName = "Test2",
                    Favorite = true,
                    Emergency = false
                }
            };
            var expectedUser = new User
            {
                Username = "test",
                Id = userId
            };
            
            _mockGetUser.Setup(c => c.Get())
                .Returns(expectedUser);
            _mockService.Setup(c => c.GetContacts(userId))
                .ReturnsAsync(expectedContacts);

            //ACT
            var response = await _controller.GetContactsAsync();
            //ASSERT
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var actualContacts = Assert.IsAssignableFrom<IEnumerable<ContactWithoutDetailsDto>>(okResult.Value);
            Assert.Equal(expectedContacts, actualContacts);
            Assert.Equal(200, okResult.StatusCode);
        }
        [Fact]
        public async Task GetContactsAsync_UserNotFound_ReturnsNotFound()
        {
            var userId = 0;
            var expectedUser = new User
            {
                Username = "test",
                Id = userId
            };
            _mockGetUser.Setup(c => c.Get())
                .Returns(expectedUser);
            _mockService.Setup(c => c.GetContacts(userId))
                .ThrowsAsync(new UserNotFoundException());

            //ACT
            var response = await _controller.GetContactsAsync();

            //ASSERT
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }
        [Fact]
        public async Task GetContactsAsync_Exception_ReturnsStatusCode500()
        {
            var userId = 0;
            var expectedUser = new User
            {
                Username = "test",
                Id = userId
            };
            _mockGetUser.Setup(c => c.Get())
                .Returns(expectedUser);
            _mockService.Setup(c => c.GetContacts(userId))
                .ThrowsAsync(new Exception());

            //ACT
            var response = await _controller.GetContactsAsync();

            //ASSERT
            var objectResult = Assert.IsType<ObjectResult>(response.Result);
            Assert.Equal(500, objectResult.StatusCode);
            Assert.Equal("Something went wrong.", objectResult.Value);
        }
        [Fact]
        public async Task GetContactAsync_ValidUserAndContact_ReturnsContact()
        {
            var userId = 1;
            var contactId = 1;
            var expectedUser = new User
            {
                Username = "test",
                Id = userId
            };
            var expectedContact = new ContactDto
            {
                FirstName = "Test",
                LastName = "Test",
                Favorite = true,
                Emergency = false
            };

            _mockGetUser.Setup(c => c.Get())
                .Returns(expectedUser);
            _mockService.Setup(c => c.GetContact(userId, contactId))
               .ReturnsAsync(expectedContact);

            //ACT
            var response = await _controller.GetContactAsync(contactId);

            //ASSERT
            var okObject = Assert.IsType<OkObjectResult>(response);
            var resultContact = Assert.IsType<ContactDto>(okObject.Value);
            Assert.Equal(200, okObject.StatusCode);
            Assert.Equal(expectedContact.Id, resultContact.Id);
        }
        [Fact]
        public async Task GetContactAsync_UserNotFound_ReturnsNotFound()
        {
            var userId = 0;
            var contactId = 0;
            var expectedUser = new User
            {
                Username = "test",
                Id = userId
            };
            _mockGetUser.Setup(c => c.Get())
                .Returns(expectedUser);
            _mockService.Setup(c => c.GetContact(userId, contactId))
                .ThrowsAsync(new UserNotFoundException("Not Found"));

            //ACT

            var response = await _controller.GetContactAsync(contactId);


            //ASSERT
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, notFoundResult.StatusCode);
            var errorMessage = Assert.IsType<string>(notFoundResult.Value);
        }
        [Fact]
        public async Task GetContactAsync_ContactNotFound_ReturnsNotFound()
        {
            var userId = 0;
            var contactId = 0;
            var expectedUser = new User
            {
                Username = "test",
                Id = userId
            };
            _mockGetUser.Setup(c => c.Get())
                .Returns(expectedUser);
            _mockService.Setup(c => c.GetContact(userId, contactId))
                .ThrowsAsync(new ContactNotFoundException("Not Found"));

            //ACT

            var response = await _controller.GetContactAsync(contactId);


            //ASSERT
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response);
            Assert.Equal(404, notFoundResult.StatusCode);
            var errorMessage = Assert.IsType<string>(notFoundResult.Value);
        }
    }
}
