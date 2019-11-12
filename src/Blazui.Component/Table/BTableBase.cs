using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.Component.Table
{
    public class BTableBase<TRow> : ComponentBase
    {
        public List<TableHeader<TRow>> Headers { get; set; } = new List<TableHeader<TRow>>();
        private bool rendered = false;

        [Parameter]
        public List<TRow> Rows { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
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

        protected override void OnAfterRender(bool firstRender)
        {
            if (rendered)
            {
                return;
            }
            rendered = true;
            StateHasChanged();
        }
    }
}
