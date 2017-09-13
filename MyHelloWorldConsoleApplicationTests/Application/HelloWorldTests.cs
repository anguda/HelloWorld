using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyHelloWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using MyHelloWorld.Service;
using MyHelloWorld.Application;
using HelloWorldInfrastructure.Models;

namespace MyHelloWorld.Tests
{
    [TestFixture]
    public class HelloWorldConsoleAppUnitTests
    {
        /// <summary>
        ///     The list of log messages set by calling classes
        /// </summary>
        private List<string> logMessageList;

        /// <summary>
        ///     The list of exceptions set by calling classes
        /// </summary>
        private List<Exception> exceptionList;

        /// <summary>
        ///     The list of other properties set by calling classes
        /// </summary>
        private List<object> otherPropertiesList;

        /// <summary>
        ///     The mocked Hello World Web Service
        /// </summary>
        private Mock<IHelloWorldWebService> helloWorldWebServiceMock;

       
        private HelloWorld helloWorldConsoleApp;

        /// <summary>
        ///     Initialize the test fixture (runs one time)
        /// </summary>
        [TestFixtureSetUp]
        public void InitTestSuite()
        {
            // Instantiate lists
            this.logMessageList = new List<string>();
            this.exceptionList = new List<Exception>();
            this.otherPropertiesList = new List<object>();

            // Setup mocked dependencies
            this.helloWorldWebServiceMock = new Mock<IHelloWorldWebService>();
            
            // Create object to test
            this.helloWorldConsoleApp = new HelloWorld(this.helloWorldWebServiceMock.Object);
        }
        

        #region Run Tests
        /// <summary>
        ///     Tests the class's Run method for success when normal data was found
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNormalDataSuccess()
        {
            const string Data = "Hello World!";

            // Create return models for dependencies
            var data = GetSampleTodaysData(Data);

            // Set up dependencies
            this.helloWorldWebServiceMock.Setup(m => m.GetData()).Returns(data);

            // Call the method to test
            this.helloWorldConsoleApp.Run(null);

            // Check values
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], Data);
        }

        /// <summary>
        ///     Tests the class's Run method for success when null data was found
        /// </summary>
        [Test]
        public void UnitTestHelloWorldConsoleAppRunNullDataSuccess()
        {
            // Set up dependencies
            this.helloWorldWebServiceMock.Setup(m => m.GetData()).Returns((HelloData)null);

            // Call the method to test
            this.helloWorldConsoleApp.Run(null);

            // Check values
            Assert.AreEqual(this.logMessageList.Count, 1);
            Assert.AreEqual(this.logMessageList[0], "No data was found!");
        }
        #endregion

        #region Helper Methods
        /// <summary>
        ///     Gets a sample TodaysData model
        /// </summary>
        /// <param name="data">The data</param>
        /// <returns>A sample TodaysData model</returns>
        private static HelloData GetSampleTodaysData(string data)
        {
            return new HelloData { Data = data };
        }
        #endregion
    }
}