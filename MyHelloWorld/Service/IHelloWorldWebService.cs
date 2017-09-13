using HelloWorldInfrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHelloWorld.Service
{
    public interface IHelloWorldWebService
    {
            /// <returns>A TodaysData model containing today's data</returns>
            HelloData GetData();
    }
}
