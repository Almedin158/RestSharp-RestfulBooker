using Configuration;
using Configuration.APIRequest;
using Configuration.Client;
using NUnit.Framework;
using RestSharp;

namespace RestfulBooker.LoginTests
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
        public string _url = "https://restful-booker.herokuapp.com/auth";

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
                {"Content-Type","application/json" }
            };
            var authBody = new AuthRequest()
            {
                username = "admin",
                password = "password123"
            };
            var request = new PostRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithObjectBody(authBody)
                .Build();
            var response = _client.GetClient()
                .Execute<AuthResponse>(request);


            List<int> statusCodes = new List<int>()
            {200,201};

            Assert.Multiple(() =>
            {
                Assert.IsTrue(statusCodes.Contains((int)response.StatusCode));
                Assert.IsNotEmpty(response.Data.token);
            });
        }

        [Test]
        public void UnsuccessfulTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" }
            };
            var authBody = new AuthRequest()
            {
                username = "incorrectUsername",
                password = "incorrectPassword"
            };
            var request = new PostRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithObjectBody(authBody)
                .Build();
            var response = _client.GetClient()
                .Execute<AuthResponse>(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.AreEqual(response.Data.reason, "Bad credentials");
                //Assert.NotZero(response.Data.errors.Count);
            }); 
        }

        [Test]
        public void MissingParameterTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" }
            };
            var authBody = new AuthRequest()
            {
                password = "password123"
            };
            var request = new PostRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithObjectBody(authBody)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(400,(int)response.StatusCode);
            });
        }

        [Test]
        public void EmptyParameterTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" }
            };
            var authBody = new AuthRequest()
            {
                username="",
                password = "password123"
            };
            var request = new PostRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithObjectBody(authBody)
                .Build();
            var response = _client.GetClient()
                .Execute<AuthResponse>(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(200, (int)response.StatusCode);
                Assert.AreEqual(response.Data.reason, "Bad credentials");
                //Assert.NotZero(response.Data.errors.Count);
            });
        }

        [Test]
        public void NullParameterTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" }
            };
            var authBody = new AuthRequest()
            {
                username = null,
                password = "password123"
            };
            var request = new PostRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithObjectBody(authBody)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual(400, (int)response.StatusCode);
        }

        [Test]
        public void InvalidDataTypeTest()
        {
            var headers = new Dictionary<string, string>()
            {
                {"Content-Type","application/json" }
            };
            var authBody = new AuthRequest()
            {
                username = false,
                password = "password123"
            };
            var request = new PostRequestBuilder()
                .WithUrl(_url)
                .WithHeaders(headers)
                .WithObjectBody(authBody)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.Multiple(() =>
            {
                Assert.AreEqual(400, (int)response.StatusCode);
            });
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }
    }
}
