using Configuration;
using Configuration.APIRequest;
using Configuration.Client;
using Newtonsoft.Json;
using NUnit.Framework;
using RestfulBooker.CreateBookingTests;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBooker.GetBookingIdsTests
{
    [TestFixture]
    internal class Tests
    {
        //REFAKTORISI OVO SVE, pogledaj kako si uradio updatebookingtests

        public IClient _client;
        public string _url = "https://restful-booker.herokuapp.com/booking";

        [SetUp]
        public void SetUp()
        {
            _client = new DefaultClient();
        }

        //Doddaj testove za querey parametere, koristi createbooking kako bi mogao vrsit pretragu, za podatke request
        //koristi neke random generisane kako se ne bi ponavljaji

        [Test]
        public void SuccessfulTest()
        {
            var request = new GetRequestBuilder()
                .WithUrl(_url)
                .Build();
            var response = _client.GetClient()
                .Execute<List<GetBookingIdsResponse>>(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.NotZero(response.Data[0].bookingid);
            });
        }

        [Test]
        public void SuccessfulByFirstNameTest()
        {
            var id = GetCreatedBookingId();


            var queryParameter = new Dictionary<string, string>()
            {
                {"firstname",objectBody.firstname }
            };
            var getBookingIdsRequest = new GetRequestBuilder()
                .WithUrl(_url)
                .WithQueryParameters(queryParameter)
                .Build();
            var getBookingIdsResponse = _client.GetClient()
                .Execute<List<GetBookingIdsResponse>>(getBookingIdsRequest);


            var found = false;
            foreach (var res in getBookingIdsResponse.Data)
            {
                if (res.bookingid == id)
                {
                    found = true;
                }
            }


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)getBookingIdsResponse.StatusCode);
                Assert.IsTrue(found);
            });
        }

        [Test]
        public void SuccessfulByLastNameTest()
        {
            var id = GetCreatedBookingId();


            var queryParameter = new Dictionary<string, string>()
            {
                {"lastname",objectBody.lastname }
            };
            var getBookingIdsRequest = new GetRequestBuilder()
                .WithUrl(_url)
                .WithQueryParameters(queryParameter)
                .Build();
            var getBookingIdsResponse = _client.GetClient()
                .Execute<List<GetBookingIdsResponse>>(getBookingIdsRequest);


            var found = false;
            foreach (var res in getBookingIdsResponse.Data)
            {
                if (res.bookingid == id)
                {
                    found = true;
                }
            }


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)getBookingIdsResponse.StatusCode);
                Assert.IsTrue(found);
            });
        }

        [Test]
        public void SuccessfulByCheckInTest()
        {
            var id = GetCreatedBookingId();


            var queryParameter = new Dictionary<string, string>()
            {
                {"checkin",objectBody.bookingdates.checkin }
            };
            var getBookingIdsRequest = new GetRequestBuilder()
                .WithUrl(_url)
                .WithQueryParameters(queryParameter)
                .Build();
            var getBookingIdsResponse = _client.GetClient()
                .Execute<List<GetBookingIdsResponse>>(getBookingIdsRequest);


            var found = false;
            foreach (var res in getBookingIdsResponse.Data)
            {
                if (res.bookingid == id)
                {
                    found = true;
                }
            }


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)getBookingIdsResponse.StatusCode);
                Assert.IsTrue(found);
            });
        }

        [Test]
        public void SuccessfulByCheckOutTest()
        {
            var id = GetCreatedBookingId();


            var queryParameter = new Dictionary<string, string>()
            {
                {"checkout",objectBody.bookingdates.checkout }
            };
            var getBookingIdsRequest = new GetRequestBuilder()
                .WithUrl(_url)
                .WithQueryParameters(queryParameter)
                .Build();
            var getBookingIdsResponse = _client.GetClient()
                .Execute<List<GetBookingIdsResponse>>(getBookingIdsRequest);

            
            var found = false;
            foreach (var res in getBookingIdsResponse.Data)
            {
                if (res.bookingid ==id)
                {
                    found = true;
                }
            }


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)getBookingIdsResponse.StatusCode);
                Assert.IsTrue(found);
            });
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

        public int GetCreatedBookingId()
        {
            _client = new DefaultClient();


            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" },
                {"Accept","application/json"}
            };
            var request = new PostRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithObjectBody(objectBody)
                .Build();
            var response = _client.GetClient().Execute<CreateBookingResponse>(request);


            _client.Dispose();
            return response.Data.bookingid;
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

    }
}
