using MyHelloWorld.Application;
using MyHelloWorld.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyHelloWorld
{
    
    public class HelloWorld : IHelloWorld
    {
        private readonly IHelloWorldWebService helloWorldWebService;

        public void writeHelloWorld()
        {
            Console.WriteLine("Hello World");
        }
        public HelloWorld(IHelloWorldWebService helloWorldWebService)
        {
            this.helloWorldWebService = helloWorldWebService;
            
        }
        public HelloWorld()
        {
           ;

        }

        public void Run(string[] arguments)
        {
            // Get data
            var data = this.helloWorldWebService.GetData();

        }

        public static void Main()
        {
            HelloWorld hw = new HelloWorld();

            hw.writeHelloWorld();

            Console.ReadLine();
        }
    }
}
