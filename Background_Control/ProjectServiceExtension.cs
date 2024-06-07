using Scada.AddIn.Contracts;
using Scada.AddIn.Contracts.AddIns;
using Scada.AddIn.Contracts.Function;
using Scada.AddIn.Contracts.Screen;
using Scada.AddIn.Contracts.Variable;
using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Background_Control
{
    /// <summary>
    /// Description of Project Service Extension.
    /// </summary>
    [AddInExtension("Background_Control", "Main Service for Control")]
    public class ProjectServiceExtension : IProjectServiceExtension
    {
        #region IProjectServiceExtension implementation
        IProject activeProject = null;
        IOnlineVariableContainer myContainer = null;
        readonly string onlineContainerName = "Background_Control_Container";
        BackgroundMethods commands = null;
        ControlAML AML = null;
        IFunction LoadingOn = null;
        IFunction LoadingOff = null;

        bool first = true;

        public void Start(IProject context, IBehavior behavior)
        {
            // Project initialization
            activeProject = context;
            if (activeProject == null)
            {
                Debug.Print("Reference to project is null!", DebugPrintStyle.Error);
                MessageBox.Show("Null projet in Background Control Service!");
                return;
            }
            //create background classes
            commands = new BackgroundMethods();
            AML = new ControlAML(activeProject, activeProject.VariableCollection["Control_AML_Text"],
                activeProject.VariableCollection["Control_AML_Switch"], activeProject.AlarmMessageList);
            // Variables that are checked
            string[] Control_Variables = { "Control_Weather_OnTime", "Control_Weather_Force", "App_ActiveAlarm", "Control_AML_Switch", "Control_AML_Text",
                "Control_Sunrise", "Control_Radiation_IF" };

            //Initialization of OnlineContainer
            if (activeProject.OnlineVariableContainerCollection[onlineContainerName] == null)
            {
                //create container
                myContainer = activeProject.OnlineVariableContainerCollection.Create(onlineContainerName);
                // add variables
                myContainer.AddVariable(Control_Variables);

                //subscriebe to bulk changed event
                myContainer.BulkChanged += MyContainer_BulkChanged;

                //activate bulk functionality
                myContainer.ActivateBulkMode();
                //activate online container itself
                myContainer.Activate();

            }
            activeProject.AlarmMessageList.AlarmEntryReceived += AlarmMessageList_AlarmEntryReceived;

            // Load additional resources
            LoadingOn = activeProject.FunctionCollection["Screen_Loading_On"];
            LoadingOff = activeProject.FunctionCollection["Screen_Loading_Off"];
        }

        private void AlarmMessageList_AlarmEntryReceived(object sender, Scada.AddIn.Contracts.AlarmMessageList.AlarmEntryReceivedEventArgs e)
        {
            activeProject.VariableCollection["App_ActiveAlarm"].SetValue(0, 1);
        }

        private void MyContainer_BulkChanged(object sender, BulkChangedEventArgs e)
        {

            // start a task
            Task.Run(() =>
            {

                foreach (IVariable variable in e.Variables)
                {
                    // Variable one
                    try
                    {
                        switch (variable.Name)
                        {
                            case "Control_Weather_OnTime":
                                Control_Weather_OnTime(variable);
                                break;
                            case "Control_Weather_Force":
                                Control_Weather_Force(variable);
                                break;
                            case "Control_Sunrise":
                                Control_Sunrise(variable);
                                break;
                            case "Control_Radiation_IF":
                                Control_Radiation(variable);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        AML.CreateEntry(ex.Message);
                    }
                }
            });
        }

        

        public void Stop()
        {
            // enter your code which should be executed when stopping the service for the SCADA Service Engine
            myContainer.BulkChanged -= MyContainer_BulkChanged;
            myContainer.Deactivate();
            activeProject.OnlineVariableContainerCollection.Delete(this.onlineContainerName);
            activeProject.AlarmMessageList.AlarmEntryReceived -= AlarmMessageList_AlarmEntryReceived;
        }

        #endregion

        private void Control_Radiation(IVariable variable)
        {
            if (Convert.ToBoolean(variable.GetValue(0)))
            {
                Task.Run(() =>
                {
                    LoadingOn.Execute();
                    try
                    {

                        StartStopOperation status = commands.RadiationLoadData(activeProject);
                        switch (status)
                        {
                            case StartStopOperation.Completed:
                                {
                                    activeProject.ChronologicalEventList.AddEventEntry("Wizard \"WeatherRadiation\" Succeedd");
                                    break;
                                }
                            case StartStopOperation.NotFound:
                                {
                                    throw new Exception("Wizard \"WeatherRadiationLoad\" NotFound");
                                }
                            case StartStopOperation.ErrorDuringoperation:
                                {
                                    throw new Exception("Wizard \"WeatherRadiationLoad\" ErrorDuringoperation");
                                }
                            case StartStopOperation.AlreadyRunning:
                                {
                                    throw new Exception("Wizard \"WeatherRadiationLoad\" is already running!");
                                }
                        }

                    }
                    catch (Exception ex)
                    {
                        AML.CreateEntry("SERVICE::MyContainer_BulkChanged::WeatherRadiationLoad: " + ex.Message);
                        activeProject.ChronologicalEventList.AddEventEntry("SERVICE::MyContainer_BulkChanged: Alarm Triggerd");
                    }
                    LoadingOff.Execute();
                });
            }
        }
        private void Control_Sunrise(IVariable variable)
        {
            if (Convert.ToBoolean(variable.GetValue(0)))
            {
                Task.Run(() =>
                {
                    LoadingOn.Execute();
                    try
                    {

                        StartStopOperation status = commands.SunriseLoadData(activeProject);
                        switch (status)
                        {
                            case StartStopOperation.Completed:
                                {
                                    activeProject.ChronologicalEventList.AddEventEntry("Wizard \"SunriseLoadData\" Succeedd");
                                    break;
                                }
                            case StartStopOperation.NotFound:
                                {
                                    throw new Exception("Wizard \"SunriseLoadData\" NotFound");
                                }
                            case StartStopOperation.ErrorDuringoperation:
                                {
                                    throw new Exception("Wizard \"SunriseLoadData\" ErrorDuringoperation");
                                }
                            case StartStopOperation.AlreadyRunning:
                                {
                                    throw new Exception("Wizard \"SunriseLoadData\" is already running!");
                                }
                        }

                    }
                    catch (Exception ex)
                    {
                        AML.CreateEntry("SERVICE::MyContainer_BulkChanged::SunriseLoadData: " + ex.Message);
                        activeProject.ChronologicalEventList.AddEventEntry("SERVICE::MyContainer_BulkChanged: Alarm Triggerd");
                    }
                    LoadingOff.Execute();
                });
            }
        }
        private void Control_Weather_OnTime(IVariable variable)
        {

            if (Convert.ToBoolean(variable.GetValue(0)))
            {
                Task.Run(() =>
                {
                    LoadingOn.Execute();
                    try
                    {

                        StartStopOperation status = commands.WeatherLoadData(activeProject);
                        switch (status)
                        {
                            case StartStopOperation.Completed:
                                {
                                    activeProject.ChronologicalEventList.AddEventEntry("Wizard \"WeatherLoadData\" Succeedd");
                                    break;
                                }
                            case StartStopOperation.NotFound:
                                {
                                    throw new Exception("Wizard \"WeatherLoadData\" NotFound");
                                }
                            case StartStopOperation.ErrorDuringoperation:
                                {
                                    throw new Exception("Wizard \"WeatherLoadData\" ErrorDuringoperation");
                                }
                            case StartStopOperation.AlreadyRunning:
                                {
                                    throw new Exception("Wizard \"WeatherLoadData\" is already running!");
                                }
                        }

                    }
                    catch (Exception ex)
                    {
                        AML.CreateEntry("SERVICE::MyContainer_BulkChanged::Control_Weather_On_Time: " + ex.Message);
                        activeProject.ChronologicalEventList.AddEventEntry("SERVICE::MyContainer_BulkChanged: Alarm Triggerd");
                    }
                    LoadingOff.Execute();
                });
            }
        }

        private void Control_Weather_Force(IVariable variable)
        {

            if (Convert.ToBoolean(variable.GetValue(0)))
            {
                Task.Run(() =>
                {
                    LoadingOn.Execute();
                    try
                    {

                        StartStopOperation status = commands.WeatherLoadData(activeProject);
                        switch (status)
                        {
                            case StartStopOperation.Completed:
                                {
                                    activeProject.ChronologicalEventList.AddEventEntry("Wizard \"Weather_LoadData\" Succeedd");
                                    break;
                                }
                            case StartStopOperation.NotFound:
                                {
                                    throw new Exception("Wizard \"Weather_LoadData\" NotFound");
                                }
                            case StartStopOperation.ErrorDuringoperation:
                                {
                                    throw new Exception("Wizard \"Weather_LoadData\" ErrorDuringoperation");
                                }
                            case StartStopOperation.AlreadyRunning:
                                {
                                    throw new Exception("Wizard \"Weather_LoadData\" is already running!");
                                }
                        }

                    }
                    catch (Exception ex)
                    {
                        AML.CreateEntry("SERVICE::MyContainer_BulkChanged::Control_Weather_Force: " + ex.Message);
                        activeProject.ChronologicalEventList.AddEventEntry("SERVICE::MyContainer_BulkChanged: Alarm Triggerd");
                    }
                    LoadingOff.Execute();
                });
            }
        }

    }
}