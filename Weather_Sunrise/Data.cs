using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather_Sunrise
{
    public class Data
    {
        public DateTime _sunset;
        public DateTime _sunrise;
        public Data() 
        {
            _sunset = DateTime.MaxValue;
            _sunrise = DateTime.MinValue;
        }
        
        public void Set(DateTime sunrise,DateTime sunset)
        {
            this._sunset = sunset;
            this._sunrise = sunrise;
        }
    }
}
