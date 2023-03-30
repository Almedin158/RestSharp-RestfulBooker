using Configuration;
using Configuration.APIRequest;
using Configuration.Client;
using Newtonsoft.Json;
using NUnit.Framework;
using RestfulBooker.CreateBookingTests;
using RestfulBooker.GetBookingTests;
using RestfulBooker.LoginTests;
using RestSharp;
using RestSharp.Serializers;

namespace RestfulBooker.UpdateBookingTests
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
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname="Neko",
                lastname="Nesto",
                totalprice=1234,
                depositpaid=false,
                bookingdates = new BookingDatesRequest()
                {
                    checkin = ("2000-01-01"),
                    checkout = ("2000-01-01")
                },
                additionalneeds="Nista"
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PutRequestBuilder()
                .WithUrl(_url)
                .WithObjectBody(objectBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute<GetBookingResponse>(request);


            List<int> statusCodes = new List<int> { 200, 202, 204 };
            Assert.Multiple(() =>
            {
                Assert.IsTrue(statusCodes.Contains((int)response.StatusCode));
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
        public void IncorrectTokenTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token=incorrectToken" }
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = "Neko",
                lastname = "Nesto",
                totalprice = 1234,
                depositpaid = false,
                bookingdates = new BookingDatesRequest()
                {
                    checkin = ("2000-01-01"),
                    checkout = ("2000-01-01")
                },
                additionalneeds = "Nista"
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PutRequestBuilder()
                .WithUrl(_url)
                .WithObjectBody(objectBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(401, (int)response.StatusCode);
        }

        [Test]
        public void NoTokenTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" }
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = "Neko",
                lastname = "Nesto",
                totalprice = 1234,
                depositpaid = false,
                bookingdates = new BookingDatesRequest()
                {
                    checkin = ("2000-01-01"),
                    checkout = ("2000-01-01")
                },
                additionalneeds = "Nista"
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PutRequestBuilder()
                .WithUrl(_url)
                .WithObjectBody(objectBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(401, (int)response.StatusCode);
        }

        [Test]
        public void ResponseIsJsonTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = "Neko",
                lastname = "Nesto",
                totalprice = 1234,
                depositpaid = false,
                bookingdates = new BookingDatesRequest()
                {
                    checkin = ("2000-01-01"),
                    checkout = ("2000-01-01")
                },
                additionalneeds = "Nista"
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PutRequestBuilder()
                .WithUrl(_url)
                .WithObjectBody(objectBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute<GetBookingResponse>(request);


            Assert.AreEqual(response.ContentType, ContentType.Json);
        }

        [Test]
        public void ResponseIsXmlTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/xml" },
                {"Cookie", "token="+GetToken() }
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = "Neko",
                lastname = "Nesto",
                totalprice = 1234,
                depositpaid = false,
                bookingdates = new BookingDatesRequest()
                {
                    checkin = ("2000-01-01"),
                    checkout = ("2000-01-01")
                },
                additionalneeds = "Nista"
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PutRequestBuilder()
                .WithUrl(_url)
                .WithObjectBody(objectBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(response.ContentType, ContentType.Xml);
        }

        [Test]
        public void InvalidTotalPriceTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = "Neko",
                lastname = "Nesto",
                totalprice = -1,
                depositpaid = false,
                bookingdates = new BookingDatesRequest()
                {
                    checkin = ("2000-01-01"),
                    checkout = ("2000-01-01")
                },
                additionalneeds = "Nista"
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PutRequestBuilder()
                .WithUrl(_url)
                .WithObjectBody(objectBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(200, (int)response.StatusCode);
            //response returns an error message.
        }

        [Test]
        public void InvalidDataTypeTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = "Neko",
                lastname = "Nesto",
                totalprice = 1234,
                depositpaid = "???",
                bookingdates = new BookingDatesRequest()
                {
                    checkin = ("2000-01-01"),
                    checkout = ("2000-01-01")
                },
                additionalneeds = "Nista"
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PutRequestBuilder()
                .WithUrl(_url)
                .WithObjectBody(objectBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void EmptyParameterTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = "",
                lastname = "",
                totalprice = 1234,
                depositpaid = false,
                bookingdates = new BookingDatesRequest()
                {
                    checkin = ("2000-01-01"),
                    checkout = ("2000-01-01")
                },
                additionalneeds = "Nista"
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PutRequestBuilder()
                .WithUrl(_url)
                .WithObjectBody(objectBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                //Assert.IsNotEmpty(response.Data.reason);
                //Assert.NotZero(response.Data.errors.Count);
            });
        }

        [Test]
        public void NullParameterTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = null,
                lastname = "Nesto",
                totalprice = 1234,
                depositpaid = false,
                bookingdates = new BookingDatesRequest()
                {
                    checkin = ("2000-01-01"),
                    checkout = ("2000-01-01")
                },
                additionalneeds = "Nista"
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PutRequestBuilder()
                .WithUrl(_url)
                .WithObjectBody(objectBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void MissingParameterTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var objectBody = new CreateBookingRequest()
            {
                lastname = "Nesto",
                totalprice = 1234,
                depositpaid = false,
                bookingdates = new BookingDatesRequest()
                {
                    checkin = ("2000-01-01"),
                    checkout = ("2000-01-01")
                },
                additionalneeds = "Nista"
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PutRequestBuilder()
                .WithUrl(_url)
                .WithObjectBody(objectBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void InvalidDateParameterValueTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = "Neko",
                lastname = "Nesto",
                totalprice = 1234,
                depositpaid = false,
                bookingdates = new BookingDatesRequest()
                {
                    checkin = ("8000-01-01"),
                    checkout = ("2000-01-01")
                },
                additionalneeds = "Nista"
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PutRequestBuilder()
                .WithUrl(_url)
                .WithObjectBody(objectBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(200, (int)response.StatusCode);
            //response returns an error message.
        }

        [Test]
        public void InvalidDateParameterFormatTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = "Neko",
                lastname = "Nesto",
                totalprice = 1234,
                depositpaid = false,
                bookingdates = new BookingDatesRequest()
                {
                    checkin = ("20000-1-01"),
                    checkout = ("2000-01-01")
                },
                additionalneeds = "Nista"
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PutRequestBuilder()
                .WithUrl(_url)
                .WithObjectBody(objectBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        public string GetCreatedBookingId()
        {
            string _bookingUrl = "https://restful-booker.herokuapp.com/booking/";
            _client = new DefaultClient();


            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" }
            };
            var objectBody = new CreateBookingRequest()
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
            var request = new PostRequestBuilder()
                .WithUrl(_bookingUrl)
                .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(objectBody))
                .Build();
            var response = _client.GetClient().Execute<CreateBookingResponse>(request);


            _client.Dispose();
            return response.Data.bookingid.ToString();
        }

        public string GetToken()
        {
            string _createToken = "https://restful-booker.herokuapp.com/auth";
            _client = new DefaultClient();


            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" }
            };
            var authBody = new AuthRequest()
            {
                username = "admin",
                password = "password123"
            };
            var request = new PostRequestBuilder()
                .WithUrl(_createToken)
                .WithHeaders(headers)
                .WithObjectBody(authBody)
                .Build();
            var response = _client.GetClient()
                .Execute<AuthResponse>(request);


            _client.Dispose();
            return response.Data.token;
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }
    }
}
