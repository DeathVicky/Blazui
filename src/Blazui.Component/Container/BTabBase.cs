﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Blazui.Component.Dom;
using Blazui.Component.EventArgs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Blazui.Component.Container
{
    public class BTabBase : ComponentBase
    {
        [Parameter]
        public bool? IsClosable { get; set; }
        [Parameter]
        public bool? IsAddable { get; set; }

        [Parameter]
        public TabType Type { get; set; }

        [Parameter]
        public bool IsEditable { get; set; }

        [Parameter]
        public TabPosition TabPosition { get; set; }
        [Inject]
        private IJSRuntime JSRuntime { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public EventCallback<BChangeEventArgs<ITab>> OnActiveTabChanged { get; set; }

        [Parameter]
        public Func<ITab, Task<bool>> OnActiveTabChangingAsync { get; set; }

        protected async Task<bool> OnInternalActiveTabChangingAsync(ITab tab)
        {
            if (OnActiveTabChangingAsync == null)
            {
                return await Task.FromResult(true);
            }
            return await OnActiveTabChangingAsync(tab);
        }

        //internal async Task AddTabAsync(TabOption option)
        //{
        //    if (!TabPanels.Contains(tab))
        //    {
        //        TabPanels.Add(tab);
        //    }
        //    if (ActiveTab == null)
        //    {
        //        await SetActivateTabAsync(tab);
        //    }
        //}

        [Parameter]
        public EventCallback<MouseEventArgs> OnAddingTab { get; set; }

        //protected override async Task OnAfterRenderAsync()
        //{
        //    if (ActiveTab == null)
        //    {
        //        return;
        //    }
        //    var dom = ActiveTab.Element.Dom(JSRuntime);
        //    var width = await dom.GetClientWidthAsync();
        //    var paddingLeft = await dom.Style.GetPaddingLeftAsync();
        //    var offsetLeft = await dom.GetOffsetLeftAsync();
        //    var padding = paddingLeft + (await dom.Style.GetPaddingRightAsync());
        //    var barWidth = width - padding;
        //    var barOffsetLeft = offsetLeft + paddingLeft;
        //    if (BarWidth == barWidth && barOffsetLeft == BarOffsetLeft)
        //    {
        //        if (OnRenderCompleteAsync != null)
        //        {
        //            await OnRenderCompleteAsync();
        //        }
        //        return;
        //    }
        //    BarWidth = barWidth;
        //    BarOffsetLeft = barOffsetLeft;
        //}

        //public async Task RemoveTabAsync(ITab tab)
        //{
        //    var index = For.IndexOf(tab);
        //    if (!RemovedTabPanels.Contains(tab))
        //    {
        //        RemovedTabPanels.Add(tab);
        //    }
        //    TabPanels.Remove(TabPanels.FirstOrDefault(x => x.Name == tab.Name));
        //    ITab<T> activeTab;
        //    if (index <= TabPanels.Count - 1)
        //    {
        //        activeTab = TabPanels[index];
        //    }
        //    else
        //    {
        //        activeTab = TabPanels[TabPanels.Count - 1];
        //    }
        //    await SetActivateTabAsync(activeTab);
        //}

        //public async Task SetActivateTabAsync(ITab<T> tab)
        //{
        //    var eventArgs = new BChangeEventArgs<ITab<T>>();
        //    eventArgs.OldValue = ActiveTab;
        //    eventArgs.NewValue = tab;
        //    if (ActiveTab != tab)
        //    {
        //        if (ActiveTab != null)
        //        {
        //            ActiveTab.IsActive = false;
        //        }
        //        ActiveTab = tab;
        //        if (ActiveTab != null)
        //        {
        //            ActiveTab.IsActive = true;
        //        }
        //        if (OnActiveTabChanged.HasDelegate)
        //        {
        //            await OnActiveTabChanged.InvokeAsync(eventArgs);
        //        }
        //    }
        //}

        //public event Func<Task> OnRenderCompleteAsync;

        //protected override void OnParametersSet()
        //{
        //    if (Type == TabType.Normal && IsEditable)
        //    {
        //        throw new NotSupportedException("TabType为Card的情况下才能进行编辑");
        //    }
        //    base.OnParametersSet();
        //}
    }
}
