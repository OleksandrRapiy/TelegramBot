using System.Threading.Tasks;

namespace TelegramBotIBWT.Services
{
    interface IWeatherService
    {
        Task<CurrentWeather> GetWeatherAsync(float lat, float lon);
    }
}