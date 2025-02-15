using Moq;

namespace TicketBookingCore.Tests
{
    public class TicketBookingRequestProcessorTests
    {
        private readonly TicketBookingRequest _request;
        private readonly Mock<ITicketBookingRepository> _ticketBookingRepositoryMock;
        private readonly TicketBookingRequestProcessor _processor;
        public TicketBookingRequestProcessorTests()
        {
            _request = new TicketBookingRequest
            {
                FirstName = "Sandra",
                LastName = "Turesson",
                Email = "sandra@gmail.com"
            };
            _ticketBookingRepositoryMock = new Mock<ITicketBookingRepository>();
        _processor = new TicketBookingRequestProcessor(_ticketBookingRepositoryMock.Object); 
        }

        
        [Fact]
        public void ShouldReturnTicketBookingResultWithRequestValue()
        {
            //Arrange
            var request = new TicketBookingRequest
            {
                FirstName = "Sandra",
                LastName = "Turesson",
                Email = "sandra@gmail.com"
            };

            //Act
            TicketBookingResponse response = _processor.Book(request);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(request.FirstName, response.FirstName);
            Assert.Equal(request.LastName, response.LastName);
            Assert.Equal(request.Email, response.Email);
        } 

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            //Arrange
            
            //Act
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.Book(null));
            //Assert
            Assert.Equal("request", exception.ParamName);
        } 

       


        [Fact]
        public void ShouldSaveToDatabase()
        {
        //Arrange
            TicketBooking savedTicketBooking = null;

        //Save metoden
            _ticketBookingRepositoryMock.Setup(x => x.Save(It.IsAny<TicketBooking>()))
            .Callback<TicketBooking>((ticketBooking) =>
            {
                savedTicketBooking = ticketBooking;
            });

        /* var request = new TicketBookingRequest
            {
                FirstName = "Celina",
                LastName = "Tsson",
                Email = "celina@gmail.com"
            }; */

        //Act
            _processor.Book(_request);

            _ticketBookingRepositoryMock.Verify(x => x.Save(It.IsAny<TicketBooking>()), Times.Once());

        //Assert
            Assert.NotNull(savedTicketBooking);
            Assert.Equal(_request.FirstName, savedTicketBooking.FirstName);
            Assert.Equal(_request.LastName, savedTicketBooking.LastName);
            Assert.Equal(_request.Email, savedTicketBooking.Email);

        }
    }
}