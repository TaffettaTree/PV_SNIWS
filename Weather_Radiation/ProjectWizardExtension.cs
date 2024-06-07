using Scada.AddIn.Contracts;
using System;

namespace Weather_Radiation
{
    /// <summary>
    /// Description of Project Wizard Extension.
    /// </summary>
    [AddInExtension("Weather_Radiation", "Load Radiation for Gliwice", Id = "Weather_Radiation")]
    public class ProjectWizardExtension : IProjectWizardExtension
    {
        #region IProjectWizardExtension implementation
        GetRadiation radiation = new GetRadiation();
        public void Run(IProject context, IBehavior behavior)
        {
            // enter your code which should be executed on triggering the function "Execute Project Wizard Extension" in the SCADA Service Engine
            IProject activeProject = context;
            int[] data = { -1, -1 };
            try
            {
                data = radiation.Load(DateTime.Now);
                activeProject.VariableCollection["PV_Panel[0].RADIATION[0]"].SetValue(0, data[0]);
                activeProject.VariableCollection["PV_Panel[0].RADIATION[1]"].SetValue(0, data[1]);
                activeProject.VariableCollection["PV_Panel[1].RADIATION[0]"].SetValue(0, data[0]);
                activeProject.VariableCollection["PV_Panel[1].RADIATION[1]"].SetValue(0, data[1]);
                activeProject.VariableCollection["PV_Panel[2].RADIATION[0]"].SetValue(0, data[0]);
                activeProject.VariableCollection["PV_Panel[2].RADIATION[1]"].SetValue(0, data[1]);


                activeProject.VariableCollection["PV_Panel[0].RADIATION_NEW"].SetValue(0, 1);
                activeProject.VariableCollection["PV_Panel[1].RADIATION_NEW"].SetValue(0, 1);
                activeProject.VariableCollection["PV_Panel[2].RADIATION_NEW"].SetValue(0, 1);

            }
            catch (Exception ex)
            {
                activeProject.VariableCollection["Control_AML_Switch"].SetValue(0, 1);
                activeProject.VariableCollection["Control_AML_Text"].SetValue(0, "Radiation Wizard: " + ex.Message);
            }
        }

        #endregion
    }

}