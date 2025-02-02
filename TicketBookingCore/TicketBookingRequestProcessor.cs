
using System.Runtime.InteropServices;

namespace TicketBookingCore
{
    public class TicketBookingRequestProcessor
    {
        private readonly ITicketBookingRepository _ticketBookingRepository;
        public TicketBookingRequestProcessor(ITicketBookingRepository ticketBookingRepository)
        {
            _ticketBookingRepository = ticketBookingRepository;
        }

        public TicketBookingResponse Book(TicketBookingRequest request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            /*
            _ticketBookingRepository.Save(new TicketBooking //sparar i databasen
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            });

            return new TicketBookingResponse  //returnera ny ticketbookingresponse
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
            }; */

            _ticketBookingRepository.Save(Create<TicketBooking>(request));

            return Create<TicketBookingResponse>(request);
        }

        private static T Create<T>(TicketBookingRequest request) where T : TicketBookingBase, new()
        {
            return new T
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            };
        }
    }
}