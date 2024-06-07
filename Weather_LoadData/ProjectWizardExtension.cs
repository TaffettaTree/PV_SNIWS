using Scada.AddIn.Contracts;
using System;
using System.Windows.Forms;
using Weather;
using Newtonsoft.Json;
using Mono.Addins;

namespace Weather_LoadData
{
    /// <summary>
    /// Description of Project Wizard Extension.
    /// </summary>
    [AddInExtension("Get_Forecast", "Load weather forecast for Gliwice",Id = "Weather_LoadData")]
    
    
    public class ProjectWizardExtension : IProjectWizardExtension
    {
        #region IProjectWizardExtension implementation
        IProject activeProject = null;
        public async void Run(IProject context, IBehavior behavior)
        {
            // enter your code which should be executed on triggering the function "Execute Project Wizard Extension" in the SCADA Service Engine
                       
            GetWeather getWeather = new Weather.GetWeather();
            ResponseData responseData = new ResponseData();
            try
            {
                activeProject = context;
                DoStuff(ref getWeather, ref responseData);
            }
            catch (Exception ex)
            {
                activeProject.VariableCollection["Control_AML_Switch"].SetValue(0, 1);
                activeProject.VariableCollection["Control_AML_Text"].SetValue(0, "Weather Wizard: " + ex.Message);
            }
        }

        private void DoStuff(ref GetWeather getWeather, ref ResponseData responseData)
        {
            try
            {
                responseData = getWeather.Load();
                ConvertData(responseData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ConvertData(ResponseData _responseData)
        {
            try
            {
                var _weatherData = _responseData.properties.timeseries;

                var hourVar = "Weather_Tisr_";
                var dayOne = _weatherData[0].time.Date;
                var dayCurrent = dayOne;
                var day = 0;
                for (int i = 1; i <= 82; i++)
                {
                    activeProject.VariableCollection[hourVar + i + ".TIME"].SetValue(0,_weatherData[i - 1].time.Hour + Convert.ToInt32(activeProject.VariableCollection["Control_Weather_TimeZone"].GetValue(0)));
                    activeProject.VariableCollection[hourVar + i + ".DATE"].SetValue(0, _weatherData[i - 1].time.Date.ToShortDateString());
                    if (_weatherData[i - 1].time.Date != dayCurrent)
                    {
                        dayCurrent = dayCurrent.AddDays(1);
                        day++;
                    }
                    activeProject.VariableCollection[hourVar + i + ".DAY"].SetValue(0,day);
                    if (_weatherData[i - 1].data.next_1_hours != null)
                    {
                        activeProject.VariableCollection[hourVar + i + ".SYMBOL"].SetValue(0, Methods.WeatherDictionary(_weatherData[i - 1].data.next_1_hours.summary.symbol_code));
                        activeProject.VariableCollection[hourVar + i + ".RAIN"].SetValue(0, (double)_weatherData[i - 1].data.next_1_hours.details.precipitation_amount);
                        activeProject.VariableCollection[hourVar + i + ".TYPE"].SetValue(0, 1);
                    }
                    else
                    {
                        activeProject.VariableCollection[hourVar + i + ".SYMBOL"].SetValue(0, Methods.WeatherDictionary(_weatherData[i - 1].data.next_6_hours.summary.symbol_code));
                        activeProject.VariableCollection[hourVar + i + ".RAIN"].SetValue(0, (double)_weatherData[i - 1].data.next_6_hours.details.precipitation_amount);
                        activeProject.VariableCollection[hourVar + i + ".TYPE"].SetValue(0, 6);
                    }
                    activeProject.VariableCollection[hourVar + i + ".TEMPERATURE"].SetValue(0, (int)Math.Round(_weatherData[i - 1].data.instant.details.air_temperature));
                    activeProject.VariableCollection[hourVar + i + ".CFRACTION"].SetValue(0, (double)_weatherData[i - 1].data.instant.details.cloud_area_fraction);
                    
                    
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        #endregion
    }

}