﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.ServerRender.Model
{
    public class DemoInfoModel
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public IList<string> Files { get; set; }
    }
}
