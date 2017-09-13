using HelloWorldInfrastructure.Models;

namespace HelloWorldInfrastructure.Mappers
{
    public interface IHelloWorldMapper
    {
        /// <summary>
        ///     Maps a string to a TodaysData model
        /// </summary>
        /// <param name="input">The input</param>
        /// <returns>A TodaysData model</returns>
        HelloData StringToTodaysData(string input);
    }
}