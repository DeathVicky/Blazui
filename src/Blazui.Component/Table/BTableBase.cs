using Blazui.Component.Dom;
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
        public List<TRow> DataSource { get; set; }

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
        protected override void OnInitialized()
        {
            //Headers = typeof(TRow).GetProperties().Select(x => new
            //{
            //    TableColumn = (TableColumnAttribute)x.GetCustomAttributes(typeof(TableColumnAttribute), true).FirstOrDefault(),
            //    Row = x
            //}).Where(x => x.TableColumn != null)
            //.Select(x => new TableHeader()
            //{
            //    Property = x.Row.Name,
            //    Text = x.TableColumn.Text,
            //    Width = x.TableColumn.Width
            //}).ToArray();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (requireRender)
            {
                StateHasChanged();
                requireRender = false;
                return;
            }
        }
    }
}
