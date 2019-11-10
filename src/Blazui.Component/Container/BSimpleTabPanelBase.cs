﻿using Blazui.Component.Dom;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.Component.Container
{
    public class BSimpleTabPanelBase : ComponentBase, ITab
    {
        private static int tabIndex = 0;
        protected bool isClosable
        {
            get
            {
                if (IsClosable.HasValue)
                {
                    return IsClosable.Value;
                }
                if (TabContainer.IsClosable.HasValue)
                {
                    return TabContainer.IsClosable.Value;
                }
                return TabContainer.IsEditable;
            }
        }

        [Parameter]
        public EventCallback<ITab> OnTabClose { get; set; }

        protected async Task OnInternalTabCloseAsync(MouseEventArgs e)
        {
            if (!OnTabClose.HasDelegate)
            {
                return;
            }
            await OnTabClose.InvokeAsync(this);
        }

        public bool IsActive { get; set; }
        public ElementReference  Element { get; set; }

        [CascadingParameter]
        public BSimpleTab TabContainer { get; set; }

        protected async Task Activate(MouseEventArgs e)
        {
            await TabContainer.SetActivateTabAsync(this);
        }

        [Parameter]
        public string Name { get; set; }

        public int Index { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool? IsClosable { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Index = TabContainer.TabPanels.Count;
            await TabContainer.AddTabAsync(this);
        }

        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                return;
            }
            Name = "tab_" + (++tabIndex);
            await Task.CompletedTask;
        }


        [Parameter]
        public Func<ITab, Task> OnRenderCompleted { get; set; }

        public event Func<ITab, Task> OnRenderCompletedAsync;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (OnRenderCompletedAsync != null)
            {
                await OnRenderCompletedAsync(this);
            }
            if (OnRenderCompleted != null)
            {
                await OnRenderCompleted(this);
            }
            await base.OnAfterRenderAsync(firstRender);
        }
    }
}
