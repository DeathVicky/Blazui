using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.Component.CheckBox
{
    public class CheckBoxOption
    {
        public Status Status { get; set; }
        public object TrueValue { get; set; }
        public object FalseValue { get; set; }
        public string Label { get; set; }
        public bool IsDisabled { get; set; }
    }
}
