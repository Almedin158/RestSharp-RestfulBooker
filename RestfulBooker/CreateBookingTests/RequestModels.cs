using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBooker.CreateBookingTests
{
    public class CreateBookingRequest
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int totalprice { get; set; }
        public object depositpaid { get; set; }
        public BookingDatesRequest bookingdates { get; set; }
        public string additionalneeds { get; set; }
    }

    public class BookingDatesRequest
    {
        public string checkin { get; set; }
        public string checkout { get; set; }
    }
}
