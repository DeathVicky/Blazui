using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.Component.Table
{
    public class BTableBase<TRow> : ComponentBase
    {
        [Parameter]
        public IReadOnlyCollection<TableHeader> Headers { get; set; }

        [Parameter]
        public IReadOnlyCollection<TRow> Rows { get; set; }

        protected override void OnInitialized()
        {
        }
    }
}
