using Scada.AddIn.Contracts;
using System;
using System.Windows.Forms;

namespace Weather_Sunrise
{
    /// <summary>
    /// Description of Project Wizard Extension.
    /// </summary>
    [AddInExtension("Weather_Sunrise", "Load Sunrise & Sunset for Gliwice", Id = "Weather_Sunrise")]
    public class ProjectWizardExtension : IProjectWizardExtension
    {
        #region IProjectWizardExtension implementation
        private Data data = new Data();
        private GetSunrise getSunrise = new GetSunrise();
        IProject activeProject = null; 
        public void Run(IProject context, IBehavior behavior)
        {
            // enter your code which should be executed on triggering the function "Execute Project Wizard Extension" in the SCADA Service Engine
            activeProject = context;
            try
            {
                data = getSunrise.Load(DateTime.Today, "+01:00");
                activeProject.VariableCollection["Weather_Sunrise_Today.SUNRISE"].SetValue(0, data._sunrise.ToString("HH:mm"));
                activeProject.VariableCollection["Weather_Sunrise_Today.SUNSET"].SetValue(0, data._sunset.ToString("HH:mm"));
            }
            catch (Exception ex)
            {
                activeProject.VariableCollection["Control_AML_Switch"].SetValue(0, 1);
                activeProject.VariableCollection["Control_AML_Text"].SetValue(0, "Sunrise Wizard: " + ex.Message);
            }
        }

        #endregion
    }

}