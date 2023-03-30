using RestfulBooker.GetBookingTests;

namespace RestfulBooker.CreateBookingTests
{
    public class CreateBookingResponse
    {
        public int bookingid { get; set; }
        public GetBookingResponse booking { get; set; }
        public List<string> errors { get; set; }
        public string reason { get; set; }
    }
}
