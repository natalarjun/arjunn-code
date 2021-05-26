using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;

namespace APISolution
{
    public class Tests
    {
        readonly HttpClient client = new HttpClient();

        [SetUp]
        public void Setup()
        {
           // client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts/"); 
        }

        [Test]
        public void Verify_GetDataWithValidID_ReturnSuccess()
        {
            HttpResponseMessage response = client.GetAsync("https://jsonplaceholder.typicode.com/posts/1").Result;
            var responseData = response.Content.ReadAsStringAsync().Result;
            Email content =  JsonConvert.DeserializeObject<Email>(responseData);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotNull(content.id);
        }

        [Test]
        public void Verify_GetDataWithInvalidID_ReturnFailure()
        {
            HttpResponseMessage response = client.GetAsync("https://jsonplaceholder.typicode.com/posts/a").Result;
            var responseData = response.Content.ReadAsStringAsync().Result;
            Email content = JsonConvert.DeserializeObject<Email>(responseData);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.IsTrue(content.id.GetType().Equals(typeof(int)));
            
        }

        [Test]
        public void Verify_GetSingleTodoWithValidId_ReturnSuccess()
        {
            HttpResponseMessage response = client.GetAsync("https://jsonplaceholder.typicode.com/todos/1").Result;
            var responseData = response.Content.ReadAsStringAsync().Result;
            ToDo content = JsonConvert.DeserializeObject<ToDo>(responseData);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsTrue(content.id.Equals(1));
            Assert.IsTrue(content.completed.GetType().Equals(typeof(bool)));

        }
    }

}