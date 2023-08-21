using System.Security.Cryptography.X509Certificates;
using Bongo.Core.Services.IServices;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Bongo.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Bongo.Web.Tests
{
    [TestFixture]
    public class RoomBookingControllerTests
    {
        private Mock<IStudyRoomBookingService> _studyRoomBookingServiceMock;
        private RoomBookingController _bookingController;

        [SetUp]
        public void Setup()
        {
            _studyRoomBookingServiceMock = new Mock<IStudyRoomBookingService>();
            _bookingController = new RoomBookingController(_studyRoomBookingServiceMock.Object);
        }

        [Test]
        public void IndexPage_CallRequest_VerifyGetAllInvoked()
        {
            _bookingController.Index();
            _studyRoomBookingServiceMock.Verify(x => x.GetAllBooking(), Times.Once);
        }

        [Test]
        public void BookRoomCheck_ModelStateInvalid_ReturnView()
        {
            _bookingController.ModelState.AddModelError("test", "test");
            var result = _bookingController.Book(new StudyRoomBooking());
            ViewResult viewResult = result as ViewResult;
            Assert.AreEqual("Book", viewResult.ViewName);
        }

        [Test]
        public void BookRoomCheck_NotSuccessful_NoRoomCode()
        {
            _studyRoomBookingServiceMock.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
                .Returns(new StudyRoomBookingResult()
                {
                    Code = StudyRoomBookingCode.NoRoomAvailable,
                });

            var result = _bookingController.Book(new StudyRoomBooking());
            Assert.IsInstanceOf<ViewResult>(result);
            ViewResult viewResult = result as ViewResult;
            Assert.AreEqual("No Study Room available for selected date",
             viewResult.ViewData["Error"]);
        }
        [Test]
        public void BookRoomCheck_Successful_SuccessCodeAndRedirect()
        {
            // arrange
            _studyRoomBookingServiceMock.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
                .Returns((StudyRoomBooking booking) => new StudyRoomBookingResult
                {
                    Code = StudyRoomBookingCode.Success,
                    FirstName = booking.FirstName,
                    LastName = booking.LastName,
                    Date = booking.Date,
                    Email = booking.Email,
                });

            // act
            var result = _bookingController.Book(new StudyRoomBooking()
            {
                Date = DateTime.Now,
                Email = "sa@gmail.com",
                FirstName = "Hello",
                LastName = "Dotn",
                StudyRoomId = 1
            });
            // assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            RedirectToActionResult actionResult = result as RedirectToActionResult;
            Assert.AreEqual("Hello", actionResult.RouteValues["FirstName"]);
            Assert.AreEqual(StudyRoomBookingCode.Success, actionResult.RouteValues["Code"]);

        }
    }
}