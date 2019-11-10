using Blazui.Component.CheckBox;
using Blazui.Component.EventArgs;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.ServerRender.Demo.CheckBox
{
    public class HardCodeBase : ComponentBase
    {
        public int Value { get; set; }
        public Status Status = Status.Checked;
        public Status Status2 = Status.Checked;
        public void ChangeValue(ChangeEventArgs e)
        {
            var args = (BChangeEventArgs<CheckBoxValue>)e;
            Value = Convert.ToInt32(args.NewValue.Value);

        }
    }
}
