using Blazui.Component.EventArgs;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.Component.CheckBox
{
    public class SimpleCheckBoxBase : ComponentBase
    {
        protected string _isChecked = string.Empty;
        protected string _isIndeterminate = string.Empty;
        protected string isDisabled;

        [Parameter]
        public Status Status { get; set; }

        [Parameter]
        public object TrueValue { get; set; }

        [Parameter]
        public object FalseValue { get; set; }

        [Parameter]
        public Action<ChangeEventArgs> OnStatusChanged { get; set; }
        [Parameter]
        public EventCallback<Status> StatusChanged { get; set; }

        protected void ChangeStatus(ChangeEventArgs uIMouseEvent)
        {
            if (IsDisabled)
            {
                return;
            }
            var oldValue = new CheckBoxValue()
            {
                Status = Status
            };
            var newValue = new CheckBoxValue();
            switch (Status)
            {
                case Status.UnChecked:
                    oldValue.Value = FalseValue;
                    Status = Status.Checked;
                    newValue.Value = TrueValue;
                    break;
                case Status.Checked:
                    Status = Status.UnChecked;
                    oldValue.Value = TrueValue;
                    newValue.Value = FalseValue;
                    break;
                case Status.Indeterminate:
                    Status = Status.Checked;
                    oldValue.Value = default;
                    newValue.Value = TrueValue;
                    break;
            }
            newValue.Status = Status;
            var args = new BChangeEventArgs<CheckBoxValue>();
            args.OldValue = oldValue;
            args.NewValue = newValue;
            OnStatusChanged?.Invoke(args);
        }

        [Parameter]
        public bool IsDisabled
        {
            get
            {
                return isDisabled == "is-disabled";
            }
            set
            {
                if (value)
                {
                    isDisabled = "is-disabled";
                }
                else
                {
                    isDisabled = null;
                }
            }
        }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
