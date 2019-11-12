using Blazui.Component.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.ServerRender.Demo.Table
{
    public class TestData
    {
        [TableColumn(Text = "时间", Width = 100)]
        public DateTime Time { get; set; }
        [TableColumn(Text = "名称", Width = 100)]
        public string Name { get; set; }
        [TableColumn(Text = "地址", Width = 200)]
        public string Address { get; set; }
    }
}
