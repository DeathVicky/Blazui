﻿using Blazui.Component.EventArgs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.Component.Radio
{
    public class BRadioBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public string SelectedValue { get; set; }

        [Parameter]
        public EventCallback<string> SelectedValueChanged { get; set; }

        [Parameter]
        public EventCallback<BChangeEventArgs<string>> SelectedValueChanging { get; set; }

        [CascadingParameter]
        public BRadioGroup RadioGroup { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public RadioSize Size { get; set; }
        [Parameter]
        public bool IsBordered { get; set; }
        [Parameter]
        public bool IsDisabled { get; set; }

        public event Func<BChangeEventArgs<string>, Task<bool>> OnValueChangingAsync;

        public event Func<BChangeEventArgs<string>, Task> OnSelectedValueChangedAsync;

        protected async Task OnRadioChangedAsync(MouseEventArgs e)
        {
            if (IsDisabled)
            {
                return;
            }
            if (RadioGroup != null)
            {
                await RadioGroup.TrySetValueAsync(Value);
                return;
            }
            var arg = new BChangeEventArgs<string>()
            {
                NewValue = Value,
                OldValue = SelectedValue
            };
            if (SelectedValueChanging.HasDelegate)
            {
                await SelectedValueChanging.InvokeAsync(arg);
                if (arg.DisallowChange)
                {
                    return;
                }
            }
            if (OnValueChangingAsync != null)
            {
                var allowChanging = await OnValueChangingAsync(arg);
                if (!allowChanging)
                {
                    return;
                }
            }
            SelectedValue = Value;
            if (SelectedValueChanged.HasDelegate)
            {
                await SelectedValueChanged.InvokeAsync(Value);
            }
            if (OnSelectedValueChangedAsync != null)
            {
                await OnSelectedValueChangedAsync(arg);
            }
        }
    }
}
