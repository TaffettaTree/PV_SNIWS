using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.License;
using Scada.AddIn.Contracts.Variable;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Weather_Var_Creator
{
    /// <summary>
    /// Description of Engineering Studio Wizard Extension.
    /// </summary>
    [AddInExtension("Weather_Var_Creator", "This script creates needed weather variables", "Weather_Add-In")]
    public class EngineeringStudioWizardExtension : IEditorWizardExtension
    {
        #region IEditorWizardExtension implementation
        IProject activeProject = null;
        Creator creator = null;
        List<IVariable> variables = null;
        public void Run(IEditorApplication context, IBehavior behavior)
        {
            // enter your code which should be executed when starting the SCADA Engineering Studio Wizard
            activeProject = context.Workspace.ActiveProject;
            creator = new Creator(activeProject);
             
            variables = new List<IVariable>();


            AddVariables();

            ChangeVariables();

            /* Code for array thingy
            activeProject.VariableCollection.CreateArrayVariable("Weather_data", activeProject.DriverCollection["Driver for internal variables"]
                , ChannelType.DriverVariable, activeProject.DataTypeCollection["REAL"], 1, 86, 6, 0, AddressingOption.AutomaticAddressing, false);
            */

        }
        public bool AddVariables()
        {
            try
            {
                var hourVar = "Weather_Tisr_";
                var driver = "Driver for internal variables";
                for (int i = 1; i <= 82; i++)
                {
                    variables.Add(creator.CreateVariable(hourVar + i , driver, ChannelType.DriverVariable, "WEATHER_DATA"));
                }
            }
            catch (Exception ex)
            {
                activeProject.Parent.Parent.DebugPrint("Exception thrown: " + ex.Message, DebugPrintStyle.Error);
            }
            return false;
        }

        public bool ChangeVariables()
        {
            try
            {
                IVariable temp = null;
                    foreach (IVariable i in variables)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                        i.ActivateSubItem(j);
                        i.GetSubItem(j).SetDynamicProperty("Remanenz", 2);
                        }
                    }
                    

            }
            catch (Exception ex)
            {
                activeProject.Parent.Parent.DebugPrint("Exception thrown: " + ex.Message, DebugPrintStyle.Error);
            }
            return false;
        }
        #endregion
        //public bool AddVariables()
        //{
        //    try
        //    {
        //        var hourVar = "Weather_Tisr_";
        //        var driver = "Driver for internal variables";
        //        for (int i = 1;  i <= 82; i++)
        //        {
        //            variables.Add(new IVariable[8]);
        //            variables[i-1][0] = creator.CreateVariable(hourVar + i + "_Time", driver, ChannelType.DriverVariable, "INT");
        //            variables[i-1][1] = creator.CreateVariable(hourVar + i + "_Day", driver, ChannelType.DriverVariable, "INT");
        //            variables[i-1][2] = creator.CreateVariable(hourVar + i + "_Cloud", driver, ChannelType.DriverVariable, "INT");
        //            variables[i-1][3] = creator.CreateVariable(hourVar + i + "_Temp", driver, ChannelType.DriverVariable, "INT" );
        //            variables[i-1][4] = creator.CreateVariable(hourVar + i + "_Cfraction", driver, ChannelType.DriverVariable, "REAL");
        //            variables[i-1][5] = creator.CreateVariable(hourVar + i + "_Rain", driver, ChannelType.DriverVariable, "REAL");
        //            variables[i - 1][6] = creator.CreateVariable(hourVar + i + "_Date", driver, ChannelType.DriverVariable, "STRING");
        //            variables[i - 1][7] = creator.CreateVariable(hourVar + i + "_Type", driver, ChannelType.DriverVariable, "INT");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        activeProject.Parent.Parent.DebugPrint("Exception thrown: " + ex.Message,DebugPrintStyle.Error);
        //    }
        //    return false;
        //}

        //public bool ChangeVariables()
        //{
        //    try
        //    {
        //        foreach (IVariable[] variable in variables)
        //        {
        //            foreach (IVariable i in variable)
        //            {
        //                i.SetDynamicProperty("Remanenz", 2);
        //                //i.SetDynamicProperty("DDEActive", "TRUE");
        //                //i.SetDynamicProperty("SetValueProtocol", 1);
        //                //i.SetDynamicProperty("SV_VBA", "FALSE");

        //            }
        //            //variable[4].SetDynamicProperty("Digits", 2);
        //            //variable[5].SetDynamicProperty("Digits", 2);
        //        }
        //        /* old code
        //        for (int i = 1; i <= 60; i++)
        //        {
        //            activeProject.VariableCollection[hourVar + i + "_Time"].SetDynamicProperty("Remanenz", 2);
        //            activeProject.VariableCollection[hourVar + i + "_Day"].SetDynamicProperty("Remanenz", 2);
        //            activeProject.VariableCollection[hourVar + i + "_Cloud"].SetDynamicProperty("Remanenz", 2);
        //            activeProject.VariableCollection[hourVar + i + "_Temp"].SetDynamicProperty( "Remanenz", 2);
        //            activeProject.VariableCollection[hourVar + i + "_Cfraction"].SetDynamicProperty("Remanenz", 2);
        //            activeProject.VariableCollection[hourVar + i + "_Rain"].SetDynamicProperty("Remanenz", 2);
        //            activeProject.VariableCollection[hourVar + i + "_Cfraction"].SetDynamicProperty("Digits", 2);
        //            activeProject.VariableCollection[hourVar + i + "_Rain"].SetDynamicProperty("Digits", 2);
        //        }
        //        // cloud forecast variable
        //        for (int i = 61; i <= 86; i++)
        //        {
        //            activeProject.VariableCollection[sixVar + i + "_Time"].SetDynamicProperty("Remanenz", 2);
        //            activeProject.VariableCollection[sixVar + i + "_Day"].SetDynamicProperty("Remanenz", 2);
        //            activeProject.VariableCollection[sixVar + i + "_Cloud"].SetDynamicProperty("Remanenz", 2);
        //            activeProject.VariableCollection[sixVar + i + "_Temp"].SetDynamicProperty("Remanenz", 2);
        //            activeProject.VariableCollection[sixVar + i + "_Cfraction"].SetDynamicProperty("Remanenz", 2);
        //            activeProject.VariableCollection[sixVar + i + "_Rain"].SetDynamicProperty("Remanenz", 2);
        //            activeProject.VariableCollection[sixVar + i + "_Cfraction"].SetDynamicProperty("Digits", 2);
        //            activeProject.VariableCollection[sixVar + i + "_Rain"].SetDynamicProperty("Digits", 2);
        //        }
        //        */

        //    }
        //    catch (Exception ex)
        //    {
        //        activeProject.Parent.Parent.DebugPrint("Exception thrown: " + ex.Message, DebugPrintStyle.Error);
        //    }
        //    return false;
        //}
    }

}