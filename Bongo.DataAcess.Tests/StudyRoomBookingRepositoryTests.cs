using Bongo.DataAccess;
using Bongo.DataAccess.Repository;
using Bongo.Models.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections;

namespace Bongo.DataAcess.Tests
{
    // unit test for data access class is redundant as efcore already does this
    [TestFixture]
    public class StudyRoomBookingRepositoryTests
    {
        private StudyRoomBooking studyRoomBooking_One;
        private StudyRoomBooking studyRoomBooking_Two;
        private DbContextOptions<ApplicationDbContext> options;
        public StudyRoomBookingRepositoryTests()
        {
            studyRoomBooking_One = new StudyRoomBooking()
            {
                FirstName = "Ben1",
                LastName = "Spark1",
                Date = new DateTime(2024, 1, 1),
                Email = "ben1@gmail.com",
                BookingId = 11,
                StudyRoomId = 1,
            };

            studyRoomBooking_Two = new StudyRoomBooking()
            {
                FirstName = "Ben2",
                LastName = "Spark2",
                Date = new DateTime(2024, 2, 2),
                Email = "ben2@gmail.com",
                BookingId = 22,
                StudyRoomId = 2,
            };
        }
        [SetUp]
        public void SetUp() 
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase(databaseName: "temp_Bongo").Options;
        }
        [Test]
        [Order(1)]
        public void SaveBooking_Booking_One_CheckValuesFromDataBase()
        {
            // arrange

            // act
            using (var context = new ApplicationDbContext(options)) 
            { 
                var repository = new StudyRoomBookingRepository(context);
                repository.Book(studyRoomBooking_One);
            }
            // assert 
            using (var context = new ApplicationDbContext(options))
            {
                var bookingFromDb = context.StudyRoomBookings.FirstOrDefault(u => u.BookingId == 11);
                Assert.AreEqual(studyRoomBooking_One.BookingId, bookingFromDb.BookingId);
                Assert.AreEqual(studyRoomBooking_One.FirstName, bookingFromDb.FirstName);
                Assert.AreEqual(studyRoomBooking_One.LastName, bookingFromDb.LastName);
                Assert.AreEqual(studyRoomBooking_One.Email, bookingFromDb.Email);
                Assert.AreEqual(studyRoomBooking_One.Date, bookingFromDb.Date);
            }
        }

        [Test]
        [Order(2)]
        public void GetAllBooking_Booking_OneAndTwo_CheckBothTheBookingsFromDataBase()
        {
            // arrange
            var expectedResult = new List<StudyRoomBooking> { studyRoomBooking_One, studyRoomBooking_Two };

            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new StudyRoomBookingRepository(context);
                repository.Book(studyRoomBooking_One);
                repository.Book(studyRoomBooking_Two);
            }
            //
            List<StudyRoomBooking> actualList;
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new StudyRoomBookingRepository(context);
                actualList = repository.GetAll(null).ToList();
            }

            // assert 
            CollectionAssert.AreEqual(expectedResult, actualList, new BookingCompare());
        }
        private class BookingCompare : IComparer
        {
            public int Compare(object? x, object? y)
            {
                var booking1 = (StudyRoomBooking)x;
                var booking2 = (StudyRoomBooking)y;
                if (booking1.BookingId != booking2.BookingId)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
        }
    }
}
