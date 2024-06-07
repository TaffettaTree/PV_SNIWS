using System;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using System.Linq;

namespace Weather_Sunrise
{
    public class GetSunrise
    {
        //latitude = 50.283,
        //longtitude = 18.666
        public Data data = new Data();
        private double _lattitude;
        private double _longtitude;

        public GetSunrise(double lattitude, double longtitude)
        {
            this._lattitude = lattitude;
            this._longtitude = longtitude;
            Dumb();
        }
        public GetSunrise()
        {
            this._lattitude = 50.292;
            this._longtitude = 18.666;
            Dumb();
        }

        private void Dumb()
        {
            Application.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        }

        /// <summary>
        /// sets default location
        /// </summary>
        /// <param name="lat"> lattitude of your place</param>
        /// <param name="lon"> longtitude of your place</param>
        public void SetLocation(double lat, double lon)
        {
            this._lattitude = lat;
            this._longtitude = lon;
        }

        /// <summary>
        /// loads weather from default tingywingy
        /// </summary>
        /// <returns>ResponseData object</returns>
        public Data Load(DateTime date, string offset)
        {
            // proxy (może kiedyś)
            /*
            WebProxy proxy = new WebProxy
            {
                Address = new Uri("")

            };
            HttpClientHandler httpClientHandler = new HttpClientHandler()
            {
                Proxy = proxy
            };
            */

            // Init request to YR

            var _sunriseString = "";
            var _sunsetString = "";


            //============================
            //==API settings !IMPORTANT!==
            //============================

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.met.no/weatherapi/sunrise/3.0/");

            client.DefaultRequestHeaders.UserAgent.ParseAdd("PVsim");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("(+github.com/TaffettaTree/PV_sim)");


            // Request
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = "sun?lat=" + _lattitude + "&lon=" + _longtitude +
                "&date=" + date.ToString("yyyy-MM-dd") + "&offset=" + offset;

            var response = new HttpResponseMessage();
            try
            {
                response = client.GetAsync(request).Result;
            }
            catch (Exception ex)
            {
                throw new Exception("Request Failed : " + ex);
            }
            var responseContent = response.Content.ReadAsStringAsync().Result;
            try
            {
                responseContent = responseContent.Substring(responseContent.IndexOf("sunrise"));
                responseContent = responseContent.Substring(responseContent.IndexOf("time"));
                responseContent = responseContent.Substring(7);

                _sunriseString = responseContent.Substring(0,22);

                responseContent = responseContent.Substring(responseContent.IndexOf("sunset"));
                responseContent = responseContent.Substring(responseContent.IndexOf("time"));
                responseContent = responseContent.Substring(7);

                _sunsetString = responseContent.Substring(0, 22);

                data.Set(DateTime.Parse(_sunriseString), DateTime.Parse(_sunsetString));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return data;

        }
    }
}
