using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2022_Test.uis
{
    public class CustomButton : Button
    {
        public enum StateType
        {
            Released = 0, Pressed
        };
        public class CustomButtonStateChangedEventArg : EventArgs
        {
            StateType Status;
            public CustomButtonStateChangedEventArg(StateType _status)
            {
                Status = _status;
            }
        }
        public EventHandler<CustomButtonStateChangedEventArg> StatusChanged;

        protected StateType _status = StateType.Released;

        private int _presscnt = 0;
        public virtual StateType Status
        {
            get => _status;
            set
            {
                _status = value;
                _presscnt = (int)_status;
                if (StatusChanged != null)
                {
                    StatusChanged.Invoke(this, new CustomButtonStateChangedEventArg(_status));
                }
            }
        }
        protected CustomButton()
        {
            this.Click += (s, e) =>
            {
                this.Status = (StateType)(++_presscnt % Enum.GetValues(typeof(StateType)).Length);
            };
        }

    }
}
