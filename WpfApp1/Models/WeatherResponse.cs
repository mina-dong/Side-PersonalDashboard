using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    internal class WeatherResponse
    {
        //Open meteo 에서 중첩된 형식으로 응답하고 있어서 이와 같이 처리함
        public WeatherInfo current_weather { get; set; }

    }
}
