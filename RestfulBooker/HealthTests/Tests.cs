using Configuration;
using Configuration.APIRequest;
using Configuration.Client;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestfulBooker.HealthTests
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

        IClient _client;
        public string _url = "https://restful-booker.herokuapp.com/ping";

        [SetUp]
        public void SetUp()
        {
            _client = new DefaultClient();
        }

        [Test]
        public void SuccessfulTest()
        {
            var request = new GetRequestBuilder()
                .WithUrl(_url)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual((int)response.StatusCode, 200);
        }


        //These tests that check for unavailable service most likely aren't necessery, for these to "pass"
        //the service itself must not be running, in this case, all other tests would fail.
        [Test]
        public void UnsuccessfulTest()
        {
            var request = new GetRequestBuilder()
                .WithUrl(_url)
                .Build();
            var response = _client.GetClient()
                .Execute(request);


            Assert.AreEqual((int)response.StatusCode, 503);
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
        }

    }
}
