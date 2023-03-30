using Configuration.APIRequest;
using Configuration.Client;
using Configuration;
using NUnit.Framework;
using RestfulBooker.GetBookingIdsTests;
using RestSharp;
using RestSharp.Serializers;
using RestfulBooker.CreateBookingTests;

namespace RestfulBooker.GetBookingTests
{
    [TestFixture]
    internal class Tests
    {
        public IClient _client;
        public string _url = "https://restful-booker.herokuapp.com/booking/{id}";

        [SetUp]
        public void SetUp()
        {
            _client = new DefaultClient();
        }

        [Test]
        public void SuccessfulTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Accept","application/json" }
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id",GetCreatedBookingId() }
            };
            var request = new GetRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient().Execute<GetBookingResponse>(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.AreEqual(objectBody.firstname, response.Data.firstname);
                Assert.AreEqual(objectBody.lastname, response.Data.lastname);
                Assert.AreEqual(objectBody.totalprice, response.Data.totalprice);
                Assert.AreEqual(objectBody.depositpaid, response.Data.depositpaid);
                Assert.AreEqual(objectBody.bookingdates.checkin, response.Data.bookingdates.checkin);
                Assert.AreEqual(objectBody.bookingdates.checkout, response.Data.bookingdates.checkout);
                Assert.AreEqual(objectBody.additionalneeds, response.Data.additionalneeds);
            });
        }

        [Test]
        public void ResponseIsJsonTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Accept","application/json" }
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id",GetRandomdBookingId() }
            };
            var request = new GetRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var getBookingResponse = _client.GetClient().Execute(request);


            Assert.AreEqual(getBookingResponse.ContentType, ContentType.Json);
        }

        [Test]
        public void ResponseIsXmlTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Accept","application/xml" }
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id",GetRandomdBookingId() }
            };
            var request = new GetRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var getBookingResponse = _client.GetClient().Execute(request);


            Assert.AreEqual(getBookingResponse.ContentType, ContentType.Xml);
        }
        
        internal static CreateBookingRequest objectBody = new CreateBookingRequest()
        {
            firstname = "MadeUp",
            lastname = "AlsoMadeUp",
            totalprice = 1508,
            depositpaid = true,
            bookingdates = new BookingDatesRequest()
            {
                checkin = DateTime.Now.ToString("yyyy'-'MM'-'dd"),
                checkout = DateTime.Now.ToString("yyyy'-'MM'-'dd")
            },
            additionalneeds = "Dorucak"
        };

        public string GetCreatedBookingId()
        {
            string _BookingUrl = "https://restful-booker.herokuapp.com/booking";
            _client = new DefaultClient();


            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json"}
            };
            var request = new PostRequestBuilder()
                .WithUrl(_BookingUrl)
                .WithHeaders(headers)
                .WithObjectBody(objectBody)
                .Build();
            var response = _client.GetClient().Execute<CreateBookingResponse>(request);
            
            
            _client.Dispose();
            return response.Data.bookingid.ToString();
        }

        public string GetRandomdBookingId()
        {
            string _BookingUrl = "https://restful-booker.herokuapp.com/booking";
            _client = new DefaultClient();


            var request = new GetRequestBuilder()
                .WithUrl(_BookingUrl)
                .Build();
            var response = _client.GetClient()
                .Execute<List<GetBookingIdsResponse>>(request);


            _client.Dispose();
            return response.Data[0].bookingid.ToString();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

    }
}
