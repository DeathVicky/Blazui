﻿using Blazui.Component.CheckBox;
using Blazui.Component.Dom;
using Blazui.Component.EventArgs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.Component.Table
{
    public class BTableBase<TRow> : ComponentBase
    {
        protected ElementReference headerElement;
        public List<TableHeader<TRow>> Headers { get; set; } = new List<TableHeader<TRow>>();
        private bool requireRender = true;
        protected int headerHeight = 49;

        [Parameter]
        public List<TRow> DataSource { get; set; } = new List<TRow>();

        [Parameter]
        public HashSet<TRow> SelectedRows { get; set; } = new HashSet<TRow>();

        protected Status selectAllStatus;

        [Inject]
        private IJSRuntime jsRunTime { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public int Height { get; set; }

        /// <summary>
        /// 启用斑马纹
        /// </summary>
        [Parameter]
        public bool IsStripe { get; set; }

        /// <summary>
        /// 启用边框
        /// </summary>
        [Parameter]
        public bool IsBordered { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (requireRender)
            {
                StateHasChanged();
                requireRender = false;
                return;
            }
        }

        protected override void OnParametersSet()
        {
            RefreshSelectAllStatus();
        }

        void RefreshSelectAllStatus()
        {
            if (DataSource.Count == 0 || SelectedRows.Count == 0)
            {
                selectAllStatus = Status.UnChecked;
            }
            else if (DataSource.Count > SelectedRows.Count)
            {
                selectAllStatus = Status.Indeterminate;
            }
            else
            {
                selectAllStatus = Status.Checked;
            }
        }

        protected void ChangeAllStatus(Status status)
        {
            if (status == Status.Checked)
            {
                SelectedRows = new HashSet<TRow>(DataSource);
            }
            else
            {
                SelectedRows = new HashSet<TRow>();
            }
            RefreshSelectAllStatus();
        }

        protected void ChangeRowStatus(Status status, TRow row)
        {
            if (status == Status.Checked)
            {
                SelectedRows.Add(row);
            }
            else
            {
                SelectedRows.Remove(row);
            }
            RefreshSelectAllStatus();
        }
    }
}
