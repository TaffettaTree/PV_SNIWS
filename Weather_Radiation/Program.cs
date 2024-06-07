using System;
using System.Net;
using System.Net.Http;
using System.Windows.Forms;
using System.Linq;

namespace Weather_Radiation
{
    public class GetRadiation
    {
        //latitude = 50.283,
        //longtitude = 18.666
        private double _lattitude = 50.283;
        private double _longtitude = 18.666;

        public GetRadiation(double lattitude, double longtitude)
        {
            this._lattitude = lattitude;
            this._longtitude = longtitude;
            Dumb();
        }
        public GetRadiation()
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
        public int[] Load(DateTime date)
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

            var _nowString = "";
            var _laterString = "";


            //============================
            //==API settings !IMPORTANT!==
            //============================

            var client = new HttpClient();
            client.BaseAddress = new Uri("https://re.jrc.ec.europa.eu/api/");

            client.DefaultRequestHeaders.UserAgent.ParseAdd("PVsim");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("(+github.com/TaffettaTree/PV_sim)");


            // Request
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var request = "tmy?lat=" + _lattitude + "&lon=" + _longtitude +
                "&outputformat=json";

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
                var occurance = date.ToString("MMdd:HH");
                responseContent = responseContent.Substring(responseContent.IndexOf(occurance),500);
                responseContent = responseContent.Substring(responseContent.IndexOf("G(h)"));
                responseContent = responseContent.Substring(6);

                _nowString = responseContent.Substring(0,responseContent.IndexOf(','));

                responseContent = responseContent.Substring(responseContent.IndexOf("G(h)"));
                responseContent = responseContent.Substring(6);

                _laterString = responseContent.Substring(0, responseContent.IndexOf(','));

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(request);
                throw new Exception(ex.Message);
            }

            int[] test = { Convert.ToInt32(double.Parse(_nowString)), Convert.ToInt32(double.Parse(_laterString)) };
            return test ;

        }
    }
}
