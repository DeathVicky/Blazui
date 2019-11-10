using Blazui.Component.EventArgs;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.Component.Radio
{
    public class BRadioGroupBase : ComponentBase
    {
        [Parameter]
        public EventCallback<string> SelectedValueChanged { get; set; }

        [Parameter]
        public RadioSize Size { get; set; }
        [Parameter]
        public EventCallback<BChangeEventArgs<string>> SelectedValueChanging { get; set; }
        [Parameter]
        public string SelectedValue { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public event Func<BChangeEventArgs<string>, Task<bool>> OnSelectedValueChangingAsync;

        public event Func<BChangeEventArgs<string>, Task> OnSelectedValueChangedAsync;

        internal async Task<bool> TrySetValueAsync(string value)
        {
            var arg = new BChangeEventArgs<string>()
            {
                NewValue = value,
                OldValue = SelectedValue
            };
            if (SelectedValueChanging.HasDelegate)
            {
                Console.WriteLine("2");
                await SelectedValueChanging.InvokeAsync(arg);
                if (arg.DisallowChange)
                {
                    return false;
                }
            }
            if (OnSelectedValueChangingAsync != null)
            {
                var allowChanging = await OnSelectedValueChangingAsync(arg);
                if (!allowChanging)
                {
                    return false;
                }
            }
            SelectedValue = value;
            if (SelectedValueChanged.HasDelegate)
            {
                await SelectedValueChanged.InvokeAsync(value);
            }
            if (OnSelectedValueChangedAsync != null)
            {
                await OnSelectedValueChangedAsync(arg);
            }
            return true;
        }
    }
}
