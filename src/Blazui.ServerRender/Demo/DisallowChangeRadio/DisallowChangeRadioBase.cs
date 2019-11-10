using Blazui.Component.EventArgs;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazui.ServerRender.Demo.DisallowChangeRadio
{
    public class DisallowChangeRadioBase : ComponentBase
    {
        protected string selectedValue = "1";

        protected void OnSelectedValueChanging(BChangeEventArgs<string> e)
        {
            e.DisallowChange = true;
        }
    }
}
