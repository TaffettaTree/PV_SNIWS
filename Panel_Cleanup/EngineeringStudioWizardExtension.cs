using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.SmartObject;
using System;

namespace Panel_Cleanup
{
    /// <summary>
    /// Description of Engineering Studio Wizard Extension.
    /// </summary>
    [AddInExtension("Panel_Cleanup", "This script makes smart objects", "Weather_Add-In")]
    public class EngineeringStudioWizardExtension : IEditorWizardExtension
    {
        #region IEditorWizardExtension implementation

        public void Run(IEditorApplication context, IBehavior behavior)
        {
            // enter your code which should be executed when starting the SCADA Engineering Studio Wizard
            
            for (int i = 2; i <= 86; i++)
            {
                context.Workspace.ActiveProject.SmartObjects.DeleteByName("Weather_Panel_" + i);
            }
        }

        #endregion
    }

}