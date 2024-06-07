using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.ScreenElement;
using System;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Test_Script
{
    /// <summary>
    /// Description of Engineering Studio Wizard Extension.
    /// </summary>
    [AddInExtension("Script_for_testing", "your Engineering Studio Wizard Extension Description", "Testing_functionality")]
    public class EngineeringStudioWizardExtension : IEditorWizardExtension
    {
        #region IEditorWizardExtension implementation

        public void Run(IEditorApplication context, IBehavior behavior)
        {
            // enter your code which should be executed when starting the SCADA Engineering Studio Wizard
            var element = context.Workspace.ActiveProject.ScreenCollection["Screen 0"].ScreenElementCollection["Weather_icon"];
            element.SetDynamicProperty("Width", 100);
            element.SetDynamicProperty("Height", 100);

            /* Edit combined element
            var element = context.Workspace.ActiveProject.ScreenCollection["Screen 0"].ScreenElementCollection["Weather_icon"];
            if (element != null)
            {
                
                try
                {
                    for (int i = 1; i <= 63; i++)
                    {
                        element.CreateDynamicProperty("States[" + i.ToString() + "]");
                        element.SetDynamicProperty("States[" + i.ToString() + "].Value", i);
                        element.SetDynamicProperty("States[" + i.ToString() + "].TextOrBitmap", "WEATHER_" + i.ToString() + ".PNG");
                        element.SetDynamicProperty("States[" + i.ToString() + "].ValueMask", 4294967295);

                    }
                    
                    MessageBox.Show("maybe?");
                } catch  (Exception ex)
                {
                    MessageBox.Show("error: " + ex.Message);
                }
                ;

            } else
            {
                MessageBox.Show("NULL");
            }
        }*/

            #endregion
        }
    }
}