using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.AddIns;

namespace Background_Control
{
    internal class BackgroundMethods
    {
        public StartStopOperation WeatherLoadData (IProject activeProject)
        {
            return activeProject.AddInContext.RunWizard("Weather_LoadData.Weather_LoadData");
        }

        public StartStopOperation SunriseLoadData(IProject activeProject)
        {
            return activeProject.AddInContext.RunWizard("Weather_Sunrise.Weather_Sunrise");
        }

        internal StartStopOperation RadiationLoadData(IProject activeProject)
        {
            return activeProject.AddInContext.RunWizard("Weather_Radiation.Weather_Radiation");
        }
    }
}
