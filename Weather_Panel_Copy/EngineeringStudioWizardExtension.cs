using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.ScreenElement;
using Scada.AddIn.Contracts.SmartObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weather_Panel_Copy
{
    /// <summary>
    /// Description of Engineering Studio Wizard Extension.
    /// </summary>
    [AddInExtension("Weather_Panel_Creator", "This script makes smart objects", "Weather_Add-In")]
    public class EngineeringStudioWizardExtension : IEditorWizardExtension
    {
        #region IEditorWizardExtension implementation
        IProject activeProject = null;
        IScreenElementCollection elements = null;
        public void Run(IEditorApplication context, IBehavior behavior)
        {
            activeProject = context.Workspace.ActiveProject;
            elements = activeProject.ScreenCollection["Weather"].ScreenElementCollection;
                try 
                {
                    Create();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "\n\n" + e);
                }

        }
        public void Create ()
        {
            try
            {
                
                IScreenElement activeElement = null;
                for (int i = 1; i <= 82; i++)
                {
                    activeElement = elements.Create("Weather_Panel_" + i, ElementType.Symbol);
                    activeElement.SetDynamicProperty("LinkName", "Weather_Panel");
                    activeElement.SetDynamicProperty("StartY", 38 + (100 * (i - 1)));
                    activeElement.SetDynamicProperty("StartX", 9);
                    activeElement.SetDynamicProperty("Width", 600);
                    activeElement.SetDynamicProperty("Height", 100);
                    activeElement.SetDynamicProperty("SubstituteSource", "*1*");
                    activeElement.SetDynamicProperty("SubstituteDestination", i.ToString());
                    activeElement.SetDynamicProperty("PropSymbolNumber", i);

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }

}