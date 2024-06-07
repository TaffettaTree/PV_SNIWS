using Scada.AddIn.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Scada.AddIn.Contracts.Frame;
using Scada.AddIn.Contracts.Screen;
using Scada.AddIn.Contracts.Variable;
using System.Security.Claims;

namespace Weather_Var_Creator
{
    public class Creator
    {
        IProject myProject = null;
        public Creator(IProject zProject) 
        {
            if (zProject == null)
            {
                System.Diagnostics.Debug.Print("No zenon refrence in Creator Consturctor");
                return;
            }
            myProject = zProject;
        }

        public bool CreateFrame(string frameName )
        {
            IFrame myframe = myProject.FrameCollection[frameName];
            try
            {
                if (myframe == null)
                {
                    myframe = myProject.FrameCollection.Create(frameName, true);
                    myProject.Parent.Parent.DebugPrint("New frame has been created: " + myframe.Name, DebugPrintStyle.Standard);
                    return true;
                }
                else
                {
                    myProject.Parent.Parent.DebugPrint("Frame: " + myframe.Name + "already exists, not overwritten", DebugPrintStyle.Warning);
                    return false;
                }
            }
            catch (Exception ex) 
            {
                myProject.Parent.Parent.DebugPrint("exception thrown: "+ex.Message, DebugPrintStyle.Error);
                return false;
            }
        }

        public bool CreateFrame(string frameName, int iTop, int iLeft, int iBottom, int iRight)
        {
            IFrame myframe = myProject.FrameCollection[frameName];
            try
            {
                if (myframe == null)
                {
                    myframe = myProject.FrameCollection.Create(frameName, true);
                    myframe.Top = iTop;
                    myframe.Left = iLeft;
                    myframe.Bottom = iBottom;
                    myframe.Right = iRight;
                    myProject.Parent.Parent.DebugPrint("New frame has been created: " + myframe.Name, DebugPrintStyle.Standard);
                    return true;
                }
                else
                {
                    myProject.Parent.Parent.DebugPrint("Frame: " + myframe.Name + " already exists, not overwritten", DebugPrintStyle.Warning);
                    return false;
                }
            }
            catch (Exception ex)
            {
                myProject.Parent.Parent.DebugPrint("exception thrown: " + ex.Message, DebugPrintStyle.Error);
                return false;
            }
        }

        public void CreateScreen(string screenName,string frameName)
        {
            IScreen myScreen = myProject.ScreenCollection[screenName];

            try
            {
                if (myScreen == null)
                {
                    myScreen = myProject.ScreenCollection.Create(screenName,frameName, ScreenType.Standard );
                    myProject.Parent.Parent.DebugPrint("Screen created succsefully: " + screenName,DebugPrintStyle.Standard);
                }
                else
                {
                    myProject.Parent.Parent.DebugPrint("Screen: " + screenName + " already exists", DebugPrintStyle.Warning);
                }
            }
            catch (System.Exception ex)
            {
                myProject.Parent.Parent.DebugPrint("exception thrown: " + ex.Message, DebugPrintStyle.Error);
            }
        }

        public void CreateDriver(string driverName, string driverExe)
        {
            IDriver myDriver = myProject.DriverCollection[driverName];

            try
            {
                if (myDriver == null)
                {
                    myDriver = myProject.DriverCollection.Create(driverName, driverExe, false);
                    myProject.Parent.Parent.DebugPrint("Driver created succsefully: " + driverName + "::" + driverExe, DebugPrintStyle.Standard);
                }
                else
                {
                    myProject.Parent.Parent.DebugPrint("Driver: " + driverName + "::" + driverExe + " already exists", DebugPrintStyle.Warning);
                }
            }
            catch (System.Exception ex)
            {
                myProject.Parent.Parent.DebugPrint("exception thrown: " + ex.Message, DebugPrintStyle.Error);
            }
        }
        public IVariable CreateVariable(string variableName,string driverName,ChannelType varChannelType, string dataType)
        {
            IVariable myVariable = null;
            try
            {
                myVariable = myProject.VariableCollection[variableName];

                IDataType myDataType = myProject.DataTypeCollection[dataType];
                if (myDataType == null)
                {
                    myProject.Parent.Parent.DebugPrint("Data Type does not exists: " + dataType, DebugPrintStyle.Warning);
                    return null;
                }

                IDriver myDriver = myProject.DriverCollection[driverName];
                if (myDriver == null)
                {
                    myProject.Parent.Parent.DebugPrint("Driver does not exists: " + driverName, DebugPrintStyle.Warning);
                    return null;
                }

            
                if (myVariable == null)
                {
                    myVariable = myProject.VariableCollection.Create(variableName, myDriver, varChannelType, myDataType); 
                    myProject.Parent.Parent.DebugPrint("Variable created succsefully: " + variableName + "::" + driverName +"::"+dataType, DebugPrintStyle.Standard);
                }
                else
                {
                    myProject.Parent.Parent.DebugPrint("Variable: " + variableName + "::" + driverName + " already exists"
                        , DebugPrintStyle.Warning);
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                myProject.Parent.Parent.DebugPrint("exception thrown: " + ex.Message, DebugPrintStyle.Error);
                return null;
            }
            return myVariable;
        }
    }
}
