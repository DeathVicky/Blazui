﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.Component.EventArgs
{
    public class ChangeEventArgs<T> : UIChangeEventArgs
    {
        public T OldValue { get; set; }
        public T NewValue { get; set; }
    }
}