using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather
{
    public class ResponseData
    {
        public Properties properties {  get; set; }
    }
    
    public class Properties
    {
        public Time[] timeseries { get; set; }
    }

    public class Time
    {
        public DateTime time {  get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public Instant instant { get; set; }
        public Next_1_Hours next_1_hours { get; set; }
        public Next_6_Hours next_6_hours { get; set; }
    }

    public class Instant
    {
        public Details details { get; set; }
    }

    public class Details
    {
        public float air_pressure_at_sea_level { get; set; }
        public float air_temperature { get; set; }
        public float cloud_area_fraction { get; set; }
        public float relative_humidity { get; set; }
        public float wind_from_direction { get; set; }
        public float wind_speed { get; set; }
        public float precipitation_amount { get; set; }
    }
    public class Summary
    {
        public string symbol_code { get; set; }
    }

    public class Next_1_Hours
    {
        public Summary summary { get; set; }
        public Details details { get; set; }
    }
    public class Next_6_Hours
    {
        public Summary summary { get; set; }
        public Details details { get; set; }
    }
}
