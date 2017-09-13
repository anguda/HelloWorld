using HelloWorldInfrastructure.Models;

namespace HelloWorldInfrastructure.Mappers
{
    public class HelloWorldMapper : IHelloWorldMapper
    {
        /// <summary>
        ///     Maps a string to a TodaysData model
        /// </summary>
        /// <param name="input">The input</param>
        /// <returns>A TodaysData model</returns>
        public HelloData StringToTodaysData(string input)
        {
            return new HelloData { Data = input };
        }
    }
}