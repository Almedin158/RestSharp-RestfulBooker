using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBooker.GetBookingTests
{
    public class BookingdatesResponse
    {
        public string checkin { get; set; }
        public string checkout { get; set; }
    }

    public class GetBookingResponse
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int totalprice { get; set; }
        public bool depositpaid { get; set; }
        public BookingdatesResponse bookingdates { get; set; }
        public string additionalneeds { get; set; }
    }
}
