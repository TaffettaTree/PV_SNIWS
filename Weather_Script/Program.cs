using System;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Weather;
using System.Windows.Forms;

namespace Weather
{
    public class GetWeather
    {
        //latitude = 50.283,
        //longtitude = 18.666
        private double lattitude;
        private double longtitude;

        public GetWeather(double lattitude, double longtitude)
        {
            this.lattitude = lattitude;
            this.longtitude = longtitude;
            Dumb();

        }
        public GetWeather() 
        {
            this.lattitude = 50.292;
            this.longtitude = 18.666;
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
            this.lattitude = lat;
            this.longtitude = lon;  
        }
        
        /// <summary>
        /// loads weather from default tingywingy
        /// </summary>
        /// <returns>ResponseData object</returns>
        public ResponseData Load()
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
            var requestData = new RequestData
            {
                Latitude = this.lattitude,
                Longtitude = this.longtitude
            };

            //============================
            //==API settings !IMPORTANT!==
            //============================

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.met.no/weatherapi/locationforecast/2.0/");

            client.DefaultRequestHeaders.UserAgent.ParseAdd("PVsim");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("(+github.com/TaffettaTree/PV_sim)");


            // Request
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = "compact?lat=" + requestData.Latitude + "&lon=" + requestData.Longtitude;
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
            ResponseData returnVal = null;
            try
            {   
                var responseData = JsonConvert.DeserializeObject<ResponseData>(responseContent, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
                returnVal = responseData;
            }
            catch (Exception ex)
            {
                throw new Exception("Json: " + ex.Message);
            }
            return returnVal;

            
        }
        /// <summary>
        /// loads weather from params
        /// </summary>
        /// </summary>
        /// <param name="lat"> lattitude of your place</param>
        /// <param name="lon"> longtitude of your place</param>
        /// <returns>ResponseData object</returns>
        public static ResponseData Load(double lat, double lon)
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
            var requestData = new RequestData
            {
                Latitude = lat,
                Longtitude = lon
            };
            var responseData = new ResponseData();

            //============================
            //==API settings !IMPORTANT!==
            //============================

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.met.no/weatherapi/locationforecast/2.0/");

            client.DefaultRequestHeaders.UserAgent.ParseAdd("PVsim");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("(+github.com/TaffettaTree/PV_sim)");


            // Request
            var request = "compact?lat=" + requestData.Latitude + "&lon=" + requestData.Longtitude;
            var response = new HttpResponseMessage();
            try
            {
                response = client.GetAsync(request).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            var responseContent = response.Content.ReadAsStringAsync().Result;
            try
            {
                responseData = JsonConvert.DeserializeObject<ResponseData>(responseContent);
            }
            catch (Exception ex)
            {
                throw new Exception("Json" + ex.Message);
            }
            return responseData;

        }
    }
    
}

