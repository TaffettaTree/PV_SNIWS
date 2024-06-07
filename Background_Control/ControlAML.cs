using Scada.AddIn.Contracts.AlarmMessageList;
using Scada.AddIn.Contracts.Variable;
using Scada.AddIn.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background_Control
{
    internal class ControlAML
    {
        private readonly IProject _project = null;
        private readonly IVariable _message = null;
        private readonly IVariable _switch = null;
        private readonly IAlarmMessageList _AML = null;
        public ControlAML(IProject activeProject, IVariable messageVariable,IVariable switchVariable,IAlarmMessageList AML )
        {
            this._message = messageVariable;
            this._switch = switchVariable;
            this._AML = AML;
            this._project = activeProject;
        }
        

        public void CreateEntry(string message)
        {
            _message.SetValue(0, message);
            _switch.SetValue(0, 1);
        }

    }
}
