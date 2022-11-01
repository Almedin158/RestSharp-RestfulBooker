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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBooker.PartialUpdateBookingTests
{
    [TestFixture]
    internal class Tests
    {
        IClient _client;
        string _url = "https://restful-booker.herokuapp.com/booking/{id}";

        [SetUp]
        public void SetUp()
        {
            _client = new DefaultClient();
        }

        [Test]
        public void NoTokenTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" }
            };
            var jsonBody = @"{" + "\n" +
@"    ""firstname"" : ""Neko""" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(401, (int)response.StatusCode);
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
            var jsonBody = @"{" + "\n" +
@"    ""firstname"" : ""Neko""" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(401, (int)response.StatusCode);
        }

        [Test]
        public void SuccessfulFirstNameTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var jsonBody = @"{" + "\n" +
@"    ""firstname"" : ""Neko""" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute<GetBookingResponse>(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.AreEqual("Neko", response.Data.firstname);
            });
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
            var jsonBody = @"{" + "\n" +
@"    ""firstname"" : ""Neko""" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
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
            var jsonBody = @"{" + "\n" +
@"    ""firstname"" : ""Neko""" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(response.ContentType, ContentType.Xml);
        }

        [Test]
        public void SuccessfulLastNameTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var jsonBody = @"{" + "\n" +
@"    ""lastname"" : ""Nesto""" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute<GetBookingResponse>(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.AreEqual("Nesto", response.Data.lastname);
            });
        }

        [Test]
        public void SuccessfulTotalPriceTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var jsonBody = @"{" + "\n" +
@"    ""totalprice"" : ""130""" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute<GetBookingResponse>(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.AreEqual(130, response.Data.totalprice);
            });
        }

        [Test]
        public void SuccessfulDepositPaidTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var jsonBody = @"{" + "\n" +
@"    ""depositpaid"": false" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute<GetBookingResponse>(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.AreEqual(false, response.Data.depositpaid);
            });
        }

        [Test]
        public void SuccessfulCheckInTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var jsonBody = @"{" + "\n" +
@"    ""bookingdates"" : {" + "\n" +
@"        ""checkin"" : ""2020-01-01""" + "\n" +
@"    }" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute<GetBookingResponse>(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.AreEqual("2020-01-01", response.Data.bookingdates.checkin);
            });
        }

        [Test]
        public void SuccessfulCheckOutTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var jsonBody = @"{" + "\n" +
@"    ""bookingdates"" : {" + "\n" +
@"        ""checkout"" : ""2020-01-01""" + "\n" +
@"    }" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute<GetBookingResponse>(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.AreEqual("2020-01-01", response.Data.bookingdates.checkout);
            });
        }

        [Test]
        public void SuccessfulAdditionalNeedsTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json" },
                {"Cookie", "token="+GetToken() }
            };
            var jsonBody = @"{" + "\n" +
@"    ""additionalneeds"" : ""Nista""" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute<GetBookingResponse>(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.AreEqual("Nista", response.Data.additionalneeds);
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
            var jsonBody = @"{" + "\n" +
@"    ""totalprice"" : null" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
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
            var jsonBody = @"{" + "\n" +
@"    ""totalprice"" : """"" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(200, (int)response.StatusCode);
            //with an error message in the response
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
            var jsonBody = @"{" + "\n" +
@"    ""totalprice"" : -1" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(200, (int)response.StatusCode);
            //response returns an error message
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
            var jsonBody = @"{" + "\n" +
@"    ""bookingdates"" : {" + "\n" +
@"        ""checkin"" : ""20000-1-01""" + "\n" +
@"    }" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
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
            var jsonBody = @"{" + "\n" +
@"    ""bookingdates"" : {" + "\n" +
@"        ""checkin"" : ""8000-01-01""" + "\n" +
@"    }" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
                .WithHeaders(headers)
                .WithUrlSegments(urlSegments)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(200, (int)response.StatusCode);
            //resposne returns an error message
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
            var jsonBody = @"{" + "\n" +
@"    ""depositpaid"" : ""???""" + "\n" +
@"}";
            var urlSegments = new Dictionary<string, string>()
            {
                {"id", GetCreatedBookingId()}
            };


            var request = new PatchRequestBuilder()
                .WithUrl(_url)
                .WithJsonBody(jsonBody)
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
