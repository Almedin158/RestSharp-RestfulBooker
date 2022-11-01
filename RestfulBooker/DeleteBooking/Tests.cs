using Configuration;
using Configuration.APIRequest;
using Configuration.Client;
using Newtonsoft.Json;
using NUnit.Framework;
using RestfulBooker.CreateBookingTests;
using RestfulBooker.GetBookingTests;
using RestfulBooker.LoginTests;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBooker.DeleteBooking
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
                {"Cookie", "token="+GetToken() }
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new DeleteRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            List<int> statusCodes = new List<int>() { 200, 202, 204 };

            Assert.Multiple(() =>
            {
                Assert.IsTrue(statusCodes.Contains((int)response.StatusCode));
            });
        }

        [Test]
        public void NonExistantIdTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", "4848466657"}
            };


            var request = new DeleteRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(204,(int)response.StatusCode);
            });
        }

        [Test]
        public void IncorrectTokenTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Cookie", "token=incorrectToken" }
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new DeleteRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(401,(int)response.StatusCode);
        }

        [Test]
        public void NoTokenTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" }
            };
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new DeleteRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(401, (int)response.StatusCode);
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
