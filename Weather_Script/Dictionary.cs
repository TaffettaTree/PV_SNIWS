using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{ 
    public class Methods
    {
        public static int WeatherDictionary(string s)
        {
            IDictionary<string, int> dic = new Dictionary<string, int>();
            string init = @"clearsky_day:clearsky_night:fair_day:fair_night:partlycloudy_day:partlycloudy_night:cloudy:rainshowers_day:rainshowers_night:rainshowersandthunder_day:rainshowersandthunder_night:sleetshowers_day:sleetshowers_night:snowshowers_day:snowshowers_night:rain:heavyrain:heavyrainandthunder:sleet:snow:snowandthunder:fog:sleetshowersandthunder_day:sleetshowersandthunder_night:snowshowersandthunder_day:snowshowersandthunder_night:rainandthunder:sleetandthunder:lightrainshowersandthunder_day:lightrainshowersandthunder_night:heavyrainshowersandthunder_day:heavyrainshowersandthunder_night:lightssleetshowersandthunder_day:lightssleetshowersandthunder_night:heavysleetshowersandthunder_day:heavysleetshowersandthunder_night:lightssnowshowersandthunder_day:lightssnowshowersandthunder_night:heavysnowshowersandthunder_day:heavysnowshowersandthunder_night:lightrainandthunder:lightsleetandthunder:heavysleetandthunder:lightsnowandthunder:heavysnowandthunder:lightrainshowers_day:lightrainshowers_night:heavyrainshowers_day:heavyrainshowers_night:lightsleetshowers_day:lightsleetshowers_night:heavysleetshowers_day:heavysleetshowers_night:lightsnowshowers_day:lightsnowshowers_night:heavysnowshowers_day:heavysnowshowers_night:lightrain:lightsleet:heavysleet:lightsnow:heavysnow";
            dic = init.Split(':').Select((x,i) => new { Key = i+1, Val = x }).ToDictionary( n => n.Val, n=> n.Key );
            int output =0;
            dic.TryGetValue(s,out output);
            return output;
        }
    }
}
