using Configuration;
using Configuration.APIRequest;
using Configuration.Client;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using RestSharp.Serializers;

namespace RestfulBooker.CreateBookingTests
{
    [TestFixture]
    internal class Tests
    {
        //I made three assumptions here:
        //1. The entire request body is required and the backend service tests the entire request body against a schema
        //(meaning I do not need to check each and every parameter, test cases for a single parameter are enough)
        //2. Invalid request body, such as an empty,null,invalid data type required fields are handled with a 400 status code bad request,
        //instead of a 200 status code ok with an error message, I have seen both options used.
        //3. Invalid request bodies where all parameters are sent but the parameter value is incorrect are handled with
        //a 200 status code ok response containting an error message. I have added a "reason" and a list of errors
        //as response parameters, this would be a way i would image a server handling these issues and notifying the client.


        public IClient _client;
        public string _url = "https://restful-booker.herokuapp.com/booking";

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
                {"Accept","application/json"}
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
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(objectBody))
                .Build();
            var response = _client.GetClient().Execute<CreateBookingResponse>(request);

            
            var statusCodes = new List<int>()
            {
                200,201
            };


            Assert.Multiple(() =>
            {
                Assert.IsTrue(statusCodes.Contains((int)response.StatusCode));
                Assert.NotZero(response.Data.bookingid);
                Assert.AreEqual(response.Data.booking.firstname, objectBody.firstname);
                Assert.AreEqual(response.Data.booking.lastname, objectBody.lastname);
                Assert.AreEqual(response.Data.booking.totalprice, objectBody.totalprice);
                Assert.AreEqual(response.Data.booking.depositpaid, objectBody.depositpaid);
                Assert.AreEqual(response.Data.booking.additionalneeds, objectBody.additionalneeds);
                Assert.AreEqual(response.Data.booking.bookingdates.checkin, objectBody.bookingdates.checkin);
                Assert.AreEqual(response.Data.booking.bookingdates.checkout, objectBody.bookingdates.checkout);
            });
        }

        [Test]
        public void ResponseIsJsonTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json"}
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
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(objectBody))
                .Build();
            var response = _client.GetClient().Execute(request);

            
            Assert.AreEqual(response.ContentType, ContentType.Json);
        }

        [Test]
        public void ResponseIsXmlTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/xml"}
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
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(objectBody))
                .Build();
            var response = _client.GetClient().Execute(request);


            Assert.AreEqual(response.ContentType, ContentType.Xml);
        }

        [Test]
        public void InvalidTotalPriceTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json"}
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = "MadeUp",
                lastname = "AlsoMadeUp",
                totalprice = -1,
                depositpaid = true,
                bookingdates = new BookingDatesRequest()
                {
                    checkin = DateTime.Now.ToString("yyyy'-'MM'-'dd"),
                    checkout = DateTime.Now.ToString("yyyy'-'MM'-'dd")
                },
                additionalneeds = "Dorucak"
            };
            var request = new PostRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(objectBody))
                .Build();
            var response = _client.GetClient().Execute(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                //Assert.AreEqual(422, (int)response.StatusCode);
                //Assert.IsNotEmpty(response.Data.reason);
                //Assert.NotZero(response.Data.errors.Count);
            });
        }

        [Test]
        public void InvalidDataTypeTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json"}
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = "MadeUp",
                lastname = "AlsoMadeUp",
                totalprice = 1508,
                depositpaid = "???",
                bookingdates = new BookingDatesRequest()
                {
                    checkin = DateTime.Now.ToString("yyyy'-'MM'-'dd"),
                    checkout = DateTime.Now.ToString("yyyy'-'MM'-'dd")
                },
                additionalneeds = "Dorucak"
            };
            var request = new PostRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(objectBody))
                .Build();
            var response = _client.GetClient().Execute(request);


            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void EmptyParameterTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json"}
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = "",
                lastname = "",
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
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(objectBody))
                .Build();
            var response = _client.GetClient().Execute<CreateBookingResponse>(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200,(int)response.StatusCode);
                //Assert.IsNotEmpty(response.Data.reason);
                //Assert.NotZero(response.Data.errors.Count);
            });
        }

        [Test]
        public void NullParameterTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Accept","application/json"}
            };
            var objectBody = new CreateBookingRequest()
            {
                firstname = null,
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
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(objectBody))
                .Build();
            var response = _client.GetClient().Execute(request);

            
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void MissingParameterTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json"}
            };
            var objectBody = new CreateBookingRequest()
            {
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
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(objectBody))
                .Build();
            var response = _client.GetClient().Execute(request);

            
            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void InvalidDateParametersValueTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json"}
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
                    checkout = "8000-12-12"
                },
                additionalneeds = "Dorucak"
            };
            var request = new PostRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(objectBody))
                .Build();
            var response = _client.GetClient().Execute(request);


            Assert.AreEqual(400, (int)response.StatusCode);
            //Could also be handled with a 200 status code ok and an error message.
        }

        [Test]
        public void InvalidDateParametersFormatTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json"}
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
                    checkout = "2000-112-12"
                },
                additionalneeds = "Dorucak"
            };
            var request = new PostRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithJsonBody(JsonConvert.SerializeObject(objectBody))
                .Build();
            var response = _client.GetClient().Execute(request);


            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }
    }
}
