using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    internal class WeatherService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        
        //추후 변경 고려해서 상수처리 - 우선 서울 기준으로 경도 위도 고정 형태
        private const string CITY = "서울";
        private const double LATITUDE = 37.55;
        private const double LONGITUDE = 127.0;

        public async Task<WeatherInfo> GetWeatherAsync()
        {
            string url =
                $"https://api.open-meteo.com/v1/forecast?latitude={LATITUDE}&longitude={LONGITUDE}&current_weather=true";

            string json = await _httpClient.GetStringAsync(url);

            WeatherResponse response =
                JsonConvert.DeserializeObject<WeatherResponse>(json);

            return response.current_weather;
        }

        public string GetWeatherDescription(int code)
        {
            switch (code)
            {
                case 0:
                    return "맑음";

                case 1:
                case 2:
                    return "구름 조금";

                case 3:
                    return "흐림";

                case 45:
                case 48:
                    return "안개";

                case 61:
                case 63:
                case 65:
                    return "비";

                case 71:
                case 73:
                case 75:
                    return "눈";

                case 95:
                    return "천둥번개";

                default:
                    return "알 수 없음";
            }
        }
    }
}